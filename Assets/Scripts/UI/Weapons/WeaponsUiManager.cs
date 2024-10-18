using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsUiManager : MonoBehaviour
{
    private int pistolNum = 1;
    private int stickNum = 2;

    [SerializeField] private GameObject pistolMain;
    [SerializeField] private GameObject stickMain;

    [SerializeField] private GameObject pistolSec;
    [SerializeField] private GameObject stickSec;

    void Start()
    {
        MainWeaponChanged(1);
    }

    public void MainWeaponChanged(int weapon)
    {
        if (pistolNum == weapon)
        {
            pistolMain.SetActive(true);
            stickSec.SetActive(true);

            pistolSec.SetActive(false);
            stickMain.SetActive(false);
        }
        else if(stickNum == weapon)
        {
            pistolMain.SetActive(false);
            stickSec.SetActive(false);

            pistolSec.SetActive(true);
            stickMain.SetActive(true);
        }
        else
        {
            Debug.LogError("No Weapon Selected");
        }
    }

    void Update()
    {
        
    }
}
