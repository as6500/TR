using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] private AntiRadiationScript antiRadiationScript;
    [SerializeField] private PillsScript pillsScript;
    [SerializeField] private PillsUIScript pillsUIScript;
    [SerializeField] private AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;
    [SerializeField] private GameObject treasure;
    [SerializeField] private EntrancesAndExits entrancesAndExits;
    private GameObject tempTreasure;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) //change scene to the City
        {
            SceneManager.LoadScene("City");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.B)) //change scene to Bunker
        {
            SceneManager.LoadScene("BunkerInside");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.K)) //change scene to Road
        {
            SceneManager.LoadScene("Road");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.I)) //change scene to Farm
        {
            SceneManager.LoadScene("Farm");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.N)) //adds 80 more pills and anti-radiation flasks to the player
        {
            antiRadiationScript.UpdateFlasks(80);
            pillsScript.UpdatePills(80);
        }

        if (Input.GetKeyDown(KeyCode.L)) // treasure found and activates the treasure
        {
            if (tempTreasure == null && SceneManager.GetActiveScene().name == "BunkerOutside")
            {
                tempTreasure = Instantiate(treasure);
                tempTreasure.transform.position = new Vector3(-25, 5, 0);
            }
        }
    }
}
