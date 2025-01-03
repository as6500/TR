using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleParent { World, Enemy }
public enum CollectibleType { Pills, Ammo, AntiRadiationFlasks }

public class Collectible : MonoBehaviour, ICollectible
{
    private CollectibleParent parent;
    [SerializeField] private CollectibleType type;
    [SerializeField] private QuestManager manager;

    [SerializeField] private int maxDrop = 1;
    [SerializeField] private int minDrop = 1;
    [SerializeField] private int drop = 0;

    void Start()
    {
        drop = Random.Range(minDrop, maxDrop);

        manager = FindFirstObjectByType<QuestManager>();
    }

    public virtual void Collect(GameObject instigator)
    {
        if (type == CollectibleType.Ammo) 
        {
            PocketBullets pocketBullets = GameObject.FindGameObjectWithTag("Consumables").GetComponent<PocketBullets>();
            pocketBullets.UpdateBullets(drop);
        }
        else
        {
            PillsScript pills = GameObject.FindGameObjectWithTag("Consumables").GetComponent<PillsScript>();
            pills.UpdatePills(drop);
        }

        if (manager.activeQuest.type == QuestType.Resource)
        {
            CollectibleType temp = (CollectibleType) manager.activeQuest.typeParam;

            if (parent == CollectibleParent.Enemy && type == temp)
            {
                QuestManager.OnQuestAction.Invoke();
            }
        }
       
        Destroy(gameObject);
    }

    public virtual void SetParent(CollectibleParent parent)
    {
        this.parent = parent;
    }
}
