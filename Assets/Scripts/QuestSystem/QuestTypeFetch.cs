using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestTypeFetch : MonoBehaviour
{
	[SerializeField] private bool isInRange;
	[SerializeField] private Interacting interacting;
	[SerializeField] private QuestScriptableObject quest;
	private bool itemPickedUp;
	private void Start()
	{
		gameObject.SetActive(false);
		itemPickedUp = false;
	}

	private void Update()
	{
		if (isInRange == true)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				interacting.Invoke();
				GetItem();
			}
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

	public void GetItem()
	{
		quest.typeCount++;
		itemPickedUp = true;
		Destroy(gameObject);
		Debug.Log("Item on inventory!");
	}

	public bool ItemPickedUp()
	{
		return itemPickedUp;
	}
}
