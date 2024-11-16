using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit3rdFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scenes/MapInside/2ndFloorBuilding1");
            collision.transform.position = new Vector3(1, 1, 0);
        }
    }
}
