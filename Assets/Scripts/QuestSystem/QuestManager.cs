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
	private int activeQuestCounter;
	private bool activeQuestCompleted = false;
	private int activeQuest;

	public void Start()
	{
		activeQuest = quests[0].id;
		activeQuestState = QuestState.Pending;

		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);

	}

	private void Update()
	{
		CompleteQuest(quests[activeQuest].questNPCId);
	}

	public void AcceptQuest()
	{
		if (activeQuestState == QuestState.Pending)
		{
			activeQuestState = QuestState.Active;

			npcs[quests[activeQuest].questNPCId].SetIcon(IconType.None);
			//npcs[1].SetIcon(IconType.None);

			Debug.Log("Active? " +  activeQuestState);
		}
	}

	public void CompleteQuest(int npcId)
	{
		if (Input.GetKeyDown(KeyCode.G) && activeQuestState == QuestState.Active)
		{
			activeQuestState = QuestState.Completed;

			npcs[npcId].SetIcon(IconType.InterrogationPoint);

			Debug.Log("Completed? " + activeQuestState);
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

			Debug.Log("All quests: " + (quests.Count - 1));
			Debug.Log("Did it move? " + activeQuestState);
			Debug.Log("What is the next active quest? " + quests[activeQuest].id);
			Debug.Log("What is the name of the active quest? " + quests[activeQuest].displayName);
		}
	}

	//public void updatethings()
	//{
	//	if (input.getkeydown(keycode.f)) //means player talked to the npc
	//	{
	//		activequeststate = queststate.active;


	//		debug.log(activequeststate);
	//	}

	//	if (input.getkeydown(keycode.m)) //means player completed quest
	//	{
	//		activequeststate = queststate.completed;
	//		npcs[0].seticon(icontype.interrogationpoint);
	//		npcs[1].seticon(icontype.none);

	//		debug.log(activequeststate);
	//	}

	//	if (input.getkeydown(keycode.b)) //means player delivered quest and is prepared for next one
	//	{

	//		debug.log("again: " + npcs[0].icontype);

	//		debug.log(activequeststate);

	//	}
	//}

	//UpdateThings();
	//Debug.Log("activequestid: " + activeQuest);
	//Debug.Log("displayname:" + quests[activeQuest + 1].displayName);
	//Debug.Log(npcInteractable[0].Interacted(true));
	//Debug.Log("activeQuestState part2: " + activeQuestState);
	//Debug.Log("activeQuestID part2: " + activeQuest);
	//Debug.Log("NPC ID: " + npc.NPCId);

	//public void Update()
	//{

	//}


}
