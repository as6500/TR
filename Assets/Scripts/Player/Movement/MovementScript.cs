using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : Singleton<MovementScript>
{
    [Header("Velocity")]
    [SerializeField] private float velocityModifier;
    [SerializeField] private float walkVelocity;
    [SerializeField] private float sprintVelocity;

    [Header("Input Actions References")]
    [SerializeField] private InputActionReference walkAction;
    [SerializeField] private InputActionReference sprintAction;

    private Vector2 currentInput = Vector2.zero;

    private Rigidbody2D myRigidbody;

    private void OnEnable()
    {
        walkAction.action.Enable();
        sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        walkAction.action.Disable();
        sprintAction.action.Disable();
    }


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        velocityModifier = walkVelocity;

        walkAction.action.started += OnWalkStarted;
        walkAction.action.performed += OnWalkPerformed;
        walkAction.action.canceled += OnWalkCancelled;


        sprintAction.action.performed += OnSprintPerformed;
        sprintAction.action.canceled += OnSprintCancelled;
    }

    private void OnWalkStarted(InputAction.CallbackContext callbackContext)
    {
    }

    private void OnWalkPerformed(InputAction.CallbackContext callbackContext)
    {
        currentInput = callbackContext.ReadValue<Vector2>();
    }

    private void OnWalkCancelled(InputAction.CallbackContext callbackContext)
    {
        currentInput = Vector2.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext callbackContext)
    {
        velocityModifier *= sprintVelocity;
    }

    private void OnSprintCancelled(InputAction.CallbackContext callbackContext)
    {
        velocityModifier = walkVelocity;
    }

    private void FixedUpdate()
    {
        if (myRigidbody != null) 
        {
            myRigidbody.velocity = currentInput * velocityModifier;
        }
    }
}
