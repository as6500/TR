using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadiationOutsideSafeZones : MonoBehaviour
{
    private AntiRadiationScript antiRadiationScript;
    
    private void Start()
    {
        antiRadiationScript = FindObjectOfType<AntiRadiationScript>();

        if (SceneManager.GetActiveScene().name != "BunkerInsideBuildings" && antiRadiationScript.IsCoroutineActive() == false)
            StartCoroutine(antiRadiationScript.RadiationDamage());
    }
}
