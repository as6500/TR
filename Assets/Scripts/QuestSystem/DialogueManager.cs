using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueManager : Singleton<DialogueManager>
{
    private DialogueData currentDialogue;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private NPCInteractable interactableNPC;
    private bool ballonTextEnded;
    private string currentDialogueText;
    private string currentDialogueTextProgress;


    private int index = 0;
    private int j;
    
    public UnityEvent OnDialogueEnd;
    
    private void Start()
    {
        ballonTextEnded = false;
        continueButton.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogueToPlay)
    {
        if (!dialogueToPlay)
        {
            Debug.LogError("DialogueManager::StartDialogue: Mafalda Needs To Fix The Null Ref");
            return;
        }
        currentDialogue = dialogueToPlay;
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {   
        continueButton.onClick.AddListener(OnButtonClicked);
        Debug.Log(SceneManager.GetActiveScene().name);
        dialogueCanvas.gameObject.SetActive(true);
        for (int i = 0; i < currentDialogue.dialogueWithQuest.Length; ++i) //passing from ballon to another ballon
        {
            dialogueText.text = "";
            currentDialogueText = currentDialogue.dialogueWithQuest[i];

            for (j = 0; j < currentDialogueText.Length; ++j) //actual text
            {
                dialogueText.text += currentDialogueText[j];
                currentDialogueTextProgress = dialogueText.text;
                yield return new WaitForSeconds(0.07f);
            }
            continueButton.gameObject.SetActive(true);
            yield return new WaitUntil(ButtonClickedNotifier);
            ++index;
        }
            
        if (index >= currentDialogue.dialogueWithQuest.Length)
        {
            ballonTextEnded = false;
            yield return new WaitUntil(ButtonClickedNotifier);
            OnDialogueEnd?.Invoke();
            dialogueCanvas.gameObject.SetActive(false);
        }
        continueButton.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
    }

    private void OnButtonClicked()
    {
        if (dialogueText.text.Length == currentDialogueTextProgress.Length)
        { 
            ballonTextEnded = true;
            continueButton.gameObject.SetActive(false);
        }
    }

    private bool ButtonClickedNotifier()
    {
        return ballonTextEnded;
    }

    public void OntoNextDialogue()
    {
        if (questManager.activeQuestState == QuestState.Completed && interactableNPC.IsInRange())
        {
            currentDialogueText = currentDialogue.dialogueFinishingQuest[0];
        }
        
        if (questManager.activeQuestState == QuestState.Completed &&
            questManager.activeQuest == questManager.activeQuest.nextQuest && questManager.activeQuest.npc.id == questManager.activeQuest.npc.id + 1)
        {
            currentDialogue = currentDialogue.nextDialogue;
        }
        
    }
}
