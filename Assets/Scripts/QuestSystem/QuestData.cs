using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] public enum QuestType { fetch, locate, resource };

[CreateAssetMenu(menuName = "ScriptableObjects/QuestScriptableObject")] 

public class QuestData : ScriptableObject
{
	public int id;
	public string displayName;
	public QuestType type;
	public int typeParam;
	public int typeCount;
	public int questNPCId;
	public QuestData nextQuest;
	
}
