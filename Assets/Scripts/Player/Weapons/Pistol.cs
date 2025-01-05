using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Pistol Settings")]
    [SerializeField] private float rechargeDelaySeconds = 0.1f;
    [SerializeField] private float shootDelaySeconds = 0.5f;
    private bool canShoot = true;
    private bool reloading = false;

    [Header("Pistol Sprites")]
    [SerializeField] private Sprite horizontalSprite;
    [SerializeField] private Sprite verticalSprite;
    [SerializeField] private Sprite diagonalSprite;

    [Header("Bullet")]
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject bulletOrigin;
    [SerializeField] private GameObject bulletOriginHPosition;
    [SerializeField] private GameObject bulletOriginDPosition;
    [SerializeField] private GameObject storeBullets;
    [SerializeField] private PocketBullets pocketBullets;
    [SerializeField] private int maxBulletsMag = 10;
    [SerializeField] private int bulletsMag;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem particles;

    [Header("FlashLight")]
    [SerializeField] private GameObject flashLight;
    [SerializeField] private GameObject particlesCollider;

    [Header("UI")]
    [SerializeField] private PistolUI magBulletsUI;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    private void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.F))
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
        canShoot = true;
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
            gameObject.GetComponent<SpriteRenderer>().sprite = diagonalSprite;
            bulletOrigin.transform.position = bulletOriginDPosition.transform.position;
        }
        else if (angle > 67.5f && angle < 112.5f || angle < -67.5f && angle > -112.5f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = verticalSprite;
            bulletOrigin.transform.position = flashLight.transform.position;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = horizontalSprite;
            bulletOrigin.transform.position = bulletOriginHPosition.transform.position;
        }
    }

    private IEnumerator FillPistolMag()
    {
        reloading = true;
        canShoot = false;

        int tempCurrentBulls = bulletsMag;

        for (int i = 0; i < maxBulletsMag - tempCurrentBulls; i++)
        {
            if (pocketBullets.GetPocketBullets() > 0)
            {
                yield return new WaitForSeconds(rechargeDelaySeconds);
                pocketBullets.UpdateBullets(-1);
                AddOrRmvBullets(1);
                ChangeUIText();
            }
        }

        ResetReload();
    }

    private void Shoot()
    {
        if (MagBullets() > 0 && canShoot)
        {
            audioManager.shoot.Play();
            StartBullet();
            AddOrRmvBullets(-1);
            particles.Play();
            StartCoroutine(ShootDelay());
            ChangeUIText();
        }
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelaySeconds);
        canShoot = true;
    }

    private void StartBullet(int i = 0)
    {
        Instantiate(prefabBullet, storeBullets.transform);
    }

    private void ChangeUIText()
    {
        magBulletsUI.ChangeText();
    }

    public int MagBullets()
    {
        return bulletsMag;
    }

    private void AddOrRmvBullets(int qnt)
    {
        bulletsMag += qnt;
    }

    private void ChangeFlashlightState()
    {
        bool tempState = !flashLight.activeSelf;

        particlesCollider.SetActive(tempState);
        flashLight.SetActive(tempState);
    }
}
