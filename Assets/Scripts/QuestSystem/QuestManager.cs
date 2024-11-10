using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : MonoBehaviour
{
	public List<QuestNPC> npcs = new List<QuestNPC>();
	public List<QuestScriptableObject> quests = new List<QuestScriptableObject>();
	public QuestState activeQuestState;
	public QuestTypeFetch fetchQuest;
	private int activeQuestCounter;
	private bool activeQuestCompleted = false;
	private int activeQuest;

	public void Start()
	{
		activeQuest = quests[0].id;
		activeQuestState = QuestState.Pending;
		quests[activeQuest].typeCount = 0;

		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);
		npcs[2].SetIcon(IconType.None);
		npcs[3].SetIcon(IconType.None);
	}

	private void Update()
	{
		CompletingQuest(quests[activeQuest].questNPCId);
	}

	public void AcceptQuest()
	{
		if (activeQuestState == QuestState.Pending)
		{
			activeQuestState = QuestState.Active;

			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);
			if (quests[activeQuest].type == QuestType.fetch)
			{
				fetchQuest.gameObject.SetActive(true);
			}
			else if (quests[activeQuest].type == QuestType.locate)
			{

			}
			else if (quests[activeQuest].type == QuestType.resource)
			{

			}
		}
	}

	public void CompletingQuest(int npcId)
	{
		if (activeQuestState == QuestState.Active && quests[activeQuest].type == QuestType.fetch)
		{
			if (fetchQuest.ItemPickedUp() == true)
			{
				activeQuestState = QuestState.Completed;
				npcs[npcId].SetIcon(IconType.InterrogationPoint);
			}
		}
		//FOR WHEN THEY DO THEIR PART
		//else if (activeQuestState == QuestState.Active && quests[activeQuest].type == QuestType.locate)
		//{
		//	if (locateQuest.LocationReached() == true)
		//	{
		//		activeQuestState = QuestState.Completed;
		//		npcs[npcId].SetIcon(IconType.InterrogationPoint);
		//	}
		//}
		//else if (activeQuestState == QuestState.Active && quests[activeQuest].type == QuestType.resource)
		//{
		//	if (resourceQuest.AllItemsObtained() == true)
		//	{
		//		activeQuestState = QuestState.Completed;
		//		npcs[npcId].SetIcon(IconType.InterrogationPoint);
		//	}
		//}

	}

	public void OntoNextQuest()
	{
		if (activeQuestState == QuestState.Completed)
		{
			quests[activeQuest].typeCount = 0;
			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);
			activeQuestCompleted = true;

			if (activeQuest == quests.Count - 1 || quests[activeQuest].questNPCId == npcs.Count - 1) 
				return;

			activeQuest = quests[activeQuest + 1].id;
			activeQuestState = QuestState.Pending;

			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.ExclamationPoint);
			activeQuestCompleted = false;


			//Debug.Log("All quests: " + (quests.Count - 1));
			//Debug.Log("Did it move? " + activeQuestState);
			//Debug.Log("What is the next active quest? " + quests[activeQuest].id);
			//Debug.Log("What is the name of the active quest? " + quests[activeQuest].displayName);
		}
	}
}
