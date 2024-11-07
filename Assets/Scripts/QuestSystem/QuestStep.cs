using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

// handles the steps for all the quests (narrative and has the job to update the steps on the ui)
//states: 0 - active step; 1 - completed sted
public class QuestStep : MonoBehaviour
{
	[SerializeField] private FetchQuestInfo fetchQuestInfo;
	[SerializeField] private Quest quest;
	[SerializeField] private QuestDelivery questDelivery;
	[SerializeField] private string[] questStepStates; //states of the steps: active if the player is in that step, and completed if the player has finished that step and the quest is ready to update the step information
	private string stepStates;
	private int currentQuestStep = 0;

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
		Debug.Log("current: " + currentQuestStep);
		Debug.Log("total: " + (fetchQuestInfo.StepsLength() - 1));

		if (currentQuestStep < fetchQuestInfo.StepsLength() - 1)
		{
			stepStates = questStepStates[1];
			//GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
			currentQuestStep++;
			fetchQuestInfo.DisplayQuest(currentQuestStep);
			//GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
		}
		else if (currentQuestStep == fetchQuestInfo.StepsLength() - 1)
		{
			Debug.Log("Putting a interrogation point");
			//GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
			quest.UpdateQuestIcon();
			stepStates = questStepStates[1];
		}
	}

	public int CurrentQuestStep()
	{
		return currentQuestStep;
	}
}
