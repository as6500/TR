using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class OnPlayerHealthChanged : UnityEvent<float> { }


public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 12.0f;
    private float currentHealth = 0.0f;
    private float normalizedHealth = 0.0f;

    public OnPlayerHealthChanged OnPlayerHealthChangedEvent;

    private void Start()
    {
        currentHealth = MaxHealth;
        normalizedHealth = currentHealth / MaxHealth;

        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }
    private void ModifyHealth(float modifier)
    {
        currentHealth = Mathf.Clamp(currentHealth + modifier, 0.0f, MaxHealth);
        normalizedHealth = currentHealth / MaxHealth;
    }
}
