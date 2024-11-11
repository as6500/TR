using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float velocityModifier = 4.0f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // if shift is held apply the SprintMultiplier
        float movementSpeed = velocityModifier;

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed *= sprintMultiplier;
        }
        
        myRigidbody.velocity = new Vector2
            (
                Input.GetAxis("Horizontal") * movementSpeed,
                Input.GetAxis("Vertical") * movementSpeed
            );
        Debug.Log("your speed is"+ movementSpeed);
    }
}
