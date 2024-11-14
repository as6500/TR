using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : Singleton<QuestManager>
{
	public static readonly UnityEvent OnQuestAction = new(); //readonly so you can't change it
	public QuestData activeQuest;
	public QuestState activeQuestState;
	[SerializeField] private QuestSystemUI questText;
	public List<QuestNPC> npcs = new();
	[SerializeField] private QuestTypeLocation questTypeLocation;
	[SerializeField] private GameObject location;
	[SerializeField] private GameObject item;
	public Item interactedItem;
	private int activeQuestItemCounter;
	
	public void Start()
	{
		activeQuestState = QuestState.Pending;
		activeQuestItemCounter = 0;
		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);
		npcs[2].SetIcon(IconType.None);
		npcs[3].SetIcon(IconType.None);
		
		OnQuestAction.AddListener(CompleteQuest);
	}
	
	public void AcceptQuest()
	{
		if (activeQuestState != QuestState.Pending) return;
		activeQuestState = QuestState.Active;

		npcs[activeQuest.questNPCId].SetIcon(IconType.None);
		switch (activeQuest.type)
		{
			case QuestType.Fetch:
				AcceptFetchQuest();
				break;
			case QuestType.Resource:
				AcceptResourceQuest();
				break;
			case QuestType.Locate:
				AcceptLocateQuest();
				break;
			default:
				throw new ArgumentOutOfRangeException(); //in case it's not those 3, it gives an error
		}
	}

	public void CompleteQuest()
	{
		if (activeQuestState != QuestState.Active) return;
		switch (activeQuest.type)
		{
			case QuestType.Fetch:
				ExecuteFetchQuest();
				break;
			case QuestType.Resource:
				ExecuteResourceQuest();
				break;
			case QuestType.Locate:
				ExecuteLocateQuest();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
	public void OntoNextQuest()
	{
		if (activeQuestState != QuestState.Completed) 
			return;
		
		npcs[activeQuest.questNPCId].SetIcon(IconType.None);
		questText.TextOfQuest().enabled = false;
		
		if (activeQuest.questNPCId == npcs.Count - 1) 
			return;
		
		activeQuestItemCounter = 0;
		activeQuest = activeQuest.nextQuest;
		activeQuestState = QuestState.Pending;
			
		npcs[activeQuest.questNPCId].SetIcon(IconType.ExclamationPoint);
	}

	private void AcceptFetchQuest()
	{
		int questParam = activeQuest.typeParam;
		string questName = activeQuest.displayName;
		string questItemName = activeQuest.typeName;
		int questCount = activeQuest.typeCount;
		
		for (int i = 0; i < questCount; i++)
		{
			Vector3 randomPosition = transform.position + new Vector3(Random.Range(2f, 6f), Random.Range(3f, 4f), 0);
			GameObject tempItem = Instantiate(item, randomPosition, Quaternion.identity); //create temporary item
			Item newItem = tempItem.GetComponent<Item>();
			newItem.SetUpItem(questParam);
		}
		questText.DisplayFetchQuestText(questName, questCount, questItemName, activeQuestItemCounter);
	}

	private void AcceptResourceQuest()
	{
		//Code for Resource Quests (Tom�s)
	}
	
	private void AcceptLocateQuest()
	{
		string questName = activeQuest.displayName;
		string questItemName = activeQuest.typeName;
		
		Vector3 randomPosition = transform.position + new Vector3(Random.Range(2f, 6f), Random.Range(3f, 4f), 0);
		GameObject tempLocation = Instantiate(location, randomPosition, Quaternion.identity);
		questTypeLocation = tempLocation.GetComponent<QuestTypeLocation>();
		questText.DisplayLocateQuestText(questName, questItemName);
	}
	
	private void ExecuteFetchQuest()
	{
		string questName = activeQuest.displayName; //name of the quest itself
		string questNPCName = activeQuest.questNPCName; //name of the npc that has the quest/the npc where the player needs to deliver
		int questItemId = activeQuest.typeParam; //id of the item that the quest needs
		string questItemName = activeQuest.typeName; // name of the item that the quest needs
		int questItemCount = activeQuest.typeCount; // how many items the quest needs
		int currentItem = interactedItem.id; //id of the item that is being interacted with

		if (questItemId != currentItem) 
			return;
		
		if (activeQuestItemCounter < questItemCount)
		{
			activeQuestItemCounter++;
			questText.DisplayFetchQuestText(questName, questItemCount, questItemName, activeQuestItemCounter);
		}

		if (activeQuestItemCounter != questItemCount) 
			return;
		
		questText.DisplayFetchDeliverText(questName, questNPCName);
		activeQuestState = QuestState.Completed;
		npcs[activeQuest.questNPCId].SetIcon(IconType.InterrogationPoint);
	}

	private void ExecuteLocateQuest()
	{
		string questName = activeQuest.displayName;
		string questNPCName = activeQuest.questNPCName;

		if (!questTypeLocation.OnLocation()) 
			return;
		
		questText.DisplayLocateDeliverText(questName, questNPCName);
		activeQuestState = QuestState.Completed;
		Destroy(questTypeLocation.gameObject);
		npcs[activeQuest.questNPCId].SetIcon(IconType.InterrogationPoint);
	}

	private void ExecuteResourceQuest()
	{
		
	}
}
