using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum WormType { Small, Big }

public class AttackState : StateBehaviour
{
     //type of worm
    [SerializeField] private WormType wormType;
    private Transform player;      
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private LineOfSight lineOfSight;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    [SerializeField] public float attackCooldown = 2f;  // time between attacks
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
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // find the player by tag
        agent = GetComponent<NavMeshAgent>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log("I;m in acctack state");
        agent.isStopped = true; //stops the worm so it doesnt wiggle around
        isUnderground = true;
        // if (isUnderground == true)
        // {
        //     animator.SetBool("isUnderGround", true);
        // }
        // else if (isUnderground == false)
        // {
        //     animator.SetBool("isAboveGround", true);
        // }

        timeSinceOnAttackMode = 0;
    }

    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.zero;
        timeSinceLastAttack += Time.deltaTime; //count time since the last attack
        timeSinceOnAttackMode += Time.deltaTime; //count the time since the worm entered attack mode
        //Debug.Log(timeSinceOnAttackMode);
        CanDoMainAttackAnimation();
        CanDoEarthquakeAttackAnimation();
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
    
    private void CanDoMainAttackAnimation()
    {
        if (timeSinceLastAttack >= attackCooldown && lineOfSight.playerInAttackRange)
        {
            animator.SetTrigger("biting");
        }
    }

    private void CanDoEarthquakeAttackAnimation()
    {
        if (isUnderground && timeSinceOnAttackMode >= earthquakeCooldown && !bigAttackDone && lineOfSight.playerInAttackRange)
        {
            animator.SetTrigger("unbury");
        }
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
        // animator.SetBool("isAboveGround", true);
        // animator.SetBool("isUnderGround", false);
    }

    public override void OnStateEnd()
    {
        agent.isStopped = false; //unfreezes the worm so its ready for the next state
        lineOfSight.playerInAttackRange = false;
        timeSinceOnAttackMode = 0;
        animator.SetTrigger("bury");
        Debug.Log("I left acctack state");
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }

    private void CanDoMainAttackChecker()
    {
        if (timeSinceLastAttack >= attackCooldown && lineOfSight.playerInAttackRange)
        {
            Debug.Log("Performing main attack." );
            DoMainAttack();
        }
    }

    private void CanDoEarthquakeAttackChecker()
    {
        if (isUnderground && timeSinceOnAttackMode >= earthquakeCooldown && !bigAttackDone && lineOfSight.playerInAttackRange)
        {
            Debug.Log("Performing Earthquake Attack.");
            bigAttackDone = true;
            DoEarthquakeAttack();
        }
    }
}

