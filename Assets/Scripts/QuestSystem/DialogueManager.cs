using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueData activeDialogue;
    public QuestManager questManager;

    private void Update()
    {
        if (questManager.activeQuestState == QuestState.Pending)
        {
            
        }
    }
}
