using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheCity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SceneManager.LoadScene("Scenes/MapOutside/City");
        collision.transform.position = new Vector3(43, 9, 0);
    }
}
