using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject storeBullets;
    [SerializeField] private GameObject flashLight;

    [SerializeField] private GameObject rightSide;
    [SerializeField] private GameObject leftSide;
    [SerializeField] private GameObject topSide;
    [SerializeField] private GameObject downSide;

    [SerializeField] private BulletsUIScript pocketBullsUI;
    [SerializeField] private PistolUI bulletsMagUI;
    [SerializeField] private GameObject[] bullets;
    private int bulletsMag;

    //para as funções crazy
    private float pistolDist = 1.5f;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(FillPistolMag());
        }

        PistolPosition();
        GunAng();
    }

    private void PistolPosition()
    {
        Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        Vector3 sub = mouseP - playerP;

        Vector3 mult = Norm(sub) * pistolDist;

        transform.position = new Vector3((playerP + mult).x, (playerP + mult).y, 0);
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
        if (angle < 45 && angle > -45)
        {
            rightSide.SetActive(true);
            topSide.SetActive(false);
            leftSide.SetActive(false);
            downSide.SetActive(false);
        }
        else if (angle > 45 && angle < 135)
        {
            rightSide.SetActive(false);
            topSide.SetActive(true);
            leftSide.SetActive(false);
            downSide.SetActive(false);
        }
        else if (angle < -45 && angle > -135)
        {
            rightSide.SetActive(false);
            topSide.SetActive(false);
            leftSide.SetActive(false);
            downSide.SetActive(true);
        }
        else
        {
            rightSide.SetActive(false);
            topSide.SetActive(false);
            leftSide.SetActive(true);
            downSide.SetActive(false);
        }
    }

    private IEnumerator FillPistolMag()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] == null && pocketBullsUI.PocketBullets() > 0)
            {
                yield return new WaitForSeconds(0.1f);
                bullets[i] = Instantiate(bulletPrefab, storeBullets.transform);
                pocketBullsUI.AddOrRmvBullets(-1);
                AddOrRmvBullets(1);
                ChangeUIText();
            }
        }
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
        bulletsMagUI.ChangeText();
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
