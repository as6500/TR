using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemieAttack : MonoBehaviour
{
    [Header("Detect Player Settings")]
    [SerializeField] private CircleCollider2D coll;
    [SerializeField] private float radius;
    [SerializeField] private bool attack;

    void Start()
    {
        
    }

    void Update()
    {
        coll.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            attack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attack = false;
        }
    }

}
