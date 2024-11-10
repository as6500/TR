using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public virtual void TakeDamage(GameObject instigator, float damage)
    {
        Debug.Log("TakeDamage Interface Not Implemented");
    }

    public virtual Color GetBloodColor()
    {
        Color bloodColor = Color.white;
        Debug.Log("BloodColor Interface Not Implemented");
        return bloodColor;
    }
}
