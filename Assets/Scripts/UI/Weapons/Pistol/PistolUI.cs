using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolUI : MonoBehaviour
{
    [SerializeField] private Text bulletsText;
    [SerializeField] private Pistol bullets;

    // Start is called before the first frame update
    void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        int qnt = bullets.MagBullets();
        bulletsText.text = qnt.ToString() + "/10";
    }
}
