using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapDoorTrigger : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene("Scenes/MapInside/BunkerInsideBuildings");
        collision.transform.position = new Vector3(-6, 2, 0);
    }
}
