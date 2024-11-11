using UnityEngine;
using UnityEngine.AI;

public class SmallSandwormAttackState : StateBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] public int attackRange = 2; 
    private float attackCooldown = 5f;  // time between attacks
    private float timeSinceLastAttack;
    private Transform player;  
    private float attackEarthquakeDamage = 4f;
    private float attackBitingDamage = 3f;
    [SerializeField] private HealthScript healthScript;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    public bool isUnderground;
    
    void Start()
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
                DoBitingAttack();
                timeSinceLastAttack = 0f;
            }
        }
    }

    private void DoBitingAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(gameObject, attackBitingDamage);
        }
        Debug.Log("biting the player");
         isUnderground = false;
    }
    
    private void DoEarthquakeAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(gameObject, attackEarthquakeDamage);
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
