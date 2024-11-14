using UnityEngine;

[System.Serializable] public enum QuestType { Fetch, Locate, Resource };

[CreateAssetMenu(menuName = "ScriptableObjects/QuestScriptableObject")] 

public class QuestData : ScriptableObject
{
	public int id;
	public string displayName;
	public QuestType type;
	public int typeParam;
	public int typeCount;
	public string typeName;
	public int questNPCId;
	public string questNPCName;
	public QuestData nextQuest;
}
