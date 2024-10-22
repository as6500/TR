using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
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
    private int pillsCount = 10; //change later
    private PillsScript pillsScript;
    private AntiRadiationScript antiRadiationScript;
    [SerializeField] private PillsUIScript pillsUIScript;
    [SerializeField] private AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;

    public OnPlayerHealthChanged OnPlayerHealthChangedEvent;

    private void Start()
    {
        currentHealth = maxHealth;
        normalizedHealth = currentHealth / maxHealth;

        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);

        pillsScript = gameObject.GetComponent<PillsScript>();
        antiRadiationScript = gameObject.GetComponent<AntiRadiationScript>();

	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Q) && currentHealth < 100.0f) //pills taken
        {
			pillsScript.GainHealth();
            pillsUIScript.PillCount();
        }

        if (Input.GetKeyDown(KeyCode.E)) //flasks taken
        {
            antiRadiationScript.GainImmunity();
            antiRadiationFlaskUIScript.FlasksCount();

		}
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
}
