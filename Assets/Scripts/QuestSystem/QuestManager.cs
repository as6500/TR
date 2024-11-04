using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles all the states of all of the quests (active or not): on going/active, completed, pending
// handles the events of the quests: start, finish and advance
// (advance is for when the quests have several steps and the event gives an ok to update the steps).



public class QuestManager : MonoBehaviour
{
	public UnityEvent onStartQuest; //for when the player starts a quest
	public UnityEvent AdvanceQuest; //for when the player advances on the steps of the quest
	public UnityEvent onFinishQuest; //for when the player finishes all the steps for the quest and is completed
	public UnityEvent questStateChanges; // for the program to know what is the state that the quest that the player has, is in (mal escrito mas conta o esforço)
}
