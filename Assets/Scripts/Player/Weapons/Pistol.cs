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
    [SerializeField] private GameObject[] bullets;

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

        PistolPosition();
        FlashLightAngle();
        GunAng();
    }

    private void PistolPosition()
    {
        Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        Vector3 sub = mouseP - playerP;

        Vector3 mult = Norm(sub) * pistolDist;

        transform.position = new Vector3((playerP + mult).x, (playerP + mult).y, 0);

        sub = transform.position - playerP;

        ChangeSide(sub.x);
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

    private void ChangeSide(float sub)
    {
        float scaleY = transform.localScale.y;


        if (sub < 0)
        {
            if (scaleY > 0)
            {
                scaleY *= -1;
            }
        }
        else
        {
            if (scaleY < 0)
            {
                scaleY *= -1;
            }
        }

        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    private void GunAng()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(playerP);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        SetSpriteAng(angle);
    }

    private void SetSpriteAng(float angle)
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

        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] == null && pocketBulletsUI.PocketBullets() > 0)
            {
                yield return new WaitForSeconds(rechargeDelaySeconds);
                bullets[i] = Instantiate(prefabBullets, storeBullets.transform);
                pocketBulletsUI.AddOrRmvBullets(-1);
                AddOrRmvBullets(1);
                ChangeUIText();
            }
        }
        
        reloading = false;
    }

    private void Shoot()
    {
        if (MagBullets() > 0)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i] != null && !bullets[i].activeSelf)
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
        bullets[i].SetActive(true);
        bullets[i] = null;
    }

    private void ChangeUIText()
    {
        magBulletsUI.ChangeText();
    }

    public int MagBullets()
    {
        bulletsMag = 0;
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] != null)
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

    Vector3 Norm(Vector3 vec)
    {
        float mag = Mag(vec);

        if (mag != 0)
        {
            return vec/mag;
        }
        return vec;
    }

    float Mag(Vector3 vec)
    {
        return Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y));
    }
}
