using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapRegion { Bunker, Outside, Road, Farm, City }

public class Treasure : MonoBehaviour
{
    [Header("API connection")]
    [SerializeField] private PlayerStatsMetalDetector playerStats;

    [Header("Collectible Stats")]
    [SerializeField] private bool isInRange;
    [field: SerializeField] public bool caught { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Vector2 position { get; private set; }
    [field: SerializeField] public MapRegion mapRegion { get; private set; }
    [field: SerializeField] public string sceneName { get; private set; }

    private void Awake()
    {
        position = transform.position;
        isInRange = false;
        caught = false;
    }

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStatsMetalDetector>();
    }

    void Update()
    {
        if (!caught && gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isInRange)
                {
                    playerStats.CaughtTreasure(id);
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
