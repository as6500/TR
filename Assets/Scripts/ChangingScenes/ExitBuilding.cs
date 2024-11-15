using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitBuilding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scenes/MapOutside/City");
            collision.transform.position = new Vector3(35, -3, 0);
        }
    }
}
