using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantHumanAttack : MonoBehaviour
{
    [SerializeField] private MutantHumanState state;
    [SerializeField] private int damage;
    private IDamageable player;
    private bool attacking;

    void Start()
    {
        state = GetComponent<MutantHumanState>();
        attacking = false; 
    }

    private void Update()
    {
        if (attacking) 
        {
            UpdateAttackState(); 
        }
    }

    public void UpdateAttackState()
    {
        if (state.GetCurrentState() == State.Attack)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            player = damageable;
        }
    }

    private void DealDamageToPlayer()
    {
        if (player != null)
        {
            player.TakeDamage(gameObject, damage, 500);
        }
    }
}
