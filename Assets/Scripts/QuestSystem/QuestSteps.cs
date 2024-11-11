using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum QuestType { fetch, locate, resource };

[CreateAssetMenu(menuName = "ScriptableObjects/QuestSteps")]

public class QuestSteps : ScriptableObject
{
	public QuestType type;
	public int stepId;
	[SerializeField] private int typeParam;
	[SerializeField] private int typeCount;
	[SerializeField] private string typeDisplayName;


	public int GetParam()
	{
		return typeParam;
	}

	public int GetCount() 
	{
		return typeCount;
	}

	public string GetDisplayName()
	{
		return typeDisplayName;
	}
}
