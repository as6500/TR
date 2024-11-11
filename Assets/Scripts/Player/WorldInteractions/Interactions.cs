using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour, IInteractable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interact = collision.GetComponent<IInteractable>();

        if(interact != null)
            interact.Interact(gameObject);
    }

    public virtual void Interact(GameObject instigator)
    {
        Debug.Log($"{instigator} is interacting with {gameObject}");
    }
}
