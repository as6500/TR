using UnityEngine;

public class SmallSandwormAttackState : StateBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] public int attackRange = 2; 
    private float attackCooldown = 5f;  // time between attacks
    private float timeSinceLastAttack;
    private Transform player;  
    private float attackDamage = 5f;
    private HealthScriptForTesting playerHealthScript;
    private Rigidbody2D rb;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Find the player by tag (adjust if needed)
        rb = GetComponent<Rigidbody2D>();
        playerHealthScript = player.GetComponent<HealthScriptForTesting>();
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        spriteRenderer.color = Color.white;
        timeSinceLastAttack = 0f;  // Reset the attack timer when the state starts
    }

    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.zero;
        timeSinceLastAttack += Time.deltaTime;

        // check player is within attack range and if the attack cooldown has passed
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (timeSinceLastAttack >= attackCooldown)
            {
                DoAttack();
                timeSinceLastAttack = 0f;
            }
        }
    }

    private void DoAttack()
    {
        //Debug.Log("Attacking the player!");
         if (playerHealthScript != null)
         {
             playerHealthScript.TakeDamage(attackDamage);
             Debug.Log($"Player takes {attackDamage} damage!");
         }
         else
         {
             Debug.Log("PlayerHealth component not found on the player!");
         }
         if (playerHealthScript.currentHealth <= 0)
         {
             Debug.Log("player dead");
         }
    }

    public override void OnStateEnd()
    {
        
    }

    public override int StateTransitionCondition()
    {
        return -1;  // Define your transition condition here based on your state machine
    }
}
