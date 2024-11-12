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
	public List<QuestNPC> npcs = new List<QuestNPC>();
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
		{
			CompletingQuest();
		}
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
		int questParam = activeQuest.typeParam; //just so the param names are not long
		string questItemName = activeQuest.displayName; //just so the param names are not long
		int questCount = activeQuest.typeCount;
		
		for (int i = 0; i < questCount; i++)
		{
			Vector3 randomPosition = transform.position + new Vector3(Random.Range(2f, 6f), Random.Range(4f, 5f), 0);
			GameObject tempItem = Instantiate(item, randomPosition, Quaternion.identity); //create temporary item
			Item newItem = tempItem.GetComponent<Item>();
			newItem.SetUpItem(questParam, questItemName);
			currentItems.Add(newItem); //review this later
			itemScript.Add(tempItem.GetComponent<QuestTypeFetch>()); //review this later
		}
	}

	private void AcceptResourceQuest()
	{
		//Code for Resource Quests
	}
	
	private void AcceptLocateQuest()
	{
		//Code for Locate Quests
	}

	public void CompletingQuest()
	{
		if (activeQuestState == QuestState.Active)
		{
			ExecuteQuestSteps(activeQuest.questNPCId);
		}
	}

	public void OntoNextQuest()
	{
		if (activeQuestState == QuestState.Completed)
		{
			npcs[activeQuest.questNPCId].SetIcon(IconType.None);
		
			if (activeQuest.questNPCId == npcs.Count - 1) 
				return;
		
			activeQuestItemCounter = 0;
			activeQuest = activeQuest.nextQuest;
			activeQuestState = QuestState.Pending;
			
			npcs[activeQuest.questNPCId].SetIcon(IconType.ExclamationPoint);
		}
	}

	public void ExecuteQuestSteps(int npcId)
	{
		if (activeQuest.type == QuestType.fetch)
		{
			int questItemId = activeQuest.typeParam; //id of the item that the step needs
			int questItemCount = activeQuest.typeCount; // how many items the step needs
			string questItemName = activeQuest.displayName; //what is the name of the object that the quest needs
			int currentItem = interactedItem.id; //id of the item that is being interacted with
			
			Debug.Log(currentItem);
			if (questItemId == currentItem)
			{
				if (activeQuestItemCounter < questItemCount)
				{
					itemScript[currentItems.IndexOf(interactedItem)].GetItem();
					activeQuestItemCounter++;
				}
				
				if (activeQuestItemCounter == questItemCount)
				{
					activeQuestState = QuestState.Completed;
					npcs[activeQuest.questNPCId].SetIcon(IconType.InterrogationPoint);
				}
			}
			Debug.Log("itemId" + questItemId);
		}
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
