using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private int VelocityModifier = 4;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        myRigidbody.velocity = new Vector2
            (
                Input.GetAxis("Horizontal") * VelocityModifier,
                Input.GetAxis("Vertical") * VelocityModifier
            );
    }
}
