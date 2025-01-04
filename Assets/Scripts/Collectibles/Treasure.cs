using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapRegion { Bunker, Outside, Road, Farm, City }

public class Treasure : MonoBehaviour
{
    [Header("Collectible Stats")]
    [SerializeField] private bool isInRange;
    [field: SerializeField] public bool caught { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Vector2 position { get; private set; }
    [field: SerializeField] public MapRegion mapRegion { get; private set; }
    [field: SerializeField] public string sceneName { get; private set; }

    private void Start()
    {
        isInRange = false;
        caught = false;
        position = transform.position;
    }

    void Update()
    {
        if (!caught)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isInRange)
                {
                    caught = true;
                    gameObject.SetActive(false);
                }
            }
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
