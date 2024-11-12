using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleParent { World, Enemy }

public class Ammo : MonoBehaviour, ICollectible
{
    private CollectibleParent parent;
    [SerializeField] private int maxAmmo = 15;
    [SerializeField] private int minAmmo = 5;
    [SerializeField] private int ammo = 0;

    void Start()
    {
        ammo = Random.Range(minAmmo, maxAmmo);
    }

    public virtual void Collect(GameObject instigator)
    {
        PocketBullets pocketBullets = GameObject.FindGameObjectWithTag("Consumables").GetComponent<PocketBullets>();
        pocketBullets.AddOrRmvBullets(ammo);
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
