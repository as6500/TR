using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Sap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private int lifeTimeSeconds;

    [Header("Objects Required")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject sapOrigin;
    [SerializeField] private GameObject player;

    [Header ("Bullet destroyed Particle System")]
    [SerializeField] private ParticleSystem particleSystem;


    private Vector3 sapVelocity = Vector3.zero;

    private void FixedUpdate()
    {
        rb.velocity = sapVelocity;
    }

    public void SapSetup(GameObject origin)
    {
        sapOrigin = origin;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.Euler(0, 0, SapAng() + 90);
        sapVelocity = SapDirection();
        StartCoroutine(KillSap(lifeTimeSeconds));
    }

    private IEnumerator KillSap(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private float SapAng()
    {
        Vector3 dir = player.transform.position - sapOrigin.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    private Vector3 SapDirection()
    {
        Vector3 dir = player.transform.position - sapOrigin.transform.position;

        transform.position = sapOrigin.transform.position;

        Vector3 norm = Norm(dir);

        return norm * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.isTrigger)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (damageable != null && enemyHealth == null)
            {
                damageable.TakeDamage(gameObject, damage, 50);
                BulletDestroyedEffect(damageable.GetBloodColor(), 5);
            }
            else
            {
                BulletDestroyedEffect(Color.white);
            }
        }
    }

    private void BulletDestroyedEffect(Color particleColor, int speed = 0)
    {
        sapVelocity = Vector3.zero;
        gameObject.GetComponent<Renderer>().enabled = false;

        if (speed > 0)
        {
            particleSystem.startSpeed = speed;
        }

        particleSystem.startColor = particleColor;
        particleSystem.transform.position = transform.position;
        particleSystem.transform.rotation = Quaternion.Euler(0, 0, SapAng());
        particleSystem.Play();
        StartCoroutine(KillSap(particleSystem.main.duration * 2));
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
