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
        currentScene = SceneManager.GetActiveScene().name;
        if (SceneChanged())
        {
            if (RightScene())
            {
                antiRadiationScript.StopCoroutines();
            }
            else
            {
                antiRadiationScript.StartCoroutine(antiRadiationScript.RadiationDamage());
            }
        }
    }

    private bool SceneChanged()
    {
        if (outDatedScene != currentScene)
        {
            outDatedScene = currentScene;
            return true;
        }
        return false;
    }

    private bool RightScene()
    {
        return SceneManager.GetActiveScene().name == "BunkerInside";
    }
}
