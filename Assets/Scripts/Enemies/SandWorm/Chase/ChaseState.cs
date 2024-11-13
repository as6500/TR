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
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LineOfSight lineOfSight;
    //[SerializeField] private AttackState attackState;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D enemyRb;
    private Rigidbody2D rb;
    private GameObject player;
    [SerializeField] public float speed;
    private float playerLostMaxTime = 3f; //max time that the player can not be seen for
    private float playerLostSinceTime = 0f; //counts the time since the player was last seen
    public bool enemyHasLostPlayer = false;
    
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player"); //finds the player by tag
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
        //attackState.isUnderground = true; //make sure the worm is underground at the start of the state
        
    }

    public override void OnStateUpdate()
    {
        rb.velocity = Vector2.zero;
        agent.SetDestination(player.transform.position); //makes the worm chase the player
        if (lineOfSight.HasSeenPlayerThisFrame()) //if player is seen
        {
            ResetPlayerLostTimer(); //reset the timer
        }
        else //otherwise
        {
            playerLostSinceTime = playerLostSinceTime + Time.deltaTime; //count up the timer since last seen
            
            if (playerLostSinceTime >= playerLostMaxTime) //if the timer reaches the max
            {
                enemyHasLostPlayer = true; //declare that player is lost
            }
        }
    }

    public override void OnStateEnd()
    {
        ResetPlayerLostTimer(); //at the end of the state reset the timer
        //attackState.isUnderground = true; //and makes sure the worm is underground
    }
    private void ResetPlayerLostTimer()
    {
        playerLostSinceTime = 0f; //resets the timer
        enemyHasLostPlayer = false; //makes sure the enemy not knowing where the player is reset
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}