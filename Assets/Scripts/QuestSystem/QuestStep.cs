using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// handles the steps for all the quests (narrative and has the job to update the steps on the ui)

public class QuestStep : MonoBehaviour
{
	private string stepStates;
	private int currentQuestStep = 0;
	[SerializeField] private FetchQuestInfo fetchQuestInfo;
	[SerializeField] private string[] questStepStates; //states of the steps: active if the player is in that step, and completed if the player has finished that step and the quest is ready to update the step information
	private void Start()
	{
		stepStates = questStepStates[0];
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			UpdateSteps();
		}
	}

	public void UpdateSteps()
	{
		stepStates = questStepStates[1];
		fetchQuestInfo.DisplayQuest(currentQuestStep);
		currentQuestStep++;
	}


}
