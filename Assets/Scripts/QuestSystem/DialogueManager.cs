using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
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
    private Interacting interacting;
    private bool ballonTextEnded;
    private string currentDialogueText;
    private string currentDialogueTextProgress;
    
    public UnityEvent OnDialogueEnd;
    
    private void Start()
    {
        ballonTextEnded = false;
        continueButton.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
        dialogueImage.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(OnButtonClicked);
    }

    public void StartDialogue(DialogueData dialogueToPlay)
    {
        if (!dialogueToPlay)
        {
            Debug.LogError("DialogueManager::StartDialogue: Mafalda Needs To Fix The Null Ref");
            return;
        }
        if (currentDialogue == null)
            currentDialogue = dialogueToPlay;
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {   

        dialogueCanvas.gameObject.SetActive(true);
        Debug.Log(questManager.activeQuestState);
        if (questManager.activeQuestState != QuestState.Completed)
        {
            for (int i = 0; i < currentDialogue.dialogueWithQuest.Length; i++) //passing from ballon to another ballon
            {
                dialogueText.text = "";
                currentDialogueText = currentDialogue.dialogueWithQuest[i];
                
                for (int j = 0; j < currentDialogueText.Length; ++j) //actual text
                {
                    dialogueText.text += currentDialogueText[j];
                    currentDialogueTextProgress = dialogueText.text;
                    yield return new WaitForSeconds(0.06f);
                }
                continueButton.gameObject.SetActive(true);
                yield return new WaitUntil(ButtonClickedNotifier);
                ballonTextEnded = false;
            }
            
            OnDialogueEnd?.Invoke();
            continueButton.gameObject.SetActive(false);
            dialogueCanvas.gameObject.SetActive(false);    
        }
        else
        {
            dialogueText.text = "";
            currentDialogueText = currentDialogue.dialogueFinishingQuest[0];
            continueButton.gameObject.SetActive(false);
            
            for (int j = 0; j < currentDialogueText.Length; ++j) //actual text
            {
                dialogueText.text += currentDialogueText[j];
                currentDialogueTextProgress = dialogueText.text;
                yield return new WaitForSeconds(0.06f);
            }
            continueButton.gameObject.SetActive(true);
            yield return new WaitUntil(ButtonClickedNotifier);
            ballonTextEnded = false;
            continueButton.gameObject.SetActive(false);
            dialogueCanvas.gameObject.SetActive(false);
            currentDialogue = currentDialogue.nextDialogue;

            
            OnDialogueEnd?.Invoke();
        }
    }

    private void OnButtonClicked()
    {
        if (dialogueText.text.Length == currentDialogueTextProgress.Length)
        { 
            ballonTextEnded = true;
            continueButton.gameObject.SetActive(false);
        }
    }

    public bool ButtonClickedNotifier()
    {
        return ballonTextEnded;
    }
}
