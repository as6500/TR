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
        Debug.Log(playerInAttackRange);
        //Debug.Log(hasSeenPlayerThisFrame);
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > visionDistance)
        {
            hasSeenPlayerThisFrame = false;
            OnPlayerNotDetected();
            return;
        }
        else if (distanceToPlayer < visionDistance)
        {
            hasSeenPlayerThisFrame = true;
            OnPlayerDetected();
            return;
        }
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag(tagObjectToFind))
    //     {
    //         hasSeenPlayerThisFrame = true;
    //         OnPlayerDetected();
    //     }
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag(tagObjectToFind))
    //     {
    //         hasSeenPlayerThisFrame = false;
    //         OnPlayerNotDetected();
    //     }
    // }
    
    private void OnPlayerDetected()
    {
        //Debug.Log("Player detected!!!");
    }

    private void OnPlayerNotDetected()
    {
        //Debug.Log("Player not detected.");
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
            Debug.Log("inside attack range");
        }
    }

    // detect when player exits the attack range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = false;
            Debug.Log("outside attack range");
        }
    }
    
    // private void OnDrawGizmosSelected()
    // {
    //      Gizmos.color = Color.red; //draws the attack range circumference 
    //      Gizmos.DrawWireSphere(transform.position, attackState.attackRange);
    //     
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, visionDistance);
    //     
    //      Vector3 forward = transform.right;
    //      float halfAngle = viewConeAngle / 2 * Mathf.Deg2Rad;
    //     
    //      Vector3 coneLeft = Quaternion.Euler(0, 0, viewConeAngle / 2) * forward * visionDistance;
    //      Vector3 coneRight = Quaternion.Euler(0, 0, -viewConeAngle / 2) * forward * visionDistance;
    //     
    //      Gizmos.color = Color.yellow;
    //      Gizmos.DrawLine(transform.position, transform.position + coneLeft);
    //      Gizmos.DrawLine(transform.position, transform.position + coneRight);
    // }
    
}
