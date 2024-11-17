using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene("Scenes/MapOutside/BunkerOutside");
        collision.transform.position = new Vector3(-38, 2, 0);
    }
}
