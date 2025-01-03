using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLocations : MonoBehaviour
{
    public EBuildings building;

    public static Vector2 FindItemLocation(EBuildings buildingWanted)
    {
        List<ItemLocations> itemLocations = FindObjectsOfType<ItemLocations>().ToList();

        foreach (ItemLocations item in itemLocations)
        {
            if (item.building == buildingWanted)
                return item.transform.position;
        }
        return Vector2.zero;
    }
}
