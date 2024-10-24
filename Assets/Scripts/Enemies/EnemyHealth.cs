using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
