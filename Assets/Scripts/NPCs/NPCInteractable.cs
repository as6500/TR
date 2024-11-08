using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class interacting : UnityEvent { };

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private bool isInRange;
    [SerializeField] private interacting interacting;

    void Update()
    {
        if (isInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
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
