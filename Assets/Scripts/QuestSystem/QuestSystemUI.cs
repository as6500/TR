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
		questText.text = $"{questName}: \nGet {questAmount} {itemName} {currentItemAmount} / {questAmount} \nPress z to see the description";
	}

	public void DisplayFetchDeliverText(string questName, string npcName)
	{
		questText.text = $"{questName}: \nReturn to {npcName}";
	}

	public void DisplayLocateQuestText(string questName, string itemName)
	{
		questText.enabled = true;
		questText.text = $"{questName}: \nFind the location of a {itemName} \nPress z to see the description";
	}

	public void DisplayLocateDeliverText(string questName, string npcName)
	{
		questText.text = $"{questName}: \nReturn to {npcName}";
	}

    public void DisplayResourceQuestText(string questName, string itemName, int questAmount, int currentItemAmount)
    {
        questText.enabled = true;
		questText.text = $"{questName}: \nGet {questAmount} {itemName} by killing enemies. {currentItemAmount} / {questAmount} \nPress z to see the description";
    }

    public void DisplayResourceDeliverText(string questName, string npcName)
    {
        questText.text = $"{questName}: \nReturn to {npcName}";
    }

    public TextMeshProUGUI TextOfQuest()
	{
		return questText;
	}
}
