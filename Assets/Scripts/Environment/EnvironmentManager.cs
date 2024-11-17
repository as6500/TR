using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EnvironmentManager : Singleton<EnvironmentManager>
{
    [Header("Light Objects")]
    [SerializeField] private Light2D mainLight;

    [Header("Light Settings")]
    [SerializeField] private float minimNightLight = 0.04f;
    [SerializeField] private float bunkerLight = 0.5f;

    [Header("Day And Night Cicle")]
    [SerializeField] private float timeMinutes = 1f;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private bool? day = true;
    private float dayTimeSeconds;

    [Header("Time Settings")]
    [SerializeField] private float secondsInSeconds = 1f;
    [SerializeField] private float addToSeconds = 1f;
    private float currentTimeSeconds = 0f;

    private void Start()
    {
        dayTimeSeconds = timeMinutes * 60;
        mainLight.intensity = 1;
        StartCoroutine(CountTime());
    }

    private IEnumerator CountTime()
    {
        yield return new WaitForSeconds(secondsInSeconds);

        if (currentTimeSeconds >= timeMinutes * 60)
        {
            currentTimeSeconds = 0;
        }

        currentTimeSeconds += addToSeconds;

        //Debug.Log(currentTimeSeconds);
        StartCoroutine(CountTime());
    }

    void FixedUpdate()
    {
        CheckScene();
        ControlTime();
        dustParticles.transform.position = Camera.main.transform.position;
    }

    private void ControlTime()
    {
        if(RightScene())
        {
            if (currentTimeSeconds >= dayTimeSeconds * 0.4f && currentTimeSeconds <= dayTimeSeconds * 0.5f)
            {
                StartNight();
            }
            else if (currentTimeSeconds >= dayTimeSeconds * 0.9f && currentTimeSeconds <= dayTimeSeconds)
            {
                StartDay();
            }
            else
            {
                day = null;
            }
        }
    }

    private void CheckScene()
    {
        if (RightScene() == false)
        {
            mainLight.intensity = bunkerLight;
        }
        else if (day == true)
        {
            mainLight.intensity = 1;
        }
        else if (day == false)
        {
            mainLight.intensity = minimNightLight;
        }
        else 
        { 
        
        }
    }

    private bool RightScene()
    {
        return SceneManager.GetActiveScene().name != "BunkerInside";
    }

    private void StartNight()
    {
        float intendedIntensity = dayTimeSeconds * 0.1f;
        float currentIntensity = dayTimeSeconds * 0.5f - currentTimeSeconds;

        float intensity = currentIntensity / intendedIntensity;

        if (intensity < 0.05f)
        {
            day = false;
            intensity = minimNightLight;
        }

        mainLight.intensity = intensity;
    }

    private void StartDay()
    {
        float intendedIntensity = dayTimeSeconds * 0.1f;
        float currentIntensity = currentTimeSeconds - dayTimeSeconds * 0.9f;

        float intensity = currentIntensity / intendedIntensity;

        if (intensity < 0.05f)
        {
            intensity = minimNightLight;
        }
        else if( intensity > 0.95f)
        {
            day = true;
            intensity = 1;
        }

        mainLight.intensity = intensity;
    }
}
