using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolUI : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] private Text bulletsText;
    [SerializeField] private Pistol magBullets;
    [SerializeField] private PocketBullets pocketbullets;

    void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        int mag = magBullets.MagBullets();
        int pocket = pocketbullets.GetPocketBullets();
        bulletsText.text = mag.ToString() + "/" + pocket.ToString();
    }
}
