using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [Header("Bullets Settings")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float lifeTime = 5.0f;
    private Vector3 bulletVelocity = Vector3.zero;
    private bool dying = false;
    private Rigidbody2D rb;

    [Header("Bullets Origin")]
    [SerializeField] private GameObject startPoint;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startPoint = GameObject.FindGameObjectWithTag("BulletOrigin");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (dying == false)
        {
            dying = true;
            bulletVelocity = BulletDirection();
            BulletAng();
            StartCoroutine(KillBullet());
        }
    }

    private void BulletAng()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(playerP);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    private Vector3 BulletDirection()
    {
        transform.position = startPoint.transform.position;
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 objectP = Camera.main.WorldToScreenPoint(playerP);

        Vector3 sub =  mousePos - objectP;

        Vector3 norm = Norm(sub);

        return norm * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = bulletVelocity;
    }

    private IEnumerator KillBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    //Funções bue fixes
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
