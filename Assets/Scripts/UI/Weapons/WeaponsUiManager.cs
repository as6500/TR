using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsUiManager : MonoBehaviour
{

    [Header("Pistol Sprites")]
    [SerializeField] private GameObject pistolMain;
    private int pistolNum = 1;

    [Header("Stick Sprites")]
    [SerializeField] private GameObject stickMain;

    public void UIChanged(int weapon)
    {
        ChangeWeapon(weapon);
    }

    private void ChangeWeapon(int weapon)
    {
        bool mainWeaponPistol;

        if (pistolNum == weapon)
        {
            mainWeaponPistol = true;
        }
        else
        {
            mainWeaponPistol = false;
        }

        ApplyChanges(mainWeaponPistol);
    }

    private void ApplyChanges(bool b)
    {
        pistolMain.SetActive(b);
        stickMain.SetActive(!b);
    }
}
