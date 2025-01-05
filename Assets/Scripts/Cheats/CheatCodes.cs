using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] private AntiRadiationScript antiRadiationScript;
    [SerializeField] private PillsScript pillsScript;
    [SerializeField] private PillsUIScript pillsUIScript;
    [SerializeField] private AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;
    [SerializeField] private EntrancesAndExits entrancesAndExits;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private PocketBullets bullets;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) //change scene to the City
        {
            StartCoroutine(entrancesAndExits.GoToScene("City", EEntranceType.CityRoad, EBuildings.INVALID));
        }

        if (Input.GetKeyDown(KeyCode.B)) //change scene to Bunker
        {
            StartCoroutine(entrancesAndExits.GoToScene("BunkerInside",EEntranceType.BunkerOutside, EBuildings.INVALID));
        }

        if (Input.GetKeyDown(KeyCode.K)) //change scene to Road
        {
            StartCoroutine(entrancesAndExits.GoToScene("Road",EEntranceType.RoadOutside, EBuildings.INVALID));
        }

        if (Input.GetKeyDown(KeyCode.I)) //change scene to Farm
        {
            StartCoroutine(entrancesAndExits.GoToScene("Farm", EEntranceType.FarmRoad, EBuildings.INVALID));
        }

        if (Input.GetKeyDown(KeyCode.O)) //change scene to Outside
        {
            StartCoroutine(entrancesAndExits.GoToScene("BunkerOutside", EEntranceType.BunkerOutside, EBuildings.INVALID));
        }

        if (Input.GetKeyDown(KeyCode.N)) //adds 80 more pills and anti-radiation flasks to the player
        {
            antiRadiationScript.UpdateFlasks(80);
            pillsScript.UpdatePills(80);
            bullets.UpdateBullets(100);
        }
    }
}
