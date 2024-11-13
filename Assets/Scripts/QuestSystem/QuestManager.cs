using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : MonoBehaviour
{
	public QuestData activeQuest;
	public QuestState activeQuestState;
	[SerializeField] private QuestSystemUI questText;
	public List<QuestNPC> npcs = new List<QuestNPC>();
	[SerializeField] private QuestTypeLocation questTypeLocation;
	[SerializeField] private GameObject location;
	[SerializeField] private GameObject item;
	public Item interactedItem;
	private List<Item> currentItems = new List<Item>();
	private List<QuestTypeFetch> itemScript = new List<QuestTypeFetch>();
	private int activeQuestItemCounter;
	
	public void Start()
	{
		activeQuestState = QuestState.Pending;
		activeQuestItemCounter = 0;

		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);
		npcs[2].SetIcon(IconType.None);
		npcs[3].SetIcon(IconType.None);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
			CompletingQuest();
	}

	public void AcceptQuest()
	{
		if (activeQuestState == QuestState.Pending)
		{
			activeQuestState = QuestState.Active;

			npcs[activeQuest.questNPCId].SetIcon(IconType.None);
			switch (activeQuest.type)
			{
				case QuestType.fetch:
					AcceptFetchQuest();
					break;
				case QuestType.resource:
					AcceptResourceQuest();
					break;
				case QuestType.locate:
					AcceptLocateQuest();
					break;
			}
		}
	}

	private void AcceptFetchQuest()
	{
		int questParam = activeQuest.typeParam;
		string questName = activeQuest.displayName;
		string questItemName = activeQuest.typeName;
		int questCount = activeQuest.typeCount;
		
		for (int i = 0; i < questCount; i++)
		{
			Vector3 randomPosition = transform.position + new Vector3(Random.Range(2f, 6f), Random.Range(4f, 5f), 0);
			GameObject tempItem = Instantiate(item, randomPosition, Quaternion.identity); //create temporary item
			Item newItem = tempItem.GetComponent<Item>();
			newItem.SetUpItem(questParam);
			currentItems.Add(newItem); //review this later
			itemScript.Add(tempItem.GetComponent<QuestTypeFetch>());//review this later
		}
		questText.DisplayQuestText(questName, questCount, questItemName, activeQuestItemCounter);
	}

	private void AcceptResourceQuest()
	{
		//Code for Resource Quests (Tom�s)
	}
	
	private void AcceptLocateQuest()
	{
		//Code for Locate Quests (Sofia)
		Vector3 randomPosition = transform.position + new Vector3(Random.Range(2f, 6f), Random.Range(4f, 5f), 0);
		GameObject tempLocation = Instantiate(location, randomPosition, Quaternion.identity);
		questTypeLocation = tempLocation.GetComponent<QuestTypeLocation>();
	}

	public void CompletingQuest()
	{
		switch (activeQuest.type)
		{
			case QuestType.fetch:
				ExecuteFetchQuest(activeQuest.questNPCId);
				break;
			case QuestType.resource:
				ExecuteResourceQuest();
				break;
			case QuestType.locate:
				ExecuteLocateQuest();
				break;
		}
		
	}

	public void OntoNextQuest()
	{
		if (activeQuestState == QuestState.Completed)
		{
			npcs[activeQuest.questNPCId].SetIcon(IconType.None);
			questText.TextOfQuest().enabled = false;
		
			if (activeQuest.questNPCId == npcs.Count - 1) 
				return;
		
			activeQuestItemCounter = 0;
			activeQuest = activeQuest.nextQuest;
			activeQuestState = QuestState.Pending;
			
			npcs[activeQuest.questNPCId].SetIcon(IconType.ExclamationPoint);
		}
	}

	public void ExecuteFetchQuest(int npcId)
	{
		string questName = activeQuest.displayName; //name of the quest itself
		string questNPCName = activeQuest.questNPCName;
		int questItemId = activeQuest.typeParam; //id of the item that the quest needs
		string questItemName = activeQuest.typeName; // name of the item that the quest needs
		int questItemCount = activeQuest.typeCount; // how many items the quest needs
		int currentItem = interactedItem.id; //id of the item that is being interacted with
		
		if (questItemId == currentItem)
		{
			if (activeQuestItemCounter < questItemCount)
			{
				itemScript[currentItems.IndexOf(interactedItem)].GetItem();
				activeQuestItemCounter++;
				questText.DisplayQuestText(questName, questItemCount, questItemName, activeQuestItemCounter);
			}
			
			if (activeQuestItemCounter == questItemCount)
			{
				questText.DisplayDeliverText(questName, questNPCName);
				activeQuestState = QuestState.Completed;
				npcs[activeQuest.questNPCId].SetIcon(IconType.InterrogationPoint);
			}
		}
	}

	public void ExecuteLocateQuest()
	{
		if (questTypeLocation.OnLocation())
		{
			activeQuestState = QuestState.Completed;
			npcs[activeQuest.questNPCId].SetIcon(IconType.InterrogationPoint);
		}
	}

	public void ExecuteResourceQuest()
	{
		
	}

	public List<Item> GetCurrentItems()
	{
		return currentItems;
	}

	public List<QuestTypeFetch> GetItemScript()
	{
		return itemScript;
	}
}
