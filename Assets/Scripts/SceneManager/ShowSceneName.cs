using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSceneName : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Awake()
    {
        FadeInFadeOut fadeInFadeOut = FindObjectOfType<FadeInFadeOut>();
        if (fadeInFadeOut != null)
        {
            Debug.Log("Hello");
            fadeInFadeOut.SetText(sceneName);
        }
    }
}
