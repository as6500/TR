using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public DialogueData currentDialogue;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button continueButton;
    private bool isButtonPressed;

    private void Start()
    {
        dialogueText.enabled = false;
        continueButton.gameObject.SetActive(false);
        isButtonPressed = false;
    }

    private void StartDialogue(DialogueData dialogueToPlay)
    { 
        currentDialogue = dialogueToPlay;
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        continueButton.gameObject.SetActive(true);
        for (int i = 0; i < currentDialogue.dialogueWithQuest.Length; ++i)
        {
            dialogueText.enabled = true;
            dialogueText.text = "";
            string currentDialogueText = currentDialogue.dialogueWithQuest[i];

            for (int j = 0; j < currentDialogueText.Length; ++j)
            {
                dialogueText.text += currentDialogueText[j];
                yield return new WaitForSeconds(0.1f);
            }
            if (continueButton.onClick != null)
                yield return new WaitForSeconds(2.0f);
        }
        dialogueText.enabled = false;
        continueButton.gameObject.SetActive(false);
        
    }
    
    public bool OnContinueButtonClicked()
    {
        StartDialogue(currentDialogue);
        return true;
    }
}
