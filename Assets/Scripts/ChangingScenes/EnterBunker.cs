using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBunker : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene("Scenes/MapInside/BunkerInside");
        collision.transform.position = new Vector3(-6, 2, 0);
    }
}
