using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles the steps for all the quests (narrative and has the job to update the steps on the ui)

public class QuestStep : MonoBehaviour
{
	[SerializeField] private FetchQuestInfo fetchQuestInfo;

	private void Start()
	{
		fetchQuestInfo = GetComponent<FetchQuestInfo>();
	}

	public void UpdatingSteps()
	{

	}
}
