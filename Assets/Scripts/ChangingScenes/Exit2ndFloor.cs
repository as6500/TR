using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit2ndFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scenes/MapInside/Building1");
            collision.transform.position = new Vector3(-16, -2, 0);
        }
    }
}
