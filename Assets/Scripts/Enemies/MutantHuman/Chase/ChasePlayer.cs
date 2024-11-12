using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class ChasePlayer : MonoBehaviour
{
    [Header("AI Agent")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int secondsWaitingToFindPlayer;
    [SerializeField] private Vector3 idlePosition;

    [Header("Object to Follow")]
    [SerializeField] private Transform player;

    [Header("State Configurations")]
    [SerializeField] private MutantHumanState state;
    [SerializeField] private bool chasing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
        state = GetComponent<MutantHumanState>();

        chasing = false;
    }

    private void Update()
    {
        if (chasing)
        {
            UpdateChaseState(); 
        }
    }

    public void UpdateChaseState()
    {
        if (state.GetCurrentState() == State.Chase)
        {
            agent.SetDestination(player.position);   
        }
    }

    public void SetChaseState()
    {
        if (state.GetCurrentState() == State.Chase)
        {
            chasing = true;
            StopCoroutine(LookingForPlayer());
        }
        else if (state.GetCurrentState() == State.Idle)
        {
            chasing = false;
            StartCoroutine(LookingForPlayer());
        }
    }

    private IEnumerator LookingForPlayer()
    {
        yield return new WaitForSeconds(secondsWaitingToFindPlayer);
        agent.SetDestination(idlePosition);
    }
}
