using UnityEngine;

public class PatrolState : StateBehaviour
//this is a script that makes it so an enemy moves from point A to point B, you can set point A and point B on unity as well as speed
{
    public Transform pointA;
    public Transform pointB; 
    public float speed = 2f; 
    private Rigidbody2D rb;
    private bool fromAtoB; //if yes then enemy is moving from A to B, if false then it is on its way back

    void Start()
    {
        
    }

    void FixedUpdate()
    {

    }

    public override bool InitializeState()
    {
        rb = GetComponent<Rigidbody2D>();
        fromAtoB = true;
        return true;
    }

    public override void OnStateStart()
    {
        rb.velocity = Vector2.zero;
    }

    public override void OnStateUpdate()
    {
        // if fromAtoB is on then head to point B, otherwise point A
        Vector2 targetPosition;
        if (fromAtoB == true)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }
        
        //make a vector between the target position and the current position and then normalize it
        Vector2 direction = (targetPosition - rb.position).normalized;
 
        //multiply the vector by the speed that can be customised in unity
        rb.velocity = direction * speed;

        //switch directions when the enemy comes close enough to the point
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            fromAtoB = !fromAtoB;
        }
    }

    public override void OnStateEnd()
    {
        rb.velocity = Vector2.zero;
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}

