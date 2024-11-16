using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            SceneManager.LoadScene("Scenes/MapInside/Building1");
        if (Input.GetKeyDown(KeyCode.B)) 
            SceneManager.LoadScene("Scenes/MapInside/BunkerInsideBuildings");
    }
}
