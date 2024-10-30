using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FetchQuestInfo : MonoBehaviour
{
	[SerializeField] private string id;
	[SerializeField] private string displayName;
	[SerializeField] private int pillsReward;
	[SerializeField] private int objectReward;

	public void DisplayQuestName()
	{
		GetComponent<TextMeshProUGUI>().text = $"Quests: {displayName}";
	}


}
