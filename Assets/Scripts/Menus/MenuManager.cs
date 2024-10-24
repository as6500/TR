using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject backButton;

    void Start()
    {
        GoBackMainOptions();
    }

    public void GoToSettings()
    {
        settingsView.SetActive(true);
        mainOptions.SetActive(false);
    }

    public void GoBackMainOptions()
    {
        settingsView.SetActive(false);
        mainOptions.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Weapons");
    }

    public void LeaveToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LeaveGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}
