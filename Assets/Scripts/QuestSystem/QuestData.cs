using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public enum QuestType { Fetch, Locate, Resource };

[CreateAssetMenu(menuName = "ScriptableObjects/QuestData")]

public class QuestData : ScriptableObject
{
	public int id;
	public string displayName;
	public QuestType type;
	public int typeParam;
	public int typeCount;
	public string typeName;
    public string descriptionOfQuest;
	public NPCData npc;
	public QuestData nextQuest;

}
