using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Singleton<Treasure>
{
    [SerializeField] private bool isInRange;

    private void Start()
    {
        isInRange = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInRange)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) 
            return;
        
        isInRange = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) 
            return;
        
        isInRange = false;
    }
}
