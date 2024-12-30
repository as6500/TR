using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrancesAndExits: MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private EEntranceType whereToLoadTo;
    [SerializeField] private EBuildings whereToLoadToBuildings;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene(sceneName);
        
        collision.GetComponent<MapChangingInfo>().entranceTypeToFind = whereToLoadTo;
        collision.GetComponent<MapChangingInfo>().buildingTypeToFind = whereToLoadToBuildings;
    }
    
    public EEntranceType WhereToLoadTo()
    {
        return whereToLoadTo;
    }
}
