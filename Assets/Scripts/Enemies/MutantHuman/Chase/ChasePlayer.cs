using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class ChasePlayer : MonoBehaviour
{
    [Header("AI Agent")]
    [SerializeField] private NavMeshAgent agent;
    private Vector3 idlePosition;

    [Header("Object to Follow")]
    [SerializeField] private Transform player;

    [Header("State Configurations")]
    [SerializeField] private MutantHumanState state;
    [SerializeField] private bool chasing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = GetComponent<MutantHumanState>();
        idlePosition = transform.position;
        chasing = false;
    }

    private void Update()
    {
        if (chasing)
        {
            ActivOrDeactivChaseState(); //Updatedasfiags
        }
    }

    public void ActivOrDeactivChaseState()
    {
        if (state.GetCurrentState() == State.Chase)
        {
            chasing = true;
            agent.SetDestination(player.position);
        }
        else if (state.GetCurrentState() == State.Idle)
        {
            chasing = false;
            agent.SetDestination(idlePosition);
        }
    }
}
