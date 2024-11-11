using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private Transform player;
    [SerializeField] private MutantHumanState state;
    [SerializeField] private bool chasing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = GetComponent<MutantHumanState>();
        idlePosition = GetComponent<Transform>();
        chasing = false;
    }

    private void Update()
    {
        if (chasing)
        {
            ActivOrDeactivChaseState();
        }
    }

    public void ActivOrDeactivChaseState()
    {
        if (state.GetCurrentState() == State.Chase)
        {
            chasing = true;
            agent.SetDestination(player.position);
        }
        else
        {
            chasing = false;
            agent.SetDestination(idlePosition.position);
        }
    }
}
