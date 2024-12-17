using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    private enum Menu {MainMenu, InGameMenu, DeathMenu};

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
