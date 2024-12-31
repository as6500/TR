using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueData")] 
public class DialogueData : ScriptableObject
{
    public string[] dialogueWithQuest;
    public string[] dialogueWithoutQuest;
    public string[] dialogueFinishingQuest;
    public DialogueData nextDialogue;
    
}
