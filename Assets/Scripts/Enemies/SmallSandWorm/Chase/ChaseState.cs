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
    Vector2 moveDirection; 
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    public override bool InitializeState()
    {
        //this one runs
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("Chase state started");
        spriteRenderer.color = Color.yellow;
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Chase state update");
        agent.SetDestination(player.transform.position);
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}