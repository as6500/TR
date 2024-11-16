using UnityEngine;


public class QuestTypeLocation : MonoBehaviour
{
    [SerializeField] private bool onLocation;
    
    private void Start()
    {
        onLocation = false;
        SceneManagement.Instance.AddObjectToScene(gameObject, "2ndFloorBuilding1");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) 
            return;
        
        onLocation = true;
        QuestManager.OnQuestAction.Invoke();
    }
    
    public bool OnLocation()
    {
        return onLocation;
    }
}