using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewStateMachine : MonoBehaviour
{
    [SerializeField] private List<StateBehaviour> stateBehaviours = new List<StateBehaviour>();

    [SerializeField] private int defaultState = 0;

    private StateBehaviour currentState = null;

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

            Debug.Log($"StateMachine On {gameObject.name} has failed to initalize the state {stateBehaviours[i]?.GetType().Name}!");
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
            Debug.Log($"StateMachine On {gameObject.name} is has no state behaviours associated with it!");
        }
    }

    void Update()
    {
        currentState.OnStateUpdate();
        //switch to chase state
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentState.OnStateEnd();
            currentState = stateBehaviours[1];
            currentState.OnStateStart();
            currentState.OnStateUpdate();
        }
        //switch to patrol state
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