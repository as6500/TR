using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WeaponManager : MonoBehaviour
{
    [Header("Player Object")]
    [SerializeField] private GameObject player;

    [Header("Weapons")]
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject stick;

    [Header("Main Weapon Settings")]
    [SerializeField] private float weaponDist;

    private enum MainWeapon {Pistol, Stick};
    [SerializeField] private MainWeapon mainWeapon;
    private GameObject tempMainWeapon;

    private WeaponsUiManager ui;
    private Pistol pistolScript;

    private void Start()
    {
        pistolScript = pistol.GetComponent<Pistol>();
        ui = GameObject.FindGameObjectWithTag("WeaponsUI").GetComponent<WeaponsUiManager>();

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
        float anglePlayerToMouse = WeaponAng();

        tempMainWeapon.transform.rotation = Quaternion.Euler(0, 0, anglePlayerToMouse);
        tempMainWeapon.transform.position = ChangePosition(anglePlayerToMouse);
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
        float angle;

        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(player.transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        if (mainWeapon == MainWeapon.Stick)
        {
            if (angle < 22.5f && angle > -22.5f)
            {
                angle = 0;
            }
            else if (angle > 22.5f && angle < 67.5f)
            {
                angle = 45;
            }
            else if (angle > 67.5f && angle < 112.5f)
            {
                angle = 90;
            }
            else if (angle > 112.5f && angle < 157.5f)
            {
                angle = 135;
            }
            else if (angle < -22.5f && angle > -67.5f)
            {
                angle = -45;
            }
            else if (angle < -67.5f && angle > -112.5f)
            {
                angle = -90;
            }
            else if (angle < -112.5f && angle > -157.5f)
            {
                angle = -135;
            }
            else
            {
                angle = 180;
            }
        }
        else
        {
            pistolScript.SetSpriteAng(angle);
        }

        return angle;
    }
    
    private Vector3 ChangePosition(float angle)
    {
        if(mainWeapon == MainWeapon.Pistol)
        {
            return ChangePositionPistol();
        }
        else
        {
            return ChangePositionStick(angle);
        }
    }

    private Vector3 ChangePositionStick(float angle)
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        Vector3 tempDirection = Norm(new Vector3(weaponDist, weaponDist, 0));

        if (angle == 0)
        {
            x += weaponDist;
        }
        else if (angle == 45)
        {
            x += tempDirection.x*weaponDist;
            y += tempDirection.y*weaponDist;
        }
        else if (angle == 90)
        {
            y += weaponDist;
        }
        else if (angle == 135)
        {
            x -= tempDirection.x * weaponDist;
            y += tempDirection.y * weaponDist;
        }
        else if (angle == -45)
        {
            x += tempDirection.x * weaponDist;
            y -= tempDirection.y * weaponDist;
        }
        else if (angle == -90)
        {
            y -= weaponDist;
        }
        else if (angle == -135)
        {
            x -= tempDirection.x * weaponDist;
            y -= tempDirection.y * weaponDist;
        }
        else
        {
            x -= weaponDist;
        }

        tempMainWeapon.transform.localScale = ChangeSideMainWeapon((tempMainWeapon.transform.position - player.transform.position).x);

        return new Vector3(x, y, player.transform.position.z);
    }

    private Vector3 ChangePositionPistol()
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
            weaponNum = 2;
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
