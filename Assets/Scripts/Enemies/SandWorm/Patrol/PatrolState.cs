using UnityEngine;
using UnityEngine.AI;


public class PatrolState : StateBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private bool fromAtoB;
    [SerializeField] private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LineOfSight2D lineOfSight;
    [SerializeField] private AttackState attackState;
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        spriteRenderer.color = Color.red;
        attackState.isUnderground = true;
    }

    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.zero;
        
        //if agent reaches its destination, move to next point
        if (fromAtoB == true)
        {
            agent.SetDestination(pointB.transform.position);
        }

        if (fromAtoB == false)
        {
            agent.SetDestination(pointA.transform.position);
        }

        if (Vector2.Distance(agent.transform.position, pointB.transform.position) <= 0.1f)
        {
            fromAtoB = false;
        }
        
        if (Vector2.Distance(agent.transform.position, pointA.transform.position) <= 0.1f)
        {
            fromAtoB = true;
        }
    }

    public override void OnStateEnd()
    {
        attackState.isUnderground = true;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}