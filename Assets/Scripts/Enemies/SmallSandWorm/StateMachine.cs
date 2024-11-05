using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private List<StateBehaviour> stateBehaviours;
    [SerializeField] private int defaultState = 0;
    private StateBehaviour currentState;

    void Start()
    {
        if (stateBehaviours == null || stateBehaviours.Count == 0 || !InitializeStates())
        {
            Debug.LogError($"StateMachine on {gameObject.name} failed to initialize.");
            //enabled = false;
            return;
        }

        SetState(defaultState); //calls default state at start if it doesnt encounter errors 
    }

    void Update()
    {
        if (currentState == null)
        {
            return;
        }

        currentState.OnStateUpdate();

        int newStateIndex = currentState.StateTransitionCondition();
        if (IsValidStateIndex(newStateIndex))
        {
            SetState(newStateIndex);
        }
    }

    private bool InitializeStates()
    {
        foreach (var state in stateBehaviours)
        {
            if (state != null && state.InitializeState())
            {
                state.AssociatedStateMachine = this;
            }
            else
            {
                Debug.LogError($"Failed to initialize state {state?.GetType().Name} on {gameObject.name}.");
                return false;
            }
        }
        return true;
    }

    private void SetState(int index)
    {
        if (!IsValidStateIndex(index)) return;

        currentState?.OnStateEnd();
        currentState = stateBehaviours[index];
        currentState?.OnStateStart();
    }

    private bool IsValidStateIndex(int index) => index >= 0 && index < stateBehaviours.Count;
}