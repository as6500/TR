using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject stick;

    [Header("Main Weapon Settings")]
    [SerializeField] private float weaponDist;

    private enum MainWeapon {Pistol, Stick};
    private MainWeapon mainWeapon;
    private GameObject tempMainWeapon;

    private WeaponsUiManager ui;
    private Pistol pistolScript;

    private void Start()
    {
        pistolScript = pistol.GetComponent<Pistol>();
        ui = GameObject.FindGameObjectWithTag("WeaponsUI").GetComponent<WeaponsUiManager>();

        mainWeapon = MainWeapon.Pistol;
        ChangeUI();
        SetMainWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
            ResetRelodingPistol();
            ChangeUI();
            SetMainWeapon();
        }

        ChangeMainWeaponSettings();
    }

    private void ChangeMainWeaponSettings()
    {
        tempMainWeapon.transform.rotation = Quaternion.Euler(0, 0, WeaponAng());

        tempMainWeapon.transform.position = ChangePosition();

        pistolScript.SetSpriteAng(WeaponAng());
    }

    private void ChangeWeapon()
    {
        switch (mainWeapon)
        {
            case MainWeapon.Pistol:
                mainWeapon = MainWeapon.Stick;
                break;
            case MainWeapon.Stick:
                mainWeapon = MainWeapon.Pistol;
                break;
        }
    }

    private void SetMainWeapon()
    {
        if(MainWeapon.Pistol == mainWeapon)
        {
            tempMainWeapon = pistol;
            pistol.SetActive(true);
            stick.SetActive(false);
        }
        else
        {
            tempMainWeapon = stick;
            stick.SetActive(true);
            pistol.SetActive(false);
        }
    }

    public float WeaponAng()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(playerP);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        return angle;
    }

    private Vector3 ChangePosition()
    {
        Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        Vector3 sub = mouseP - playerP;

        Vector3 mult = Norm(sub) * weaponDist;

        tempMainWeapon.transform.localScale = ChangeSideMainWeapon((tempMainWeapon.transform.position - playerP).x);

        return new Vector3((playerP + mult).x, (playerP + mult).y, 0);
    }

    private Vector3 ChangeSideMainWeapon(float sub)
    {
        float scaleY = tempMainWeapon.transform.localScale.y;

        if (sub < 0)
        {
            if (scaleY > 0) scaleY *= -1;
        }
        else
        {
            if (scaleY < 0) scaleY *= -1;
        }

        return new Vector3(tempMainWeapon.transform.localScale.x, scaleY, tempMainWeapon.transform.localScale.z);
    }

    private void ResetRelodingPistol()
    {
        pistolScript.ResetReload();
    }

    private void ChangeUI()
    {
        int weaponNum = 1;

        if (MainWeapon.Stick == mainWeapon)
        {
            weaponNum = 1;
        }

        ui.UIChanged(weaponNum);
    }

    private Vector3 Norm(Vector3 vec)
    {
        float mag = Mag(vec);

        if (mag != 0)
        {
            return vec / mag;
        }
        return vec;
    }

    private float Mag(Vector3 vec)
    {
        return Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y));
    }
}
