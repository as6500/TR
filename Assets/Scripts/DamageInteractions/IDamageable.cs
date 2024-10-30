using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public virtual void TakeDamage(GameObject instigator)
    {
        Debug.Log("TakeDamage Interface Not Implemented");
    }
}
