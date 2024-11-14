using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : Singleton<MovementScript>
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float velocityModifier;
    [SerializeField] private float sprintMultiplier;
    

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
        
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            myRigidbody.velocity = new Vector2
            (
                Input.GetAxis("Horizontal") * movementSpeed,
                Input.GetAxis("Vertical") * movementSpeed
            );
        }
    }
}
