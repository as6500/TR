using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantTreeAttack : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator anim;

    [Header("Detect Player Settings")]
    [SerializeField] private CircleCollider2D coll;
    [SerializeField] private float radius;
    [SerializeField] private bool attack;

    [Header("Sap")]
    [SerializeField] private GameObject sap;
    [SerializeField] private GameObject sapHolder;
    [SerializeField] private GameObject sapOrigin;
    [SerializeField] private float shootDelaySeconds;

    void Update()
    {
        coll.radius = radius;
    }

    private void ShootSap()
    {
        GameObject tempSap = Instantiate(sap, sapHolder.transform);
        tempSap.GetComponent<Sap>().SapSetup(sapOrigin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("idle", false);
            anim.SetBool("attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("idle", true);
            anim.SetBool("attack", false);
        }
    }

}
