using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
	[SerializeField] private float healthAmount = 30.0f; //amount of health gained in total
    [SerializeField] private float healingTimeSec = 10.0f;
    [SerializeField] private float delayTimeSec = 2.0f;//2 sec in 2 sec it heals /during 10 seconds
	private int pillsQuantity = 10;
	[SerializeField] private PillsUIScript pillsUIScript;
	private HealthScript healthScript;
	private void Start()
	{
		healthScript = gameObject.GetComponent<HealthScript>();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && healthScript.CurrentHealthReturn() < 100.0f) //pills taken
		{
			GainHealth();
			PillCount();
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
            StartCoroutine(timer());
        }
    }

	private void PillCount()
	{
		if (pillsQuantity > 0)
		{
			pillsQuantity--;
			pillsUIScript.UpdateUIText();
		}
	}

	public int PillsQuantityReturn()
	{
		return pillsQuantity;
	}

	private IEnumerator timer()
	{
		for (float i = healingTimeSec; i > 0; i -= delayTimeSec)
		{
			if (healthScript.CurrentHealthReturn() < 100.0f)
			{
				yield return new WaitForSeconds(delayTimeSec);
				healthScript.HealthRegen((healthAmount / healingTimeSec) * delayTimeSec);
			}
			else
			{
				StopCoroutine(timer());
			}
		}
	}
}
