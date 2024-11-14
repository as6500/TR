using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class QuestTypeFetch : MonoBehaviour
{
	[SerializeField] private bool isInRange;
	[SerializeField] private QuestData quest;
	[SerializeField] private QuestManager manager;
	private Item item;
	private bool itemPickedUp;
	private void Start()
	{
		itemPickedUp = false;
		item = GetComponent<Item>();
		manager = FindFirstObjectByType<QuestManager>();
	}

	private void Update()
	{
		if (isInRange && Input.GetKeyDown(KeyCode.E))
		{
			QuestManager.OnQuestAction.Invoke();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isInRange = true;
			manager.interactedItem = item;
			
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isInRange = false;
			if (manager.interactedItem == item)
				manager.interactedItem = null;
		}

	}

	public void GetItem()
	{
		if (isInRange)
		{
			int id = manager.GetCurrentItems().IndexOf(item);

			itemPickedUp = true;
			manager.GetItemScript().RemoveAt(id);
			manager.GetCurrentItems().RemoveAt(id);
			Destroy(gameObject);
			Debug.Log("Item on inventory!");
		}
	}
}
