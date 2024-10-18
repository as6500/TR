using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class OnPlayerHealthChanged : UnityEvent<float> { };
public class OnPlayerDied : UnityEvent { };

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100.0f;
    private float currentHealth = 0.0f;
    private float normalizedHealth = 0.0f;
    private bool canHeal = false;

    public OnPlayerHealthChanged OnPlayerHealthChangedEvent;

    private void Start()
    {
        currentHealth = maxHealth;
        normalizedHealth = currentHealth / maxHealth;

        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }

    public void DealDamage(float damageAmount)
    {
        ModifyHealth(-damageAmount);
        canHeal = true;
        normalizedHealth = currentHealth / maxHealth;

        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }

    public void HealthRegen(float healAmount)
    {
        ModifyHealth(healAmount);
        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }

    private void ModifyHealth(float modifier)
    {
        currentHealth = Mathf.Clamp(currentHealth + modifier, 0.0f, maxHealth);
        normalizedHealth = currentHealth / maxHealth;
    }

    public bool CanHealAmount(float amount)
    {
        return normalizedHealth < 1.0f;
    }
}
