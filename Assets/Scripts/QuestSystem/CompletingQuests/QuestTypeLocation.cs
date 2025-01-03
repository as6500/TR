using UnityEngine;


public class QuestTypeLocation : MonoBehaviour
{
    [SerializeField] private bool onLocation;
    
    private void Start()
    {
        onLocation = false;

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
    
    public void InitiateLocation(QuestData activeQuest)
    {
        SceneManagement.Instance.AddObjectToScene(gameObject, activeQuest.sceneNameForItems);
    }
    
}