using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D coll;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        coll.enabled = false;
    }


    private void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

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
        RandomSwing();
    }

    private void RandomSwing()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            audioManager.swingOne.Play();
        }
        else if (rand == 1)
        {
            audioManager.swingTwo.Play();
        }
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
