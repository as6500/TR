using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : MonoBehaviour
{
	public List<QuestNPC> npcs = new List<QuestNPC>();
	public List<QuestScriptableObject> quests = new List<QuestScriptableObject>();
	public QuestState activeQuestState;
	[SerializeField] private Item currentItems;
	[SerializeField] private QuestTypeFetch typeFetch;
	[SerializeField] private GameObject item;
	private int activeQuestCounter;
	private bool activeQuestCompleted = false;
	private int activeQuest;
	private int currentStep;
	
	public void Start()
	{
		activeQuest = quests[0].id;
		currentStep = quests[activeQuest].steps[0].stepId;
		activeQuestState = QuestState.Pending;

		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);
		npcs[2].SetIcon(IconType.None);
		npcs[3].SetIcon(IconType.None);
		
		Debug.Log(activeQuestState);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			CompletingQuest(quests[activeQuest].questNPCId);
			Debug.Log(activeQuestState);
		}
		
	}

	public void AcceptQuest()
	{
		if (activeQuestState == QuestState.Pending)
		{
			activeQuestState = QuestState.Active;

			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);
			if (quests[activeQuest].steps[currentStep].type == QuestType.fetch)
			{
				GameObject tempItem = Instantiate(item, transform);
				currentItems = tempItem.GetComponent<Item>();
				currentItems.SetUpItem(quests[activeQuest].steps[currentStep].GetParam(), quests[activeQuest].steps[currentStep].GetDisplayName()); //change later
				typeFetch = tempItem.GetComponent<QuestTypeFetch>();
			}
		}
	}

	public void CompletingQuest(int npcId)
	{
		if (activeQuestState == QuestState.Active)
		{
			ExecuteQuestSteps(currentStep);
			activeQuestState = QuestState.Completed;
			npcs[npcId].SetIcon(IconType.InterrogationPoint);
		}
	}

	public void OntoNextQuest()
	{
		if (activeQuestState == QuestState.Completed)
		{
			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);
			activeQuestCompleted = true;

			if (activeQuest == quests.Count - 1 || quests[activeQuest].questNPCId == npcs.Count - 1) 
				return;

			activeQuest = quests[activeQuest + 1].id;
			activeQuestState = QuestState.Pending;
			
			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.ExclamationPoint);
			activeQuestCompleted = false;
			Debug.Log(quests[activeQuest].steps[currentStep]);
		}
	}

	public void ExecuteQuestSteps(int stepId) //still working on this
	{
		if (stepId < 0 || stepId > quests[activeQuest].steps.Count)
			return;

		if (quests[activeQuest].steps[currentStep].type == QuestType.fetch)
		{
			int questItemId = quests[activeQuest].steps[currentStep].GetParam(); //id of the item that the step needs
			int questItemCount = quests[activeQuest].steps[currentStep].GetCount(); // how many items the step needs
			string questItemName = quests[activeQuest].steps[currentStep].GetDisplayName();
			int currentItem = currentItems.id;

			if (questItemId == currentItem)
			{
				Debug.Log("Hello!");
				typeFetch.GetItem();
			}
			Debug.Log($"Get {questItemCount} {questItemName} for Many");

			Debug.Log("itemId" + questItemId);
		}
	}
}
