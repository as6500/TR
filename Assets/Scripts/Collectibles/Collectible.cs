using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleParent { World, Enemy }
public enum CollectibleType { Pills, Ammo }

public class Collectible : MonoBehaviour, ICollectible
{
    private CollectibleParent parent;
    [SerializeField] private CollectibleType type;

    [SerializeField] private int maxDrop = 1;
    [SerializeField] private int minDrop = 1;
    [SerializeField] private int drop = 0;

    void Start()
    {
        drop = Random.Range(minDrop, maxDrop);
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
        Destroy(gameObject);
    }

    public virtual void SetParent(CollectibleParent parent)
    {
        this.parent = parent;
    }

    public CollectibleParent GetParent()
    {
        return parent;
    }
}
