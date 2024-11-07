using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles the behavior of the quests: if it's fetch, it handles which object to fetch and if it is on the player's "inventory"
// if it's resource, it handles which object to count as the matching to the one in the quest (drops from mobs)
// if it's find a location, it handles if the player is really on the correct area for the quest to count and advance to another step

//states: 0 - requirements_not_met; 1 - not active; 2 - active; 3 - completed; 4 - finished

public class Quest : MonoBehaviour
{
	private bool objectTaken;
	private string state;

	[SerializeField] private GameObject exclamationPointQuest;
	[SerializeField] private QuestManager manager;
	[SerializeField] private FetchQuestInfo fetchQuestInfo;
	[SerializeField] private QuestStep questStep;
	[SerializeField] private QuestDelivery questDelivery;
	[SerializeField] private string[] questStates;

	[SerializeField] private Sprite exclamationPoint;
	[SerializeField] private Sprite questionMark;
	public QuestManager.QuestState thestate;

	private void Start()
	{
		objectTaken = false;
		state = questStates[1]; //the player hasn't picked up the quest but has the requirements for it to be available
		exclamationPointQuest.SetActive(true);
		exclamationPointQuest.GetComponent<SpriteRenderer>().sprite = exclamationPoint;
	}

	private void OnTriggerEnter2D (Collider2D collision) //accepting quest
	{
		if (collision.gameObject.CompareTag("Player") && state == questStates[1])
		{
			manager.StartQuest();
			state = questStates[2]; //player picked up the quest
			exclamationPointQuest.SetActive(false);
		}

		if (collision.gameObject.CompareTag("Player") && state == questStates[3])
		{
			exclamationPointQuest.SetActive(false);
			questDelivery.DeliveryTheQuest();
			state = questStates[4]; //finished quest
		}
	}

	public void UpdateQuestIcon()
	{
		exclamationPointQuest.SetActive(true);
		exclamationPointQuest.GetComponent<SpriteRenderer>().sprite = questionMark;
		state = questStates[3]; //completed quest (all steps), need to deliver it to the quest giver
	}

	public string CurrentQuestState(int i)
	{
		return questStates[i];
	}
}
