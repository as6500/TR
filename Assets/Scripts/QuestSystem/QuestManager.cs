using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : MonoBehaviour
{
	public List<QuestNPC> npcs = new List<QuestNPC>();
	public List<QuestScriptableObject> quests = new List<QuestScriptableObject>();
	public QuestState activeQuestState;
	[SerializeField] public List<QuestTypeFetch> typeFetch = new List<QuestTypeFetch>();
	[SerializeField] public List<Item> currentItems = new List<Item>();
	[SerializeField] private GameObject item;
	public Item interactedItem;
	private int activeQuestCounter;
	public ScriptableObject activeQuest;
	public int currentStep;
	
	public void Start()
	{
		activeQuest = quests[0].id;
		currentStep = quests[activeQuest].steps[0].stepId;
		activeQuestState = QuestState.Pending;
		activeQuestCounter = 0;

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

			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);
			switch (quests[activeQuest].steps[currentStep].type)
			{
				case QuestType.fetch:
					AcceptFetchQuest();
					break;
				case QuestType.resource:
					AcceptResourceQuest();
					break;
			}
		}
	}

	private void AcceptFetchQuest()
	{
		int questParam = quests[activeQuest].steps[currentStep].GetParam(); //just so the param names are not long
		string questItemName = quests[activeQuest].steps[currentStep].GetDisplayName(); //just so the param names are not long
		int questCount = quests[activeQuest].steps[currentStep].GetCount();

		for (int i = 0; i < questCount; i++)
		{
			Vector3 randomPosition = transform.position + new Vector3(Random.Range(2f, 6f), Random.Range(4f, 5f), 0);
			GameObject tempItem = Instantiate(item, randomPosition, Quaternion.identity); //create temporary item
			Item newItem = tempItem.GetComponent<Item>();
			newItem.SetUpItem(questParam, questItemName);
			currentItems.Add(newItem);
			typeFetch.Add(tempItem.GetComponent<QuestTypeFetch>());
		}
	}

	private void AcceptResourceQuest()
	{

	}

	public void CompletingQuest()
	{
		if (activeQuestState == QuestState.Active)
		{
			ExecuteQuestSteps(currentStep, quests[activeQuest].questNPCId);
		}
	}

	public void OntoNextQuest()
	{
		if (activeQuestState == QuestState.Completed)
		{
			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);

			if (activeQuest == quests.Count - 1 || quests[activeQuest].questNPCId == npcs.Count - 1) 
				return;

			activeQuestCounter = 0;
			activeQuest = quests[activeQuest + 1].id;
			activeQuestState = QuestState.Pending;
			
			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.ExclamationPoint);
		}
	}

	public void ExecuteQuestSteps(int stepId, int npcId)
	{
		if (stepId < 0 || stepId > quests[activeQuest].steps.Count)
			return;

		// NELIO SAID QuestSteps step = quests[activeQuest].steps[currentStep];

		if (quests[activeQuest].steps[currentStep].type == QuestType.fetch)
		{
			int questItemId = quests[activeQuest].steps[currentStep].GetParam(); //id of the item that the step needs
			int questItemCount = quests[activeQuest].steps[currentStep].GetCount(); // how many items the step needs
			string questItemName = quests[activeQuest].steps[currentStep].GetDisplayName(); //what is the name of the object that the quest needs
			int currentItem = interactedItem.id; //id of the item that is being interacted with

			if (questItemId == currentItem)
			{
				if (activeQuestCounter < questItemCount)
				{
					typeFetch[currentItems.IndexOf(interactedItem)].GetItem();
					activeQuestCounter++;
				}
				
				if (activeQuestCounter == questItemCount)
				{
					activeQuestState = QuestState.Completed;
					npcs[quests[activeQuest].questNPCId].SetIcon(IconType.InterrogationPoint);
				}
			}
			Debug.Log("itemId" + questItemId);
		}
	}
}
