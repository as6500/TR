using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
public class OnStateChanged : UnityEvent { };

public enum State { Idle, Chase, Attack }


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

    private void SawPlayerChangeState() 
    {
        if (currentState == State.Idle)
        {
            currentState = State.Chase;
        }
        else if (currentState == State.Chase)
        {
            currentState = State.Attack;
        }
    }

    private void LostPlayerChangeState()
    {
        if (currentState == State.Chase)
        {
            currentState = State.Idle;
        }
        else if (currentState == State.Attack)
        {
            currentState = State.Chase;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            SawPlayerChangeState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            LostPlayerChangeState();
        }
    }
}
