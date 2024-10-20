using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsUIScript : MonoBehaviour
{
    private Text bulletsText;
    [SerializeField] private int pocketBullets = 0;

    // Start is called before the first frame update
    void Start()
    {
        bulletsText = gameObject.GetComponent<Text>();
        ChangeText(pocketBullets);
    }

    public void AddOrRmvBullets(int quantity)
    {
        pocketBullets += quantity;
        ChangeText(pocketBullets);
    }

    public int PocketBullets()
    {
        return pocketBullets;
    }

    private void ChangeText(int qnt)
    {
        bulletsText.text = qnt.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
