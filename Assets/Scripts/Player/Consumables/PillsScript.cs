using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
	[SerializeField] private float healthAmount = 30.0f; //amount of health gained in total
    [SerializeField] private float healingTimeSec = 10.0f;//during 10 seconds
    [SerializeField] private float delayTimeSec = 2.0f;//2 sec in 2 sec it heals 
	public void GainHealth()
    {
        HealthScript healthScript = gameObject.GetComponent<HealthScript>();
        if (healthScript.CanHealAmount(healthAmount) == false)
        {
            healthScript.CanHeal(false);
        }

        if (healthScript && healthScript.CanHealAmount(healthAmount))
        {
            StartCoroutine(timer(healthScript));
        }
    }

	private IEnumerator timer(HealthScript healthScript)
	{
        for (float i = healingTimeSec; i > 0; i -= delayTimeSec)
        {
			yield return new WaitForSeconds(delayTimeSec);
			healthScript.HealthRegen((healthAmount / healingTimeSec) * delayTimeSec);
		}
	}
}
