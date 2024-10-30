using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectableObject = collision.GetComponent<ICollectible>();

        if (collectableObject != null)
            collectableObject.Collect(gameObject);
    }
}
