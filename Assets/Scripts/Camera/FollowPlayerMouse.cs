using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FollowPlayerMouse : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private int distanceAhead = 2;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        ChangePosition();
    }

    private void ChangePosition()
    {
        Vector3 newPosition = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position) / 2;
        Vector3 newPosNormalized = newPosition.normalized * distanceAhead;
        transform.position = new Vector3(player.position.x + newPosNormalized.x, player.position.y + newPosNormalized.y, player.position.z);
    }
}
