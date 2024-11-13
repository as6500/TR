using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private int speed = 0;
    [SerializeField] private CameraType type;
    private enum CameraType {FollowPlayerFixed, FollowPlayerDelay, FollowPlayerMouse};

    [Header("Camera Followed Objects")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform followMouseObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if(type == CameraType.FollowPlayerFixed)
        {
            FollowPlayerFixed();
        }
        else if (type == CameraType.FollowPlayerDelay)
        {
            FollowPlayerDelay();
        }
        else if (type == CameraType.FollowPlayerMouse)
        {
            FollowPlayerMouse();
        }
    }

    private void FollowPlayerFixed()
    {
        gameObject.transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void FollowPlayerDelay()
    {
        Vector3 direction = player.position - transform.position;
        Vector3 directionNormalized = direction.normalized;
        Vector3 newDirection = directionNormalized * speed * Time.fixedDeltaTime;

        transform.position = new Vector3(transform.position.x + newDirection.x, transform.position.y + newDirection.y, transform.position.z);
    }

    private void FollowPlayerMouse()
    {
        Vector3 direction = followMouseObject.position - transform.position;
        Vector3 directionNormalized = direction.normalized;
        Vector3 newDirection = directionNormalized * speed * Time.fixedDeltaTime;

        transform.position = new Vector3(transform.position.x + newDirection.x, transform.position.y + newDirection.y, transform.position.z);
    }
}
