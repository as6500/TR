using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerLostState : StateBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float searchDuration = 3f; // time to search after losing the player
    private float searchTimer;
    private NavMeshAgent agent;
    private float waitToPatrolMaxTime = 3f;
    private float waitToPatrolTime = 0f;
    public bool readyToPatrol = false; 
    [SerializeField] private LineOfSight2D lineOfSight;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private SmallSandwormAttackState smallSandwormAttackState;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("Player lost. Entering Lost State.");
        spriteRenderer.color = Color.blue;
        searchTimer = searchDuration;
        waitToPatrolTime = 0f;
        readyToPatrol = false;
        agent.isStopped = true;
        smallSandwormAttackState.isUnderground = true;
    }

    public override void OnStateUpdate()
    {
        if (lineOfSight.HasSeenPlayerThisFrame() == false)
        {
            waitToPatrolTime = waitToPatrolTime + Time.deltaTime;
            Debug.Log("Patrol time: " + waitToPatrolTime);
        }
        else
        {
            waitToPatrolTime = 0f;
            readyToPatrol = false;
        }
        
        if (waitToPatrolTime >= waitToPatrolMaxTime)
        {
            readyToPatrol = true;
            Debug.Log("I am ready to patrol");
        }
    }

    public override void OnStateEnd()
    {
        agent.isStopped = false; 
        smallSandwormAttackState.isUnderground = true;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}