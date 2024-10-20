using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject stick;
    private WeaponsUiManager ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("WeaponsUI").GetComponent<WeaponsUiManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon(){

        stick.SetActive(pistol.activeSelf);
        pistol.SetActive(ActiveWeapon(pistol.activeSelf));

        ResetRelodingPistol();
        ChangeUI();
    }

    private void ResetRelodingPistol()
    {
        Pistol pistol = this.pistol.GetComponent<Pistol>();

        pistol.ResetReload();
    }

    private bool ActiveWeapon(bool pistol)
    {
        return !pistol;
    }

    private void ChangeUI()
    {
        int weaponNum = 1;

        if (pistol.activeSelf == false)
        {
            weaponNum = 2;
        }

        ui.UIChanged(weaponNum);
    }
}
