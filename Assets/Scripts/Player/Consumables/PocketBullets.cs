using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketBullets : MonoBehaviour
{
    [SerializeField] private int pocketBullets = 0;
    [SerializeField] private PistolUI pistolUI;

    private void Start()
    {
        pistolUI.ChangeText();
    }

    public int GetPocketBullets()
    {
        return pocketBullets;
    }

    public void UpdateBullets(int quantity)
    {
        pocketBullets += quantity;
        pistolUI.ChangeText();
    }
}
