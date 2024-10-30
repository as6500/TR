using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactableObject = collision.GetComponent<IInteractable>();

        if(interactableObject != null)
            interactableObject.Interact(gameObject);
    }
}
