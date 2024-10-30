using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    public virtual void Collect(GameObject instigator)
    {
        Debug.Log("Collectible Interface Not Implemented");
    }
}
