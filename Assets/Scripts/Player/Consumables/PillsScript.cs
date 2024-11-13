using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    [SerializeField] private int pillsQuantity = 10;
	private bool pillsTaken = false;
	[SerializeField] private float healthAmount = 30.0f; //amount of health gained in total
    [SerializeField] private float healingTimeSec = 10.0f;
    [SerializeField] private float delayTimeSec = 2.0f;//2 sec in 2 sec it heals /during 10 seconds
	[SerializeField] private PillsUIScript pillsUIScript;
	private HealthScript healthScript;
	private void Start()
	{
		healthScript = gameObject.GetComponent<HealthScript>();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2) && healthScript.CurrentHealthReturn() < 100.0f) //pills taken
		{
			if (pillsTaken == false)
			{
				GainHealth();
				PillCount();
			}
		}
	}

	public void GainHealth()
    {
        if (healthScript.CanHealAmount(healthAmount) == false)
        {
            healthScript.CanHeal(false);
        }

        if (healthScript && healthScript.CanHealAmount(healthAmount))
        {
			StartCoroutine(PillsTimer(healingTimeSec));
        }
    }

	private void PillCount()
	{
		if (pillsQuantity > 0)
		{
			pillsQuantity--;
			
			pillsTaken = true;
		}
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
			if (healthScript.CurrentHealthReturn() < 100.0f)
			{
				yield return new WaitForSeconds(delayTimeSec);
				healthScript.HealthRegen((healthAmount / healingTimeSec) * delayTimeSec);
				StartCoroutine(PillsTimer(secondsLeft - delayTimeSec));
			}
		}
		else
		{
			pillsTaken = false;
		}
	}
}
