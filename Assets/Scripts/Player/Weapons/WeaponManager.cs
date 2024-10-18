using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject stick;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon(){

        stick.SetActive(pistol.activeSelf);
        pistol.SetActive(ActiveWeapon(pistol.activeSelf));

        ChangeUI();
    }

    private bool ActiveWeapon(bool pistol)
    {
        return !pistol;
    }

    private void ChangeUI()
    {
        WeaponsUiManager ui = GameObject.FindGameObjectWithTag("WeaponsUI").GetComponent<WeaponsUiManager>();

        int weaponNum = 1;

        if (pistol.activeSelf == false)
        {
            weaponNum = 2;
        }

        ui.MainWeaponChanged(weaponNum);
    }
}
