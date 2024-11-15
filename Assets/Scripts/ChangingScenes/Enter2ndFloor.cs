using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter2ndFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene("Scenes/MapInside/2ndFloorBuilding1");
        collision.transform.position = new Vector3(-16, -6, 0);
    }
}
