using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float lifeTime = 5.0f;
    private bool dying = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (dying == false)
        {
            dying = true;
            StartCoroutine(KillBullets());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0);
    }

    public IEnumerator KillBullets()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
