using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    [SerializeField] private float DamageToDeal = 3.0f;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            HealthScript healthScript = collider.GetComponent<HealthScript>();
            if (healthScript)
            {
				Debug.Log("DamageToDeal: " + DamageToDeal);
				healthScript.DealDamage(DamageToDeal);
            }
        }
    }
}