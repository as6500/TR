using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    private enum Menu {MainMenu, InGameMenu};

    [Header("Choose Menu")]
    [SerializeField] private Menu menu;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject backButton;

    [Header("In-Game Menu")]
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject collectibles;
    [SerializeField] private GameObject settings;

    private bool isMenuActive = false;


    void Start()
    {
        if (menu == Menu.MainMenu)
        {
            GoBackMainOptions();
        }
        else if (menu == Menu.InGameMenu)
        {
            ChangeGameState(isMenuActive);
        }
    }

    //Main Menu Functions

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
        SceneManager.LoadScene("BunkerInside");
    }

    //In-Game Menu Functions

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.M))
        {
            ChangeGameState(!isMenuActive);
            ShowMap();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameState(!isMenuActive);
            ShowSettings();
        }
    }

    public void LeaveToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeGameState(bool state)
    {
        isMenuActive = state;
        inGameMenu.SetActive(isMenuActive);
    }

    public void ShowMap()
    {
        map.transform.SetAsLastSibling();
    }

    public void ShowCollectibles()
    {
        collectibles.transform.SetAsLastSibling();
    }

    public void ShowSettings()
    {
        settings.transform.SetAsLastSibling();
    }

    //Commun Menu Functions

    public void LeaveGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    public bool IsMenuActive()
    {
        return isMenuActive;
    }
}
