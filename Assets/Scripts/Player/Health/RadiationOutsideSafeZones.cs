using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationOutsideSafeZones : MonoBehaviour
{
    [SerializeField] private AntiRadiationScript antiRadiationScript;

    private void Update()
    {
        StartCoroutine(antiRadiationScript.RadiationDamage());
    }
}
