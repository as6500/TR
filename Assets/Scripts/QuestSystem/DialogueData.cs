using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueData")] 
public class DialogueData : ScriptableObject
{
    public List<String> dialogueWithQuest;
    public List<String> dialogueWithoutQuest;
}
