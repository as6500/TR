using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles the behavior of the quests: if it's fetch, it handles which object to fetch and if it is on the player's "inventory"
// if it's resource, it handles which object to count as the matching to the one in the quest (drops from mobs)
// if it's find a location, it handles if the player is really on the correct area for the quest to count and advance to another step

public class Quest : MonoBehaviour
{
	private bool objectTaken;

	private string state;
	private string state1 = "requirementNotMet";
	private string state2 = "active";
	private string state3 = "completed";


	public UnityEvent getQuest;
	[SerializeField] private GameObject exclamationPoint;
	[SerializeField] private QuestManager manager;
	[SerializeField] private FetchQuestInfo fetchQuestInfo;

	private void Start()
	{
		objectTaken = false;
		manager = GetComponent<QuestManager>();
		state = state1;
		exclamationPoint.SetActive(true);
	}

	private void OnTriggerEnter2D (Collider2D collision) //NPC
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			getQuest.Invoke();
			state = state2;
			exclamationPoint.SetActive(false);
		}
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			int i = 0;
			fetchQuestInfo.DisplayQuestQuest(i);
			manager.AdvanceQuest.Invoke();
		}
	}
	// if player gets the item, quest updates, passes to another step.
	// if player gets the item, counter increments.

}
