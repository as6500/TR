using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D coll;

    // Start is called before the first frame update
    private void Start()
    {
        coll.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackAnimation(true);
        }
    }

    private void AttackAnimation(bool setAnim)
    {
        anim.SetBool("attacking", setAnim);
    }

    private void AnimationBeginingEvent()
    {
        coll.enabled = true;
    }

    private void AnimationEndEvent()
    {
        coll.enabled = false;
        AttackAnimation(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(gameObject, damage);
            }
        }
    }
}
