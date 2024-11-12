using UnityEngine;
using UnityEngine.AI;


public class PatrolState : StateBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LineOfSight2D lineOfSight;
    [SerializeField] private AttackState attackState;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private bool fromAtoB;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        spriteRenderer.color = Color.white; 
        attackState.isUnderground = true; //makes sure the worm is underground
    }

    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.zero; //makes sure the worm isn't affected by forces from the player
        
        //if worm reaches its destination, move to next point
        if (fromAtoB == true)
        {
            agent.SetDestination(pointB.transform.position);
        }
        if (fromAtoB == false)
        {
            agent.SetDestination(pointA.transform.position);
        }

        //if distance between worm and the point is close enough switch the direction to the other point
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
        attackState.isUnderground = true; //makes sure the worm is underground
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}