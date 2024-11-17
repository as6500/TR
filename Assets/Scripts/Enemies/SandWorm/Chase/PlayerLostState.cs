using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerLostState : StateBehaviour
{
    private float waitToPatrolMaxTime = 3f; //time to wait before switching to patrol state
    private float waitToPatrolTime = 0f;
    public bool readyToPatrol = false;
    private float searchDuration = 3f; // time to search after losing the player
    private float searchTimer;
    private NavMeshAgent agent;
    [SerializeField] private LineOfSight lineOfSight;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private AttackState attackState;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        searchTimer = searchDuration;
        waitToPatrolTime = 0f; //makes sure the waitToPatrol timer starts at 0 at the start of the state
        readyToPatrol = false; //makes sure the readyToPatrol boolean is off at the start of the state
        agent.isStopped = true; //stops the worm from moving
    }

    public override void OnStateUpdate()
    {
        if (lineOfSight.HasSeenPlayerThisFrame() == false) //if player isn't seen
        {
            waitToPatrolTime = waitToPatrolTime + Time.deltaTime; //counts up the time to patrol timer
        }
        else
        {
            waitToPatrolTime = 0f; //if player is seen in the frame then reset the timer
            readyToPatrol = false; // and reset the ready to patrol to false
        }
        
        if (waitToPatrolTime >= waitToPatrolMaxTime) //if wait to patrol time reaches the max then switch the readytopatrol bool to true
        {
            readyToPatrol = true;
        }
    }

    public override void OnStateEnd()
    {
        agent.isStopped = false; //at the end of the state makes sure the worm isn't stopped
        attackState.isUnderground = true; //and the worm is underground
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}