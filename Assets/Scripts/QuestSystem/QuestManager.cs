using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : MonoBehaviour
{
	public List<QuestNPC> npcs = new List<QuestNPC>();
	public List<QuestScriptableObject> quests = new List<QuestScriptableObject>();
	public QuestState activeQuestState;
	public List<NPCInteractable> npcInteractable = new List<NPCInteractable>();
	private int activeQuestCounter;
	private bool activeQuestCompleted = false;
	private int activeQuest;

	public void Start()
	{
		activeQuest = quests[0].id;
		activeQuestState = QuestState.Pending;

		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);

		npcInteractable[1].Interacted(false);
	}

	public void Update()
	{
		UpdateThings();
		//Debug.Log("activequestid: " + activeQuest);
		//Debug.Log("displayname:" + quests[activeQuest + 1].displayName);
		//Debug.Log(npcInteractable[0].Interacted(true));
		//Debug.Log("activeQuestState part2: " + activeQuestState);
		//Debug.Log("activeQuestID part2: " + activeQuest);
		//Debug.Log("NPC ID: " + npc.NPCId);
	}

	public void UpdateThings()
	{
		if (Input.GetKeyDown(KeyCode.F)) //means player talked to the NPC
		{
			activeQuestState = QuestState.Active;
			npcs[0].SetIcon(IconType.None);
			npcs[1].SetIcon(IconType.None);

			npcInteractable[0].Interacted(false);
			npcInteractable[1].Interacted(false);
			Debug.Log(activeQuestState);
		}

		if (Input.GetKeyDown(KeyCode.M)) //means player completed quest
		{
			activeQuestState = QuestState.Completed;
			npcs[0].SetIcon(IconType.InterrogationPoint);
			npcs[1].SetIcon(IconType.None);

			npcInteractable[0].Interacted(true);
			npcInteractable[1].Interacted(false);

			Debug.Log(activeQuestState);
		}

		if (Input.GetKeyDown(KeyCode.B)) //means player delivered quest and is prepared for next one
		{
			npcs[0].SetIcon(IconType.None);
			Debug.Log("Again: " + npcs[0].iconType);

			Debug.Log(activeQuestState);
			activeQuestCompleted = true;
			activeQuest = quests[activeQuest + 1].id;
			npcInteractable[1].Interacted(true);
			activeQuestState = QuestState.Pending;
			npcs[1].SetIcon(IconType.ExclamationPoint);
			activeQuestCompleted = false;
		}
	}
}
