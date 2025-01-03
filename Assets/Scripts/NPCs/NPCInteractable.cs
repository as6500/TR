using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class Interacting : UnityEvent { };

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private bool isInRange;
    [SerializeField] public Interacting interacting;
    [SerializeField] public TextMeshPro interactingText;
    private bool canInteract = true;
    
    private void OnEnable()
    {
	    QuestManager.finishedQuest.AddListener(OnFinishedQuest);
    }

    private void OnDisable()
    {
	    QuestManager.finishedQuest.RemoveListener(OnFinishedQuest);
    }

    void Update()
    {
	    if (!canInteract)
	    {
		    interactingText.enabled = false;
		    return;
	    }
	    if (isInRange)
	    {
		    interactingText.enabled = true;
		    interactingText.text = "E to Interact";
		    if (Input.GetKeyDown(KeyCode.E))
			    interacting?.Invoke();
		    return;
	    }
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

	private void OnFinishedQuest(NPCInteractable npcInteractable)
	{
		if (npcInteractable == this)
		{
			interacting.RemoveAllListeners();
			canInteract = false;
		}
	}
}
