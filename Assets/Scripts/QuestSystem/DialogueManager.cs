using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    private DialogueData currentDialogue;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image dialogueImage;
    private bool ballonTextEnded;
    private bool ballonTextEnded2;
    private string currentDialogueText;
    private string currentDialogueTextProgress;
    private int i;
    private int j;
    
    public UnityEvent OnDialogueEnd;
    
    private void Start()
    {
        dialogueText.enabled = false;
        ballonTextEnded = false;
        ballonTextEnded2 = false;
        continueButton.gameObject.SetActive(false);
        dialogueImage.enabled = false;
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
        
        for (i = 0; i < currentDialogue.dialogueWithQuest.Length; ++i) //passing from ballon to another ballon
        {
            dialogueImage.enabled = true;
            dialogueText.enabled = true;
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
            
        }

        if (i >= currentDialogue.dialogueWithQuest.Length)
        {
            ballonTextEnded = false;
            yield return new WaitUntil(ButtonClickedNotifier);
            OnDialogueEnd?.Invoke();
        }
        
        dialogueImage.enabled = false;
        dialogueText.enabled = false;
        continueButton.gameObject.SetActive(false);

    }

    private void OnButtonClicked()
    {
        if (dialogueText.text.Length == currentDialogueTextProgress.Length)
        { 
            Debug.Log("button clicked");
            ballonTextEnded = true;
            continueButton.gameObject.SetActive(false);
        }
    }

    private bool ButtonClickedNotifier()
    {
        return ballonTextEnded;
    }
}
