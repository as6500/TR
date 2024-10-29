using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketBullets : MonoBehaviour
{
    [SerializeField] private int pocketBullets = 0;
    [SerializeField] private BulletsUIScript bulletsUI;

    private void Start()
    {
        bulletsUI.ChangeText(pocketBullets);
    }

    public int GetPocketBullets()
    {
        return pocketBullets;
    }

    public void AddOrRmvBullets(int quantity)
    {
        pocketBullets += quantity;
        bulletsUI.ChangeText(pocketBullets);
    }
}
