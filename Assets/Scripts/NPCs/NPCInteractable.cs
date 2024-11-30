using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class Interacting : UnityEvent { };

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private bool isInRange;
    [SerializeField] private Interacting interacting;
    [SerializeField] private TextMeshPro interactingText;

    void Update()
    {
        if (isInRange)
        {
	        interactingText.enabled = true;
	        interactingText.text = "E to Interact";
            if (Input.GetKeyDown(KeyCode.E))
                interacting.Invoke();
        }
        else
	        interactingText.enabled = false;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
            isInRange = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
            isInRange = false;
	}
}
