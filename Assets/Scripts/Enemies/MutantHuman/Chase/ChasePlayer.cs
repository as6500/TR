using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class ChasePlayer : MonoBehaviour
{
    [Header("AI Agent")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int secondsWaitingToFindPlayer;
    [SerializeField] private Vector3 idlePosition;

    [Header("Animations")]
    [SerializeField] private Animator anim;

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

        chasing = false;

        StartCoroutine(BugWithPositionFix());

        agent.enabled = true;
    }

    private IEnumerator BugWithPositionFix()
    {
        yield return new WaitForSeconds(1);

        agent.nextPosition = transform.position;

        agent.enabled = true;
    }

    private void Update()
    {
        if (chasing)
        {
            UpdateChaseState();
            ChangeAnimSide();
        }
    }



    public void UpdateChaseState()
    {
        if (state.GetCurrentState() == State.Chase)
        {
            agent.SetDestination(player.position);
        }
    }

    private void ChangeAnimSide()
    {
        if (agent.velocity.x == 0 && agent.velocity.y == 0)
        {
            return;
        }

        if (agent.velocity.x >= 0 && agent.velocity.y >= 0)
        {
            if(agent.velocity.x > 0)
            {
                if (agent.velocity.y > 0)
                {
                    if (agent.velocity.x > agent.velocity.y)
                    {
                        anim.SetTrigger("side_right");
                    }
                    else
                    {
                        anim.SetTrigger("back");
                    }
                }
                else
                {
                    anim.SetTrigger("side_right");
                }
            }
            else
            {
                anim.SetTrigger("back");
            }
        }
        else if (agent.velocity.x <= 0 && agent.velocity.y <= 0)
        {
            if (agent.velocity.x < 0)
            {
                if (agent.velocity.y < 0)
                {
                    if (agent.velocity.x < agent.velocity.y)
                    {
                        anim.SetTrigger("side_left");
                    }
                    else
                    {
                        anim.SetTrigger("front");
                    }
                }
                else
                {
                    anim.SetTrigger("side_left");
                }
            }
            else
            {
                anim.SetTrigger("front");
            }
        }
        else if (agent.velocity.x >= 0 && agent.velocity.y <= 0)
        {
            if (agent.velocity.x > 0)
            {
                if (agent.velocity.y < 0)
                {
                    if (agent.velocity.x > agent.velocity.y * -1)
                    {
                        anim.SetTrigger("side_right");
                    }
                    else
                    {
                        anim.SetTrigger("front");
                    }
                }
                else
                {
                    anim.SetTrigger("side_right");
                }
            }
            else
            {
                anim.SetTrigger("front");
            }
        }
        else if (agent.velocity.x <= 0 && agent.velocity.y >= 0)
        {
            if (agent.velocity.x < 0)
            {
                if (agent.velocity.y > 0)
                {
                    if (agent.velocity.x * -1 > agent.velocity.y)
                    {
                        anim.SetTrigger("side_left");
                    }
                    else
                    {
                        anim.SetTrigger("back");
                    }
                }
                else
                {
                    anim.SetTrigger("side_left");
                }
            }
            else
            {
                anim.SetTrigger("back");
            }
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

    public void SetStartPosition(Vector3 position)
    {
        idlePosition = position;
    }
}
