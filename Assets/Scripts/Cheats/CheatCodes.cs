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
        if (Input.GetKeyDown(KeyCode.C)) //change scene to inside the building in the city
        {
            SceneManager.LoadScene("Building1");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.V)) //change scene to the entry of the city 
        {
            SceneManager.LoadScene("City");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.B)) //change scene to bunker
        {
            SceneManager.LoadScene("BunkerInside");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Road");
            entrancesAndExits.WhereToLoadTo();
        }

        if (Input.GetKeyDown(KeyCode.I))
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
