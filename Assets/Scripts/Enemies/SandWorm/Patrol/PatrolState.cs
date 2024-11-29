using UnityEngine;
using UnityEngine.AI;


public class PatrolState : StateBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LineOfSight lineOfSight;
    private Rigidbody2D rb;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private bool fromAtoB;
    [SerializeField] private AttackState attack;
    // public Animator animator;

    void Start()
    {
        attack = gameObject.GetComponent<AttackState>();
        rb = GetComponent<Rigidbody2D>();
        agent.enabled = true;
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        attack.SetBigAttackDone(false);
    }

    public override void OnStateUpdate()
    {
        // animator.SetBool("isPatrolling", true);
        rb.velocity = Vector2.zero; //makes sure the worm isn't affected by forces from the player
        
        //if worm reaches its destination, move to next point
        if (fromAtoB == true)
        {
            agent.SetDestination(pointB);
        }
        if (fromAtoB == false)
        {
            agent.SetDestination(pointA);
        }

        //if distance between worm and the point is close enough switch the direction to the other point
        if (Vector2.Distance(agent.transform.position, pointB) <= 0.1f)
        {
            fromAtoB = false;
        }
        
        if (Vector2.Distance(agent.transform.position, pointA) <= 0.1f)
        {
            fromAtoB = true;
        }
    }

    public override void OnStateEnd()
    {
        // animator.SetBool("isPatrolling", false);
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }

    public void SetPoints(Vector3 a, Vector3 b)
    {
        pointA = a;
        pointB = b;
    }
}