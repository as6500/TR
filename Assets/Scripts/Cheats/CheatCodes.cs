using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) //change scene to inside of the building in the city
        {
            SceneManager.LoadScene("Building1");
            transform.position = new Vector3(-17, -3, 0);
        }

        if (Input.GetKeyDown(KeyCode.V)) //change scene to the entry of the city 
        {
            SceneManager.LoadScene("City");
            transform.position = new Vector3(43, 9, 0);
        }

        if (Input.GetKeyDown(KeyCode.B)) //change scene to right outside of the enterance of the bunker
        {
            SceneManager.LoadScene("Bunker");
            transform.position = new Vector3(-11, 2, 0);
        }
    }
}
