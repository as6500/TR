using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsUiManager : MonoBehaviour
{
    private int pistolNum = 1;

    [SerializeField] private GameObject pistolMain;
    [SerializeField] private GameObject stickMain;

    [SerializeField] private GameObject pistolSec;
    [SerializeField] private GameObject stickSec;

    void Start()
    {
        UIChanged(1);
    }

    public void UIChanged(int weapon = -1)
    {
        if (weapon != -1)
        {
            ChangeWeapon(weapon);
        }
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
        pistolSec.SetActive(!b);
        stickSec.SetActive(b);
        stickMain.SetActive(!b);
    }

    void Update()
    {
        
    }
}
