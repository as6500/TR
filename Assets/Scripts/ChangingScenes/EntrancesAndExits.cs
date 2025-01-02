using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrancesAndExits: MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private EEntranceType whereToLoadTo;
    [SerializeField] private EBuildings whereToLoadToBuildings;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        GoToScene(sceneName, whereToLoadTo, whereToLoadToBuildings);

    }
    
    public EEntranceType WhereToLoadTo()
    {
        return whereToLoadTo;
    }

    public void GoToScene(String nameOfScene, EEntranceType place, EBuildings buildings)
    {
        SceneManager.LoadScene(nameOfScene);
        
        player.GetComponent<MapChangingInfo>().entranceTypeToFind = place;
        player.GetComponent<MapChangingInfo>().buildingTypeToFind = buildings;
    }
}
