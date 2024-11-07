using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : StateBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    [SerializeField] public float speed;
    private Rigidbody2D enemyRb;
    private GameObject player;
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
        // spriteRenderer.color = Color.yellow;
        Vector2 vectorFromEnemyToPlayer = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(vectorFromEnemyToPlayer * speed);
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}