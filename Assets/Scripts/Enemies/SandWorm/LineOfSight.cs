using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private string tagObjectToFind = "Player";
    [SerializeField] private float visionDistance = 3.0f;
    public bool hasSeenPlayerThisFrame = false;
    public bool playerInAttackRange;
    [SerializeField] private LayerMask detectionLayerMask;
    [SerializeField] private AttackState attackState;
    [SerializeField] private NewStateMachine stateMachine;
    private Transform playerTransform;
   
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(tagObjectToFind)?.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > visionDistance)
        {
            hasSeenPlayerThisFrame = false;
            return;
        }
        else if (distanceToPlayer < visionDistance)
        {
            hasSeenPlayerThisFrame = true;
            return;
        }
    }

    public bool HasSeenPlayerThisFrame()
    {
        return hasSeenPlayerThisFrame;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = true;
        }
    }

    // detect when player exits the attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = false;
        }
    }
}
