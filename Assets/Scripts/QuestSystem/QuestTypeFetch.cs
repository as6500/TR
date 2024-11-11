using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestTypeFetch : MonoBehaviour
{
	[SerializeField] private bool isInRange;
	[SerializeField] private QuestScriptableObject quest;
	private Item item;
	private bool itemPickedUp;
	private void Start()
	{
		itemPickedUp = false;
		item = GetComponent<Item>();
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
		if (isInRange)
		{
			itemPickedUp = true;
			Destroy(gameObject);
			Debug.Log("Item on inventory!");
		}
	}
}
