using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject bulletOrigin;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject[] bullets;

    private void Start()
    {
        FillPistolMag();
    }

    private void FillPistolMag()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] == null)
            {
                bullets[i] = Instantiate(bulletPrefab, bulletOrigin.transform);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InstanciateBullets();
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            ChangeFlashlightState();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            FillPistolMag();
        }
    }
    
    private void InstanciateBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] != null && !bullets[i].activeSelf)
            {
                bullets[i].SetActive(true);
                bullets[i] = null;
                break;
            }
        }
    }

    private void ChangeFlashlightState()
    {
        bool tempState = !flashLight.activeSelf;
        flashLight.SetActive(tempState);
    }
}
