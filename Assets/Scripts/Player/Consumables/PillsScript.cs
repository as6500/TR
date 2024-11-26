using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    [SerializeField] private int pillsQuantity = 10;
	private bool pillsTaken;
	[SerializeField] private float healthAmount = 30.0f; //amount of health gained in total
    [SerializeField] private float healingTimeSec = 10.0f;
    [SerializeField] private float delayTimeSec = 2.0f;//2 sec in 2 sec it heals /during 10 seconds
	[SerializeField] private PillsUIScript pillsUIScript;
	[SerializeField] private HealthScript healthScript;
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2) && healthScript.CurrentHealthReturn() < 100.0f && pillsTaken == false)
		{
			GainHealth();
			PillCount();
		}
		
		if (pillsTaken) 
			return;
	}

	public void GainHealth()
    {
        if (healthScript.CanHealAmount(healthAmount) == false)
            healthScript.CanHeal(false);

        if (healthScript && healthScript.CanHealAmount(healthAmount))
			StartCoroutine(PillsTimer(healingTimeSec));
    }

	private void PillCount()
	{
		if (pillsQuantity <= 0) 
			return;
		
		pillsQuantity--;
		pillsTaken = true;
		pillsUIScript.UpdateUIText();
	}

	public int PillsQuantityReturn()
	{
		return pillsQuantity;
	}

    public void UpdatePills(int quantity)
    {
        pillsQuantity += quantity;
        pillsUIScript.UpdateUIText();
    }

    private IEnumerator PillsTimer(float secondsLeft)
	{
		if (secondsLeft > 0)
		{
			if (healthScript.CurrentHealthReturn() >= 100.0f) 
				yield break;
			
			yield return new WaitForSeconds(delayTimeSec);
			healthScript.HealthRegen((healthAmount / healingTimeSec) * delayTimeSec);
			StartCoroutine(PillsTimer(secondsLeft - delayTimeSec));
		}
		else
			pillsTaken = false;
	}
}
