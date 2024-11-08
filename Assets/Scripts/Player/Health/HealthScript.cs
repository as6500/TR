using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerHealthChanged : UnityEvent<float> { };
//public class OnPlayerDied : UnityEvent { };

public class HealthScript : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100.0f;
    private float currentHealth = 0.0f;
    private float normalizedHealth = 0.0f;
    private bool canHeal = false;
    private AntiRadiationScript antiRadiationScript;

    public OnPlayerHealthChanged OnPlayerHealthChangedEvent;

    private void Start()
    {
        currentHealth = maxHealth;
        normalizedHealth = currentHealth / maxHealth;

        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);

        antiRadiationScript = gameObject.GetComponent<AntiRadiationScript>();
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
        if (currentHealth < 100.0f)
        {
			ModifyHealth(healAmount);
			OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
		}
    }

    public void DamageFromRadiation(float damageAmount) //damage from radiation if player doesn't take an anti-radiation flask
    {
        ModifyHealth(-damageAmount);
		normalizedHealth = currentHealth / maxHealth;

		OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }

    private void ModifyHealth(float modifier)
    {
        currentHealth = Mathf.Clamp(currentHealth + modifier, 0.0f, maxHealth);
        normalizedHealth = currentHealth / maxHealth;
    }

    public bool CanHealAmount(float amount)
    {
        if (normalizedHealth >= 1.0f)
        {
            canHeal = false;
        }
        return normalizedHealth < 1.0f;
    }

    public bool CanHeal(bool heal)
    {
		return canHeal;
    }

	public float CurrentHealthReturn()
	{
		return currentHealth;
	}

    public virtual void TakeDamage(GameObject instigator)
    {
        Destroy(gameObject);
    }
}
