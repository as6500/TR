using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    private int healthAmount = 2;

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
        }


    }
}
