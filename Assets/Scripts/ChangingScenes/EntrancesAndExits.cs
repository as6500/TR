using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrancesAndExits: MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private EEntranceType WhereToLoadTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene(sceneName);

        collision.GetComponent<MapChangingInfo>().EntranceTypeToFind = WhereToLoadTo;
    }
}
