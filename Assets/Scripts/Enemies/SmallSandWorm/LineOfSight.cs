using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight2D : MonoBehaviour
{
    [SerializeField] private string tagObjectToFind = "Player";
    [SerializeField] private float visionDistance = 5.0f;
    [SerializeField] private float viewConeAngle = 45.0f;
    [SerializeField] private LayerMask detectionLayerMask;
    [SerializeField] private SmallSandwormAttackState smallSandwormAttackState;

    private Transform playerTransform;
    private bool hasSeenPlayerThisFrame = false;
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag(tagObjectToFind)?.GetComponent<Transform>();
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            CheckPlayerInLineOfSight();
            //Debug.Log(hasSeenPlayerThisFrame);
        }
    }

    private void CheckPlayerInLineOfSight()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > visionDistance)
        {
            hasSeenPlayerThisFrame = false;
            OnPlayerNotDetected();
            return;
        }

        float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

        if (angleToPlayer <= viewConeAngle / 2)
        {
            RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, 0.5f, directionToPlayer, distanceToPlayer, detectionLayerMask);
            Debug.DrawLine(transform.position, playerTransform.position, Color.white, 0.1f);

            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag(tagObjectToFind))
                {
                    hasSeenPlayerThisFrame = true;
                    OnPlayerDetected();
                    return;
                }
            }
            else
            {
                Debug.Log("Radius did not hit anything.");
            }
        }
        else
        {
            //Debug.Log("Player is outside the view cone angle.");
        }

        hasSeenPlayerThisFrame = false;
        OnPlayerNotDetected();
    }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallSandwormAttackState.attackRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        Vector3 forward = transform.right;
        float halfAngle = viewConeAngle / 2 * Mathf.Deg2Rad;

        Vector3 coneLeft = Quaternion.Euler(0, 0, viewConeAngle / 2) * forward * visionDistance;
        Vector3 coneRight = Quaternion.Euler(0, 0, -viewConeAngle / 2) * forward * visionDistance;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + coneLeft);
        Gizmos.DrawLine(transform.position, transform.position + coneRight);
    }
    
}
