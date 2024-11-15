using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    [SerializeField] private Text currentSceneText;
    private Text tempText;
    [SerializeField] private bool fadingIn;
    [SerializeField] private bool fadingOut;
    [SerializeField] private float valueToAdd;

    private void Start()
    {
        SetTextAppha(0);
    }

    private void Update()
    {
        if (fadingIn)
        {
            if (currentSceneText.color.a < 1)
            {
                SetTextAppha(currentSceneText.color.a + valueToAdd);
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
                SetTextAppha(currentSceneText.color.a - valueToAdd);
                if (currentSceneText.color.a <= 0)
                    fadingOut = false;
            }
        }
    }

    private void SetTextAppha(float alpha)
    {
        currentSceneText.color = new Color(currentSceneText.color.r, currentSceneText.color.g, currentSceneText.color.b, alpha);   
    }
    
    public void SetText(String sceneName)
    {
        currentSceneText.text = sceneName;
        SetTextAppha(0);
        fadingIn = true;
        fadingOut = false;
    }
    
    public IEnumerator FadingOut()
    {
        yield return new WaitForSeconds(2f);
        fadingOut = true;
    }
}
