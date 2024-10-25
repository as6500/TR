using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private enum Menu {MainMenu, InGameMenu};

    [Header("Choose Menu")]
    [SerializeField] private Menu menu;

    [Header("Main Menu")]
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject backButton;

    [Header("In-Game Menu")]
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject collectibles;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject close;


    void Start()
    {
        if (menu == Menu.MainMenu)
        {
            GoBackMainOptions();
        }
        else if (menu == Menu.InGameMenu)
        {

        }
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
