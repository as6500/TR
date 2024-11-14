using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] DontDestroyOnLoadArray;
    [SerializeField] private bool gameRuning = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!gameRuning)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("SceneManager");
            if (temp != null)
            {
                Debug.LogError("Aqui");
                Destroy(gameObject);
                return;
            }

            for (int i = 0; i < DontDestroyOnLoadArray.Length; i++)
            {
                DontDestroyOnLoad(DontDestroyOnLoadArray[i]);
            }
            gameRuning = true;
        }
    }
}
