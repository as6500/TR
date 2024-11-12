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

	public void DisplayQuestText(string questName, int questAmount, string itemName)
	{
		questText.enabled = true;
		questText.text = $"{questName}: \n Get {questAmount} {itemName} ";
	}
}
