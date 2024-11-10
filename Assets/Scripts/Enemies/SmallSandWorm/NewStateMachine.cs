using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewStateMachine : MonoBehaviour
{
    [SerializeField] private List<StateBehaviour> stateBehaviours = new List<StateBehaviour>();

    [SerializeField] private int defaultState = 0;

    private StateBehaviour currentState = null;
    [SerializeField] private LineOfSight2D lineOfSight;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private PlayerLostState playerLostState;

    bool InitializeStates()
    {
        for (int i = 0; i < stateBehaviours.Count; ++i)
        {
            StateBehaviour stateBehaviour = stateBehaviours[i];
            if (stateBehaviour && stateBehaviour.InitializeState())
            {
                stateBehaviour.AssociatedStateMachine = this;
                continue;
            }

            Debug.Log($"StateMachine On {gameObject.name} has failed to initialize the state {stateBehaviours[i]?.GetType().Name}!");
            return false;
        }

        return true;
    }

    void Start()
    {
        if (!InitializeStates())
        {
            // Stop This class from executing
            this.enabled = false;
            return;
        }
        
        if (stateBehaviours.Count > 0)
        {
            int firstStateIndex = defaultState < stateBehaviours.Count ? defaultState : 0;

            currentState = stateBehaviours[firstStateIndex];
            currentState.OnStateStart();
        }
        else
        {
            Debug.Log($"StateMachine On {gameObject.name} has no state behaviours associated with it!");
        }
    }

    void Update()
    {
        currentState.OnStateUpdate();
        
        //switch to chase state by key
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[1];
            currentState.OnStateStart();
            currentState.OnStateUpdate();
        }
        //switch to patrol state by key
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[0];
            currentState.OnStateStart();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(currentState);
        }
        //switch to playerlost state by key
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[2];
            currentState.OnStateStart();
        }
        
        //if player is declared lost while chasing then switch to playerLost state
        if (chaseState.enemyHasLostPlayer && currentState == stateBehaviours[1])
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[2];
            currentState.OnStateStart();
        }
        
        //if player is seen while in patrol mode then switch to chase state
        if (currentState ==  stateBehaviours[0] && lineOfSight.HasSeenPlayerThisFrame())
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[1];
            currentState.OnStateStart();
        }
        
        //switch from lost to patrol
        if (playerLostState.readyToPatrol && currentState == stateBehaviours[2])
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[0];
            currentState.OnStateStart();
        }

        if (chaseState.enemyHasLostPlayer && currentState != stateBehaviours[2] && !lineOfSight.HasSeenPlayerThisFrame())
        {
            Debug.Log("Player lost again, switching to PlayerLostState.");
            currentState.OnStateEnd();
            currentState = stateBehaviours[2];
            currentState.OnStateStart();
        }


        int newState = currentState.StateTransitionCondition();
        if (IsValidNewStateIndex(newState))
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[newState];
            currentState.OnStateStart();
        }
    }

    public bool IsCurrentState(StateBehaviour stateBehaviour)
    {
        return currentState == stateBehaviour;
    }

    public void SetState(int index)
    {
        if (IsValidNewStateIndex(index))
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[index];
            currentState.OnStateStart();
        }
    }

    private bool IsValidNewStateIndex(int stateIndex)
    {
        return stateIndex < stateBehaviours.Count && stateIndex >= 0;
    }

    public StateBehaviour GetCurrentState()
    {
        return currentState;
    }
    
    
}