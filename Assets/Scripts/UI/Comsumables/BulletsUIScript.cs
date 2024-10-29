using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsUIScript : MonoBehaviour
{
    [SerializeField] private Text bulletsText;

    public void ChangeText(int bullets)
    {
        bulletsText.text = bullets.ToString();
    }
}
