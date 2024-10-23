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
    private GameObject mainWeapon;
    private WeaponsUiManager ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("WeaponsUI").GetComponent<WeaponsUiManager>();
        mainWeapon = pistol;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }

        ChangeMainWeaponSettings();
    }

    private void ChangeMainWeaponSettings()
    {
        mainWeapon.transform.rotation = Quaternion.Euler(0, 0, WeaponAng());

        mainWeapon.transform.position = ChangePosition();
    }

    private void ChangeWeapon()
    {
        if (mainWeapon == pistol)
        {
            stick.SetActive(true);
            pistol.SetActive(false);
            mainWeapon = stick;
        }
        else
        {
            stick.SetActive(false);
            pistol.SetActive(true);
            mainWeapon = pistol;
        }

        ResetRelodingPistol();
        ChangeUI();
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

        sub = transform.position - playerP;

        mainWeapon.transform.localScale = ChangeSideMainWeapon(sub.x);

        return new Vector3((playerP + mult).x, (playerP + mult).y, 0);
    }

    private Vector3 ChangeSideMainWeapon(float sub)
    {
        float scaleY = pistol.transform.localScale.y;

        if (sub < 0)
        {
            if (scaleY > 0) scaleY *= -1;
        }
        else
        {
            if (scaleY < 0) scaleY *= -1;
        }

        return new Vector3(pistol.transform.localScale.x, scaleY, pistol.transform.localScale.z);
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
