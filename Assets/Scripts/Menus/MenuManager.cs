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
    private enum Menu {InGameMenu, DeathMenu};

    [Header ("API Connection")]
    [SerializeField] private CreateConnection APIRequests;

    [Header("Choose Menu")]
    [SerializeField] private Menu menu;

    [Header("In-Game Menu")]
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject collectibles;
    [SerializeField] private GameObject settings;

    [Header("Death Menu")]
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject quitGame;
    [SerializeField] private GameObject quitToTitle;
    [SerializeField] private HealthScript healthScript;
    
    private bool isMenuActive = false;

    void Start()
    {
        if (menu == Menu.InGameMenu)
        {
            ChangeGameState(isMenuActive);
        }
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

    private void GamePause()
    {
        Time.timeScale = 0;
    }

    private void GameResume()
    {
        Time.timeScale = 1;
    }

    public void LeaveToMainMenu()
    {
        Debug.Log("Leaving to main menu");
        DestroyObjects();
        SceneManager.LoadScene("Main Menu");
    }
    
    private void DestroyObjects()
     {
         // Get all tracked DontDestroyOnLoad objects
         List<GameObject> trackedObjects = ProjectUtils.GetAllTrackedDontDestroyOnLoadObjects();
         // Iterate through the list and destroy each object
         for (int i = trackedObjects.Count - 1; i >= 0; i--)
         {
             if (trackedObjects[i] != null)
             {
                 Debug.Log($"Destroying tracked object: {trackedObjects[i].name}");
                 ProjectUtils.DestroyOnLoadTracked(trackedObjects[i]);
             }
         }

         // Clear the tracked objects list in ProjectUtils to avoid dangling references
         trackedObjects.Clear();
     }

    public void ResetGame()
    {
        //GameResume();
        Debug.Log("Reset game");
        healthScript.ModifyHealth(100);
        if (deathMenu != null){
            deathMenu.SetActive(false);
        }
        SceneManager.LoadScene("BunkerInside");
    }

    private void CheckPlayerDeath(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            ShowDeathScreen();
            //GamePause();
        }
    }
    
    private void ShowDeathScreen()
    {
        if (deathMenu != null)
        {
            deathMenu.SetActive(true);
        }
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
