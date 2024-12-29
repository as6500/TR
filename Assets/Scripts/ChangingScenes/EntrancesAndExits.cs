using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrancesAndExits: MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private EEntranceType whereToLoadTo;
    [SerializeField] private EBuildings whereToLoadToBuildings;
    [SerializeField] private EFloors whereToLoadToFloors;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene(sceneName);
        
        collision.GetComponent<MapChangingInfo>().entranceTypeToFind = whereToLoadTo;
        collision.GetComponent<MapChangingInfo>().buildingTypeToFind = whereToLoadToBuildings;
        collision.GetComponent<MapChangingInfo>().floorToFind = whereToLoadToFloors;
        
    }

    public EEntranceType WhereToLoadTo()
    {
        return whereToLoadTo;
    }
}
