using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    private bool tookPills = false;
    private int healthAmount = 2;
    private int pillsCount = 0;

    public void GainHealth()
    {
        HealthScript healthScript = gameObject.GetComponent<HealthScript>();
        if (healthScript.CanHealAmount(healthAmount) == false)
        {
            healthScript.CanHeal(false);
        }

        if (healthScript && healthScript.CanHealAmount(healthAmount))
        {
            healthScript.HealthRegen(healthAmount);
            tookPills = true;
            pillsCount--;
        }


    }
}
