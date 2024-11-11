using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
public class OnStateChanged : UnityEvent { };

public enum State { Idle, Chase }


public class MutantHumanState : MonoBehaviour
{
    [SerializeField] private OnStateChanged onStateChangedEvent;
    [SerializeField] private State currentState;
    private State changeFromState;


    private void Start()
    {
        currentState = State.Idle;
    }

    private bool StateChanged()
    {
        if (currentState == changeFromState)
        {
            return false;
        }

        changeFromState = currentState;

        return true;
    }

    private void Update()
    {
        if (StateChanged())
        {
            onStateChangedEvent.Invoke();
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}
