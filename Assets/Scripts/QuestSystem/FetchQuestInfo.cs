using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

// handles all the information about the quests: id, displayed name on the ui, rewards that the quest has.

public class FetchQuestInfo : MonoBehaviour
{
	[SerializeField] private string id;
	[SerializeField] private string displayName;
	[SerializeField] private int pillsReward;
	[SerializeField] private int objectReward;
	[SerializeField] private string[] steps;

	public void DisplayQuest(int i)
	{
		GetComponent<TextMeshProUGUI>().text = $"{displayName}: \n - {steps[i]}";
	}
}
