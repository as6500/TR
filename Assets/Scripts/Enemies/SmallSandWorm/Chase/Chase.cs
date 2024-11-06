using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("State Machine/Chase")]
public class Chase : StateBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override bool InitializeState()
    {
        //this one runs
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("Chase state started");
        spriteRenderer.color = Color.yellow;
    }

    public override void OnStateUpdate()
    {
        // Debug.Log("Chase state started");
        // spriteRenderer.color = Color.yellow;
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {
        return -1;
    }
}
