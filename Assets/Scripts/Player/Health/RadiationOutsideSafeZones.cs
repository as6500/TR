using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadiationOutsideSafeZones : MonoBehaviour
{
    private AntiRadiationScript antiRadiationScript;
    private string currentScene;
    private string outDatedScene;


    private void Start()
    {
        antiRadiationScript = FindObjectOfType<AntiRadiationScript>();
    }

    private void Update()
    {
        if (SceneChanged())
            antiRadiationScript.IsThisTheRightScene();
    }

    private bool SceneChanged()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (outDatedScene != currentScene)
        {
            outDatedScene = currentScene;
            return true;
        }
        return false;
    }

}
