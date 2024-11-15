using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNames : MonoBehaviour
{
    public TextMeshProUGUI currentSceneText;
    public float fadeTime;
    public bool fadingIn;
    public bool fadingOut;

    private void Start()
    {
        currentSceneText.enabled = false;
    }

    private void Update()
    {
        if (fadingIn)
        {
            if (currentSceneText.color.a < 1)
            {
                currentSceneText.enabled = true;
                fadeTime += Time.deltaTime;
                if (currentSceneText.color.a >= 1)
                    fadingIn = false;
            }
        }
        
        if (fadingOut)
        {
            if (currentSceneText.color.a >= 0)
            {
                fadeTime -= Time.deltaTime;
                if (currentSceneText.color.a == 0)
                    fadingOut = false;
            }
        }
        
    }

    public void FadeIn()
    {
        Debug.Log("Hello2!");
        fadingIn = true;
    }
    
    public void FadeOut()
    {
        fadingOut = true;
    }
}
