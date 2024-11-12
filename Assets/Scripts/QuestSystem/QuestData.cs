using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] public enum QuestType { fetch, locate, resource };

[CreateAssetMenu(menuName = "ScriptableObjects/QuestScriptableObject")] 

public class QuestData : ScriptableObject //QuestData em vez de QuestScriptableObject
{
	public int id;
	public string displayName; //put private after testing
	public QuestType type;
	public int typeParam;
	public int typeCount;
	public int questNPCId;
	public QuestData nextQuest;
	
}
