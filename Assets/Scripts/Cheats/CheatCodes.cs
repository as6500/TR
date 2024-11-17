using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] private AntiRadiationScript antiRadiationScript;
    [SerializeField] private PillsScript pillsScript;
    [SerializeField] private PillsUIScript pillsUIScript;
    [SerializeField] private AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) //change scene to inside the building in the city
        {
            SceneManager.LoadScene("Building1");
            transform.position = new Vector3(-17, -3, 0);
        }

        if (Input.GetKeyDown(KeyCode.V)) //change scene to the entry of the city 
        {
            SceneManager.LoadScene("City");
            transform.position = new Vector3(43, 9, 0);
        }

        if (Input.GetKeyDown(KeyCode.B)) //change scene to right outside the entrance of the bunker
        {
            SceneManager.LoadScene("BunkerInside");
            transform.position = new Vector3(-6, 2, 0);
        }

        if (Input.GetKeyDown(KeyCode.N)) //adds 80 more pills and anti-radiation flasks to the player
        {
            antiRadiationScript.UpdateFlasks(80);
            pillsScript.UpdatePills(80);
        }
    }
}
