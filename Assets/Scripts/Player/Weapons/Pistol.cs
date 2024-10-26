using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Pistol Settings")]
    [SerializeField] private float rechargeDelaySeconds = 0.1f;
    [SerializeField] private GameObject[] mag;
    private bool reloading = false;

    [Header("Pistol Sprites")]
    [SerializeField] private GameObject horizontalSprite;
    [SerializeField] private GameObject verticalSprite;
    [SerializeField] private GameObject diagonalSprite;

    [Header("Bullets")]
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject bulletOrigin;
    [SerializeField] private GameObject bulletOriginHPosition;
    [SerializeField] private GameObject bulletOriginDPosition;
    [SerializeField] private GameObject storeBullets;
    private int bulletsMag;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem particles;

    [Header("FlashLight")]
    [SerializeField] private GameObject flashLight;

    [Header("UI")]
    [SerializeField] private BulletsUIScript pocketBulletsUI;
    [SerializeField] private PistolUI magBulletsUI;

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
        if (angle > 22.5f && angle < 67.5f || angle < -112.5f && angle > -157.5f || angle > 112.5f && angle < 157.5f || angle < -22.5f && angle > -67.5f)
        {
            horizontalSprite.SetActive(false);
            verticalSprite.SetActive(false);
            diagonalSprite.SetActive(true);

            bulletOrigin.transform.position = bulletOriginDPosition.transform.position;
        }
        else if (angle > 67.5f && angle < 112.5f || angle < -67.5f && angle > -112.5f)
        {
            horizontalSprite.SetActive(false);
            verticalSprite.SetActive(true);
            diagonalSprite.SetActive(false);

            bulletOrigin.transform.position = flashLight.transform.position;
        }
        else
        {
            horizontalSprite.SetActive(true);
            verticalSprite.SetActive(false);
            diagonalSprite.SetActive(false);
            bulletOrigin.transform.position = bulletOriginHPosition.transform.position;
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
                mag[i] = Instantiate(prefabBullet, storeBullets.transform);
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
                    particles.Play();       
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
}
