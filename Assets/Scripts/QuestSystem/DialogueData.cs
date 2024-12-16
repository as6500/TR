using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueData")] 
public class DialogueData : ScriptableObject
{
    public string[] dialogueWithQuest;
    public List<String> dialogueWithoutQuest;
    public DialogueData nextDialogue;
    
}
