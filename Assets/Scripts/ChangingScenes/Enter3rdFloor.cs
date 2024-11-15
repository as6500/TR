using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter3rdFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene("Scenes/MapInside/3rdFloorBuilding1");
        collision.transform.position = new Vector3(-16, 0, 0);
    }
}
