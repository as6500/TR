using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class QuestTypeLocation : MonoBehaviour
{
    [SerializeField] private bool onLocation;
    
    private void Start()
    {
        onLocation = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onLocation = true;
            QuestManager.OnQuestAction.Invoke();
        }
    }
    
    public bool OnLocation()
    {
        return onLocation;
    }
}