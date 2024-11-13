using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUI : MonoBehaviour
{
	[SerializeField] private QuestManager manager;
	[SerializeField] private Item item;
	private TextMeshProUGUI questText;

	private void Start()
	{
		questText = GetComponent<TextMeshProUGUI>();
		questText.enabled = false;
	}

	public void DisplayFetchQuestText(string questName, int questAmount, string itemName, int currentItemAmount)
	{
		questText.enabled = true;
		questText.text = $"{questName}: \n - Get {questAmount} {itemName} {currentItemAmount} / {questAmount}";
	}

	public void DisplayFetchDeliverText(string questName, string NPCName)
	{
		questText.text = $"{questName}: \n Return to {NPCName}";
	}

	public void DisplayLocateQuestText(string questName, string itemName)
	{
		questText.enabled = true;
		questText.text = $"{questName}: \n Find the location of {itemName}";
	}

	public void DisplayLocateDeliverText(string questName, string NPCName)
	{
		questText.text = $"{questName}: \n Return to {NPCName}";
	}

	public TextMeshProUGUI TextOfQuest()
	{
		return questText;
	}
}
