using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingFloors : MonoBehaviour
{
    [SerializeField] private GameObject floorToLoadTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        ChangeFloor(collision.gameObject);
    }

    private void ChangeFloor(GameObject player)
    {
        player.transform.position = floorToLoadTo.transform.position;
    }
}
