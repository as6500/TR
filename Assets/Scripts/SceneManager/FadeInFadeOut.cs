using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    [SerializeField] private Text currentSceneText;
    [SerializeField] private bool fadingIn;
    [SerializeField] private bool fadingOut;
    [SerializeField] private float valueToAdd;
    
    private void Start()
    {
        currentSceneText.color = new Color(currentSceneText.color.r, currentSceneText.color.g, currentSceneText.color.b,0);
    }

    private void Update()
    {
        if (fadingIn)
        {
            if (currentSceneText.color.a < 1)
            {
                currentSceneText.color = new Color(currentSceneText.color.r, currentSceneText.color.g, currentSceneText.color.b,valueToAdd + currentSceneText.color.a);
                if (currentSceneText.color.a >= 1)
                {
                    fadingIn = false;
                    StartCoroutine(FadingOut());
                }
            }
        }
        
        if (fadingOut)
        {
            if (currentSceneText.color.a >= 0)
            {
                currentSceneText.color = new Color(currentSceneText.color.r, currentSceneText.color.g, currentSceneText.color.b,currentSceneText.color.a - valueToAdd);
                if (currentSceneText.color.a <= 0)
                    fadingOut = false;
            }
        }
    }
    
    public void SetText(String sceneName)
    {
        currentSceneText.text = sceneName;
        fadingIn = true;
    }
    
    public IEnumerator FadingOut()
    {
        yield return new WaitForSeconds(2f);
        fadingOut = true;
    }
}
