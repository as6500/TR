using UnityEngine;
using UnityEngine.AI;


public class PatrolState : StateBehaviour
//this is a script that makes it so an enemy moves from point A to point B, you can set point A and point B on unity as well as speed
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private bool fromAtoB;
    [SerializeField] private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LineOfSight2D lineOfSight;
    private Rigidbody2D rb;
    
    void Start()
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
        Debug.Log("patrol state started");
        spriteRenderer.color = Color.red;
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
        //agent.isStopped = true;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}