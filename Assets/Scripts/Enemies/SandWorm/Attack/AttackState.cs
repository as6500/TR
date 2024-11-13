using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum WormType { Small, Big }

public class AttackState : StateBehaviour
{
     //type of worm
    [SerializeField] private WormType wormType;
    private SpriteRenderer spriteRenderer;
    private Transform player;      
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private LineOfSight lineOfSight;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    [SerializeField] public float attackCooldown = 5f;  // time between attacks
    public float timeSinceLastAttack;
    [SerializeField] public float earthquakeCooldown = 0.5f; //cooldown so the worm doesnt attack immediately after emerging
    [SerializeField] public float timeSinceOnAttackMode = 0f;
    private float attackSmallEarthquakeDamage = 4f;
    private float attackBigEarthquakeDamage = 6f;
    [SerializeField] public float attackRange = 2f;
    private float attackTailDamage = 5f;
    private float attackBitingDamage = 3f;
    public bool isUnderground; //verifies if the worm is underground so it can do the earthquake attack
    private bool bigAttackDone = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  // find the player by tag
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool InitializeState()
    {
        return true;
    }
    
    public void SetBigAttackDone(bool value)
    {
        bigAttackDone = value;
    }

    public override void OnStateStart()
    {
        spriteRenderer.color = Color.red;
        agent.isStopped = true; //stops the worm so it doesnt wiggle around
        isUnderground = true;
        timeSinceOnAttackMode = 0;
    }

    public override void OnStateUpdate()
    {
        //Debug.Log("is player in attack range?" + lineOfSight.playerInAttackRange);
        //Debug.Log(timeSinceLastAttack);
        rb.velocity = Vector2.zero;
        timeSinceLastAttack += Time.deltaTime; //count time since the last attack
        timeSinceOnAttackMode += Time.deltaTime; //count the time since the worm entered attack mode
        Debug.Log(timeSinceOnAttackMode);
        
        //check if player is withing attack range and if the worm is underground and if so does the earthquake attack
        if (lineOfSight.playerInAttackRange)
        {
            if (isUnderground && timeSinceOnAttackMode >= earthquakeCooldown && !bigAttackDone)
            {
                Debug.Log("Performing Earthquake Attack.");
                bigAttackDone = true;
                DoEarthquakeAttack();
            }
            else if (timeSinceLastAttack >= attackCooldown)
            {
                Debug.Log("Performing main attack.");
                DoMainAttack();
            }
        }
    }
    
    private void DoMainAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            float attackMainDamage;
            if (wormType == WormType.Small) //does the correct earthquake damage depending on the type of worm
            {
                attackMainDamage = attackBitingDamage;
            }
            else
            {
                attackMainDamage = attackTailDamage;
            }
            damageable.TakeDamage(gameObject, attackMainDamage);
        }
        isUnderground = false;
        timeSinceLastAttack = 0f;
    }
    
    private void DoEarthquakeAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            float EarthquakeDamage;
            if (wormType == WormType.Small) //does the correct earthquake damage depending on the type of worm
            {
                EarthquakeDamage = attackSmallEarthquakeDamage;
            }
            else
            {
                EarthquakeDamage = attackBigEarthquakeDamage;
            }
            damageable.TakeDamage(gameObject, EarthquakeDamage);
            Debug.Log("Earthquake attack performed: " + EarthquakeDamage + " damage to player");
            timeSinceLastAttack = 0f;
        }
        isUnderground = false;  //emerges the worm

    }

    public override void OnStateEnd()
    {
        agent.isStopped = false; //unfreezes the worm so its ready for the next state
        lineOfSight.playerInAttackRange = false;
        timeSinceOnAttackMode = 0;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
    
    
}
