using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueData")] 
public class DialogData : ScriptableObject
{
    public List<String> dialogue;
}
