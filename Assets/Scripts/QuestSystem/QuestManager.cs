using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

[Serializable] public enum QuestState { Pending, Active, Completed };

public class QuestManager : Singleton<QuestManager>
{
	public static readonly UnityEvent OnQuestAction = new();
	public QuestData activeQuest;
	public DialogueManager dialogueManager;
	[SerializeField]private DialogueData dialogueData;
	public QuestState activeQuestState;
	[SerializeField] private QuestSystemUI questText;
	[SerializeField] private TextMeshProUGUI titleText;
	public List<QuestNPC> npcs = new();
	[SerializeField] private QuestTypeLocation questTypeLocation;
	[SerializeField] private GameObject location;
	[SerializeField] private GameObject item;
	public Item interactedItem;
    private int activeQuestItemCounter;
    [SerializeField] private Text descriptionOfQuest;
	[SerializeField] private RawImage background;
	
	public void Start()
	{
		activeQuestState = QuestState.Pending;
		activeQuestItemCounter = 0;
		npcs[0].SetIcon(IconType.ExclamationPoint);
		npcs[1].SetIcon(IconType.None);
		npcs[2].SetIcon(IconType.None);
		npcs[3].SetIcon(IconType.None);
		
		OnQuestAction.AddListener(CompleteQuest);
		
		descriptionOfQuest.enabled = false;
		background.enabled = false;
		titleText.enabled = false;
	}

	public void Update()
	{
		descriptionOfQuest.text = "<b>Quest:</b> \n\n" + activeQuest.descriptionOfQuest;
		if (activeQuestState == QuestState.Active)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				descriptionOfQuest.enabled = !descriptionOfQuest.enabled;
				background.enabled = !background.enabled;
			}
		}
	}

	public void StartingDialogue()
	{
		dialogueManager.StartDialogue(dialogueData);
		dialogueManager.OnDialogueEnd.AddListener(OnDialogueFinished);
	}

	private void OnDialogueFinished()
	{
		AcceptQuest();
	}
	
	public void AcceptQuest()
	{
		if (activeQuestState != QuestState.Pending)
			return;
		
		activeQuestState = QuestState.Active;
		titleText.enabled = true;

		npcs[activeQuest.npc.id].SetIcon(IconType.None);
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
		
		npcs[activeQuest.npc.id].SetIcon(IconType.None);
		questText.TextOfQuest().enabled = false;
		
		if (activeQuest.npc.id == npcs.Count - 1) 
			return;
		
		activeQuestItemCounter = 0;
		activeQuest = activeQuest.nextQuest;
		activeQuestState = QuestState.Pending;
		npcs[activeQuest.npc.id].SetIcon(IconType.ExclamationPoint);
	}

	private void AcceptFetchQuest()
	{
		int questParam = activeQuest.typeParam;
		string questName = activeQuest.displayName;
		string questItemName = activeQuest.typeName;
		int questCount = activeQuest.typeCount;
		
		for (int i = 0; i < questCount; i++)
		{
			Vector3 randomPosition = transform.position + new Vector3(Random.Range(-6f, -4f), Random.Range(-2f, -5f), 0);
			GameObject tempItem = Instantiate(item, randomPosition, Quaternion.identity); //create temporary item
			tempItem.name = "Item" + i; //name + 1 number
			Item newItem = tempItem.GetComponent<Item>();
			newItem.SetUpItem(questParam);
		}
		questText.DisplayFetchQuestText(questName, questCount, questItemName, activeQuestItemCounter);
	}

	private void AcceptResourceQuest()
	{
        string questName = activeQuest.displayName;
        string questItemName = activeQuest.typeName;
        int questCount = activeQuest.typeCount;

        questText.DisplayResourceQuestText(questName, questItemName, questCount, activeQuestItemCounter);
    }
	
	private void AcceptLocateQuest()
	{
		string questName = activeQuest.displayName;
		string questItemName = activeQuest.typeName;
		
		Vector3 randomPosition = transform.position + new Vector3(Random.Range(-5f, 2f), Random.Range(-2f, 1f), 0);
		GameObject tempLocation = Instantiate(location, randomPosition, Quaternion.identity);
		questTypeLocation = tempLocation.GetComponent<QuestTypeLocation>();
		questText.DisplayLocateQuestText(questName, questItemName);
	}
	
	private void ExecuteFetchQuest()
	{
		string questName = activeQuest.displayName; //name of the quest itself
		string questNPCName = activeQuest.npc.name; //name of the npc that has the quest/the npc where the player needs to deliver
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
		npcs[activeQuest.npc.id].SetIcon(IconType.InterrogationPoint);
	}

	private void ExecuteLocateQuest()
	{
		string questName = activeQuest.displayName;
		string questNPCName = activeQuest.npc.name;

		if (!questTypeLocation.OnLocation()) 
			return;
		
		questText.DisplayLocateDeliverText(questName, questNPCName);
		activeQuestState = QuestState.Completed;
		SceneManagement.Instance.RemoveObjectFromScene(questTypeLocation.gameObject);
		Destroy(questTypeLocation.gameObject);
		npcs[activeQuest.npc.id].SetIcon(IconType.InterrogationPoint);
	}

	private void ExecuteResourceQuest()
	{
        string questName = activeQuest.displayName;
        string questNPCName = activeQuest.npc.name;
        string questItemName = activeQuest.typeName; // name of the item that the quest needs
        int questItemCount = activeQuest.typeCount; // how many items the quest needs

        if (activeQuestItemCounter < questItemCount)
        {
            activeQuestItemCounter++;
            questText.DisplayResourceQuestText(questName, questItemName, questItemCount, activeQuestItemCounter);
        }

        if (activeQuestItemCounter != questItemCount)
            return;

        activeQuestState = QuestState.Completed;
        questText.DisplayResourceDeliverText(questName, questNPCName);
        npcs[activeQuest.npc.id].SetIcon(IconType.InterrogationPoint);
    }
}
