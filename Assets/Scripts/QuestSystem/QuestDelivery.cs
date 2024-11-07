using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class QuestDelivery : MonoBehaviour
{
	[SerializeField] private Quest questScript;
	[SerializeField] private QuestStep questStepScript;
    [SerializeField] private FetchQuestInfo fetchQuestInfoScript;

	public void DeliveryTheQuest()
	{
        if (questStepScript.CurrentQuestStep() > fetchQuestInfoScript.StepsLength() - 1)
        {
            questScript.CurrentQuestState(3);
            //Debug.Log(GameObject.Find("ActiveQuest").GetComponent<TextMeshProUGUI>());
            Destroy(GameObject.Find("ActiveQuest").GetComponent<TextMeshProUGUI>());
        }
    }
}
