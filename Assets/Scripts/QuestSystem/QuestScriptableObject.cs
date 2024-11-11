using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/QuestScriptableObject")] 

public class QuestScriptableObject : ScriptableObject
{
	public int id;
	public string displayName; //put private after testing
	public List<QuestSteps> steps;
	public int questNPCId;
	[SerializeField] private int nextQuestId;

}
