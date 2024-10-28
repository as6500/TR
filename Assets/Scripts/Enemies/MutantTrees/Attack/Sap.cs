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

    private Vector3 bulletVelocity = Vector3.zero;

    private void FixedUpdate()
    {
        rb.velocity = bulletVelocity;
    }

    public void SapSetup(GameObject origin)
    {
        sapOrigin = origin;
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - sapOrigin.transform.position;
        SapAng(direction);
        bulletVelocity = SapDirection(direction);
        StartCoroutine(KillSap());
    }

    private IEnumerator KillSap()
    {
        yield return new WaitForSeconds(lifeTimeSeconds);
        Destroy(gameObject);
    }

    private void SapAng(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    private Vector3 SapDirection(Vector3 dir)
    {
        transform.position = sapOrigin.transform.position;

        Vector3 norm = Norm(dir);

        return norm * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthScript playerHealth = collision.GetComponent<HealthScript>();
            playerHealth.DealDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
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
