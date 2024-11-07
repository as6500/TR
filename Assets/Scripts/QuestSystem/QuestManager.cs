using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles all the states of all of the quests (active or not): on going/active, completed, pending
// handles the events of the quests: start, finish and advance
// (advance is for when the quests have several steps and the event gives an ok to update the steps).

[System.Serializable] public class OnStartQuest : UnityEvent { }; //for when the player starts a quest
[System.Serializable] public class AdvanceQuest : UnityEvent { }; //for when the player advances on the steps of the quest
[System.Serializable] public class OnFinishQuest : UnityEvent { }; //for when the player finishes all the steps for the quest and is completed
[System.Serializable] public class QuestStateChanges : UnityEvent { }; // for the program to know what is the state that the quest that the player has, is in (mal escrito mas conta o esforço)

public class QuestManager : MonoBehaviour
{
	public enum QuestState { RequirementNotMet, NotActive, Active, Completed, Finished };

	public OnStartQuest onStartQuest;
	public AdvanceQuest advanceQuest;
	public OnFinishQuest onFinishQuest;
	public QuestStateChanges questStateChanges;

	public void StartQuest()
	{
		onStartQuest.Invoke();
	}
}