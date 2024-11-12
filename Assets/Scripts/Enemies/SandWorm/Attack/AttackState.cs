using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateBehaviour
{
    public enum WormType { Small, Big } //type of worm
    [SerializeField] private WormType wormType;
    private SpriteRenderer spriteRenderer;
    private Transform player;      
    [SerializeField] private HealthScript healthScript;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    [SerializeField] public float attackCooldown = 5f;  // time between attacks
    public float timeSinceLastAttack;
    private float attackSmallEarthquakeDamage = 4f;
    private float attackBigEarthquakeDamage = 6f;
    public float attackRange = 2f;
    private float attackTailDamage = 5f;
    private float attackBitingDamage = 3f;
    public bool isUnderground; //verifies if the worm is underground so it can do the earthquake attack
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  // find the player by tag
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        spriteRenderer.color = Color.red;
        timeSinceLastAttack = 0f;  // reset the attack timer when the state starts
        agent.isStopped = true; //stops the worm so it doesnt wiggle around
    }

    public override void OnStateUpdate()
    {
        Debug.Log(timeSinceLastAttack);
        rb.velocity = Vector2.zero;
        timeSinceLastAttack += Time.deltaTime; //count time since the last attack
        //check if player is withing attack range and if the worm is underground and if so does the earthquake attack

    Debug.Log("Distance to player: " + Vector2.Distance(transform.position, player.position));
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (isUnderground)
            {
                Debug.Log("Performing Earthquake Attack.");
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
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}
