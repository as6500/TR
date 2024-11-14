using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] DontDestroyOnLoadArray;
    private void Awake()
    {
        for (int i = 0; i < DontDestroyOnLoadArray.Length; i++) 
        {
            Instantiate(DontDestroyOnLoadArray[i]);
            DontDestroyOnLoad(DontDestroyOnLoadArray[i]);
        }
    }
}
