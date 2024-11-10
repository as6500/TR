using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ChaseState : StateBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    [SerializeField] public float speed;
    private Rigidbody2D enemyRb;
    private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LineOfSight2D lineOfSight;
    private float playerLostMaxTime = 3f;
    private float playerLostSinceTime = 0f;
    public bool enemyHasLostPlayer = false;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        //Debug.Log("Chase state started");
        spriteRenderer.color = Color.yellow;
        ResetPlayerLostTimer();
        // playerLostSinceTime = 0f;
        // enemyHasLostPlayer = false;
    }

    public override void OnStateUpdate()
    {
        //Debug.Log("Chase state update");
        //Debug.Log("hi was player seen?" + lineOfSight.HasSeenPlayerThisFrame());
        agent.SetDestination(player.transform.position);
        if (lineOfSight.HasSeenPlayerThisFrame())
        {
            ResetPlayerLostTimer();
            // playerLostSinceTime = 0f; //stays zero if player is in sight
            // enemyHasLostPlayer = false;
        }
        else
        {
            playerLostSinceTime = playerLostSinceTime + Time.deltaTime;
            
            if (playerLostSinceTime >= playerLostMaxTime)
            {
                enemyHasLostPlayer = true;
            }
        }
    }

    public override void OnStateEnd()
    {
        ResetPlayerLostTimer();
    }
    private void ResetPlayerLostTimer()
    {
        playerLostSinceTime = 0f;
        enemyHasLostPlayer = false;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}