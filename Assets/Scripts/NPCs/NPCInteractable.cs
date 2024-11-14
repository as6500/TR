using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class Interacting : UnityEvent { };

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private bool isInRange;
    [SerializeField] private Interacting interacting;

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
                interacting.Invoke();
        }
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
