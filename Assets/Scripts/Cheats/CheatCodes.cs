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
    [SerializeField] private QuestManager questManager;
    private GameObject tempTreasure;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) //change scene to the City
        {
            entrancesAndExits.GoToScene("City",EEntranceType.CityRoad, EBuildings.INVALID);
        }

        if (Input.GetKeyDown(KeyCode.B)) //change scene to Bunker
        {
            entrancesAndExits.GoToScene("BunkerInside",EEntranceType.BunkerOutside, EBuildings.INVALID);
        }

        if (Input.GetKeyDown(KeyCode.K)) //change scene to Road
        {
            entrancesAndExits.GoToScene("Road",EEntranceType.RoadOutside, EBuildings.INVALID);
        }

        if (Input.GetKeyDown(KeyCode.I)) //change scene to Farm
        {
            entrancesAndExits.GoToScene("Farm", EEntranceType.FarmRoad, EBuildings.INVALID);
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

        if (Input.GetKeyDown(KeyCode.X))
            questManager.activeQuestState = QuestState.Completed;
    }
}
