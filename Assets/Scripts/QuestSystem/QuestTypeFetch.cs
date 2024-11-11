using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestTypeFetch : MonoBehaviour
{
	[SerializeField] private bool isInRange;
	[SerializeField] private QuestScriptableObject quest;
	[SerializeField] private QuestManager manager;
	private Item item;
	private bool itemPickedUp;
	private void Start()
	{
		itemPickedUp = false;
		item = GetComponent<Item>();
		manager = FindFirstObjectByType<QuestManager>();
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
			int id = manager.currentItems.IndexOf(item);

			itemPickedUp = true;
			manager.typeFetch.RemoveAt(id);
			manager.currentItems.RemoveAt(id);
			Destroy(gameObject);
			Debug.Log("Item on inventory!");
		}
	}
}
