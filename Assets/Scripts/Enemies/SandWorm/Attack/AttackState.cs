using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateBehaviour
{
    public enum WormType { Small, Big }
    [SerializeField] private WormType wormType;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public int attackRange = 2; 
    private float attackCooldown = 5f;  // time between attacks
    private float timeSinceLastAttack;
    private Transform player;  
    private float attackSmallEarthquakeDamage = 4f;
    private float attackBigEarthquakeDamage = 6f;
    private float attackTailDamage = 5;
    private float attackBitingDamage = 3f;
    [SerializeField] private HealthScript healthScript;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    public bool isUnderground;
    
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
        spriteRenderer.color = Color.white;
        timeSinceLastAttack = 0f;  // reset the attack timer when the state starts
        agent.isStopped = true;
    }

    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.zero;
        timeSinceLastAttack += Time.deltaTime;
        
        //check if player is withing attack range and if the worm is underground
        if (Vector3.Distance(transform.position, player.position) <= attackRange && isUnderground)
        {
            DoEarthquakeAttack();
        }


        // check player is within attack range and if the attack cooldown has passed
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (timeSinceLastAttack >= attackCooldown)
            {
                DoMainAttack();
                timeSinceLastAttack = 0f;
            }
        }
    }

    private void DoMainAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            float attackMainDamage;
            if (wormType == WormType.Small)
            {
                attackMainDamage = attackBitingDamage;
            }
            else
            {
                attackMainDamage = attackTailDamage;
            }
            damageable.TakeDamage(gameObject, attackMainDamage);
        }
        Debug.Log("biting the player");
         isUnderground = false;
    }
    
    private void DoEarthquakeAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            float EarthquakeDamage;
            if (wormType == WormType.Small)
            {
                EarthquakeDamage = attackSmallEarthquakeDamage;
            }
            else
            {
                EarthquakeDamage = attackBigEarthquakeDamage;
            }
            damageable.TakeDamage(gameObject, EarthquakeDamage);
        }
        Debug.Log("earthquake attack");
        isUnderground = false;
    }

    public override void OnStateEnd()
    {
        agent.isStopped = false;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}
