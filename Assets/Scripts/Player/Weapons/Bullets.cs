using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 30.0f;
    [SerializeField] private float lifeTime = 5.0f;
    [SerializeField] private float damage = 10f;

    private Vector3 bulletVelocity = Vector3.zero;
    private bool dying = false;
    private Rigidbody2D rb;

    [Header("Bullet Origin")]
    [SerializeField] private GameObject startPoint;

    [Header("Particle System Bullet Destroyed")]
    [SerializeField] private ParticleSystem particleSystem;

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
            transform.rotation = Quaternion.Euler(0, 0, BulletAng() + 90);
            StartCoroutine(KillBullet(lifeTime));
        }
    }

    private float BulletAng()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerP = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(playerP);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        return angle;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);
                BulletDestroyedEffect(enemyHealth.GetBloodColor(), 5);
            }
            else if (collision.CompareTag("Wall"))
            {
                BulletDestroyedEffect(Color.white);
            }
        }
    }

    private void BulletDestroyedEffect(Color particleColor, int speed = 0)
    {
        bulletVelocity = Vector3.zero;
        gameObject.GetComponent<Renderer>().enabled = false;

        if (speed > 0)
        {
            particleSystem.startSpeed = speed;  
        }
        particleSystem.startColor = particleColor;

        particleSystem.transform.position = transform.position;
        particleSystem.transform.rotation = Quaternion.Euler(0, 0, BulletAng());
        particleSystem.Play();
        StartCoroutine(KillBullet(particleSystem.main.duration*2));
    }

    private IEnumerator KillBullet(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
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
