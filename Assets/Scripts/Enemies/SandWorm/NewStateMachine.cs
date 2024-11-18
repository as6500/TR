using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewStateMachine : MonoBehaviour
{
    [SerializeField] private List<StateBehaviour> stateBehaviours = new List<StateBehaviour>();
    [SerializeField] private int defaultState = 0;
    private StateBehaviour currentState = null;
    [SerializeField] private LineOfSight lineOfSight;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private PlayerLostState playerLostState;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject SmallSandworm;
    [SerializeField] private AttackState attackState;
    
    bool InitializeStates()
    {
        //goes through every state and initialises it
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
    //initialises the states, starts the default state (the one in the zero position / patrol state)
    {
        if (!InitializeStates())
        {
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
            Debug.Log($"StateMachine On {gameObject.name} has no state behaviours associated with it!");
    }

    void Update()
    {
        currentState.OnStateUpdate();
        
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

        //if the enemy has lost the player and the current state isnt player lost and the player wasnt seen
        if (chaseState.enemyHasLostPlayer && currentState != stateBehaviours[2] && !lineOfSight.HasSeenPlayerThisFrame())
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[2];
            currentState.OnStateStart();
        }

        //currentstate is lost and player is seen switch to chase
        if (currentState == stateBehaviours[2] && lineOfSight.HasSeenPlayerThisFrame())
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[1];
            currentState.OnStateStart();
        }
        
        //if the enemy is not in attacking distance and the current state is attacking then switch to chase state
         if (lineOfSight.playerInAttackRange  && currentState == stateBehaviours[1])
         {
             currentState.OnStateEnd();
             currentState = stateBehaviours[3];
             currentState.OnStateStart();
         }
        
         //if the enemy is attacking and the player leaves the attack range switch to chase state
         if (!lineOfSight.playerInAttackRange && currentState == stateBehaviours[3])
         {
             currentState.OnStateEnd();
             currentState = stateBehaviours[1];
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
    
    //checks if the number of states is valid
    private bool IsValidNewStateIndex(int stateIndex)
    {
        return stateIndex < stateBehaviours.Count && stateIndex >= 0;
    }
}