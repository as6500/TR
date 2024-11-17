using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] private Tilemap door;
    [SerializeField] private Sprite doorOpeningSprite;
    [SerializeField] private Tile doorOpeningTile;
    [SerializeField] private Vector3Int position;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            doorOpeningTile.sprite = doorOpeningSprite; 
            door.SetTiles(positionArray: new [] { position }, tileArray: new[] { doorOpeningTile } );
        }
    }
}
