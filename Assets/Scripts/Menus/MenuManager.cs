using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    private enum Menu {MainMenu, InGameMenu, DeathMenu};

    [Header ("API Connection")]
    [SerializeField] private CreateConnection APIRequests;

    [Header("Choose Menu")]
    [SerializeField] private Menu menu;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject appView;
    [SerializeField] private GameObject codeView;
    [SerializeField] private Text connectionCodeText;

    [Header("In-Game Menu")]
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject collectibles;
    [SerializeField] private GameObject settings;

    [Header("Death Menu")]
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject quitToTitle;
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private GameObject Weapons;
    
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

    public void GoToSettingsView()
    {
        settingsView.SetActive(true);
        appView.SetActive(false);
        mainOptions.SetActive(false);
        codeView.SetActive(false);
    }

    public void GoToAppView()
    {
        appView.SetActive(true);
        settingsView.SetActive(false);
        codeView.SetActive(false);
        mainOptions.SetActive(false);
    }

    public void GoToCodeView()
    {
        appView.SetActive(true);
        settingsView.SetActive(false);
        codeView.SetActive(true);
        mainOptions.SetActive(false);
    }

    public void GoBackMainOptions()
    {
        settingsView.SetActive(false);
        codeView.SetActive(false);
        appView.SetActive(false);
        mainOptions.SetActive(true);
    }

    public void GoBackAppView()
    {
        settingsView.SetActive(false);
        codeView.SetActive(false);
        appView.SetActive(true);
        mainOptions.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BunkerInside");
    }

    public void GetConnectionCode() 
    {
        APIRequests.CreateNewConnection(ShowCode, GoBackAppView);
    }

    private void ShowCode()
    {
        UnJasonedData data = APIRequests.response;
        GoToCodeView();
        connectionCodeText.text = data.unity_connection_code.ToString();
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
        if (healthScript != null)
        {
            float currentHealth = healthScript.GetCurrentHealth(); // Get health from the HealthScript
            CheckPlayerDeath(currentHealth);
        }
    }

    public void LeaveToMainMenu()
    {
        Debug.Log("Leaving to main menu");
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetGame()
    {
        Debug.Log("Reset game");
        SceneManager.LoadScene("BunkerInside");
    }
    
    private void CheckPlayerDeath(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            ShowDeathScreen();
        }
    }
    
    private void ShowDeathScreen()
    {
        deathMenu.SetActive(true);
        Weapons.SetActive(false);
        menu = Menu.DeathMenu;
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
