using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Pistol Settings")]
    [SerializeField] private float rechargeDelaySeconds = 0.1f;
    [SerializeField] private float pistolDist = 1.5f;
    [SerializeField] private GameObject[] mag;
    private bool reloading = false;

    [Header("Pistol Sprites")]
    [SerializeField] private GameObject horizontalSprite;
    [SerializeField] private GameObject verticalSprite;

    [Header("Bullets")]
    [SerializeField] private GameObject prefabBullets;
    [SerializeField] private GameObject storeBullets;
    private int bulletsMag;

    [Header("UI")]
    [SerializeField] private BulletsUIScript pocketBulletsUI;
    [SerializeField] private PistolUI magBulletsUI;

    [Header("FlashLight")]
    [SerializeField] private GameObject flashLight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            ChangeFlashlightState();
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(FillPistolMag());
        }

        FlashLightAngle();
    }

    public void ResetReload()
    {
        reloading = false;
    }

    private void FlashLightAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(playerP);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        flashLight.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void SetSpriteAng(float angle)
    {
        if ((angle > 45 && angle < 135) || (angle < -45 && angle > -135))
        {
            horizontalSprite.SetActive(false);
            verticalSprite.SetActive(true);
        }
        else
        {
            horizontalSprite.SetActive(true);
            verticalSprite.SetActive(false);
        }
    }

    private IEnumerator FillPistolMag()
    {
        reloading = true;

        for (int i = 0; i < mag.Length; i++)
        {
            if (mag[i] == null && pocketBulletsUI.PocketBullets() > 0)
            {
                yield return new WaitForSeconds(rechargeDelaySeconds);
                mag[i] = Instantiate(prefabBullets, storeBullets.transform);
                pocketBulletsUI.AddOrRmvBullets(-1);
                AddOrRmvBullets(1);
                ChangeUIText();
            }
        }

        ResetReload();
    }

    private void Shoot()
    {
        if (MagBullets() > 0)
        {
            for (int i = 0; i < mag.Length; i++)
            {
                if (mag[i] != null && !mag[i].activeSelf)
                {
                    StartBullet(i);
                    ChangeUIText();
                    AddOrRmvBullets(-1);
                    break;
                }
            }
        }
    }

    private void StartBullet(int i)
    {
        mag[i].SetActive(true);
        mag[i] = null;
    }

    private void ChangeUIText()
    {
        magBulletsUI.ChangeText();
    }

    public int MagBullets()
    {
        bulletsMag = 0;
        for (int i = 0; i < mag.Length; i++)
        {
            if (mag[i] != null)
            {
                bulletsMag++;
            }
        }
        return bulletsMag;
    }

    private void AddOrRmvBullets(int qnt)
    {
        bulletsMag += qnt;
    }

    private void ChangeFlashlightState()
    {
        bool tempState = !flashLight.activeSelf;
        flashLight.SetActive(tempState);
    }

    //Funções bué crazy

    private Vector3 Norm(Vector3 vec)
    {
        float mag = Mag(vec);

        if (mag != 0)
        {
            return vec/mag;
        }
        return vec;
    }

    private float Mag(Vector3 vec)
    {
        return Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y));
    }
}
