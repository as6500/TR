using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerHealthChanged : UnityEvent<float> { };
//public class OnPlayerDied : UnityEvent { };

public class HealthScript : MonoBehaviour, IDamageable
{
    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float currentHealth = 0.0f;

    [Header("Damage")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color bloodColor;
    [SerializeField] private Color mainColor;
    [SerializeField] private float damageEffectTimeSeconds;
    
    private float normalizedHealth = 0.0f;
    private bool canHeal = false;

    public OnPlayerHealthChanged OnPlayerHealthChangedEvent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        normalizedHealth = currentHealth / maxHealth;

        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
	}

    private void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
    }

    public void DealDamage(float damageAmount)
    {
        ModifyHealth(-damageAmount, PlayRandomHurtSound());
        canHeal = true;
        normalizedHealth = currentHealth / maxHealth;
        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }

    private AudioSource PlayRandomHurtSound()
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            return audioManager.loseHealthOne;
        }
        else if (rand == 1)
        {
            return audioManager.loseHealthTwo;
        }
        else
        {
            return audioManager.loseHealthThree;
        }
    }

    public void HealthRegen(float healAmount)
    {
        if (currentHealth < 100.0f)
        {
			ModifyHealth(healAmount, audioManager.gainHealth);
			OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
		}
    }

    public void DamageFromRadiation(float damageAmount) //damage from radiation if player doesn't take an anti-radiation flask
    {
        ModifyHealth(-damageAmount, PlayRandomHurtSound());
        StartCoroutine(DamageEffect());
        normalizedHealth = currentHealth / maxHealth;
        
        OnPlayerHealthChangedEvent.Invoke(normalizedHealth);
    }

    public void ModifyHealth(float modifier, AudioSource audioManager)
    {
        audioManager.Play();
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

    private void KnockbackEffect(GameObject instigator, float force)
    {
        Vector2 tempKnockDirection = transform.position - instigator.transform.position;
        tempKnockDirection = tempKnockDirection.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.AddForce(tempKnockDirection * force);
    }

    public virtual void TakeDamage(GameObject instigator, float damage, float knockbackForce)
    {
        DealDamage(damage);
        StartCoroutine(DamageEffect());
        KnockbackEffect(instigator, knockbackForce);
    }

    public virtual Color GetBloodColor()
    {
        return bloodColor;
    }

    private IEnumerator DamageEffect()
    {
        spriteRenderer.color = bloodColor;
        yield return new WaitForSeconds(damageEffectTimeSeconds);
        spriteRenderer.color = mainColor;
        yield return new WaitForSeconds(damageEffectTimeSeconds);
        spriteRenderer.color = bloodColor; 
        yield return new WaitForSeconds(damageEffectTimeSeconds);
        spriteRenderer.color = mainColor;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
