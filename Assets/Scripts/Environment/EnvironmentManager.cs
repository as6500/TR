using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public enum EDayCicleState { Day, Transition, Night }

public class EnvironmentManager : Singleton<EnvironmentManager>
{
    [Header("Light Objects")]
    [SerializeField] private Light2D mainLight;

    [Header("Light Settings")]
    [SerializeField] private float minimNightLight = 0.04f;
    [SerializeField] private float insideLighting = 0.5f;
    [SerializeField] private float outsideLighting = 1f;

    [Header("Day And Night Cicle")]
    [SerializeField] private float dayCicleMinutes = 1f;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private EDayCicleState currentDayCicleState;
    private EDayCicleState tempDayCicleState;
    private float dayTimeSeconds;

    [Header("Time Settings")]
    [SerializeField] private float updatePeriod = 1f;
    [SerializeField] private float addToSeconds = 1f;
    [SerializeField] private float currentTimeSeconds = 0f;

    private void Start()
    {
        currentDayCicleState = EDayCicleState.Day;
        dayTimeSeconds = dayCicleMinutes * 60;
        outsideLighting = 1;
        StartCoroutine(CountTime());
    }

    private IEnumerator CountTime()
    {
        while (true) {
            yield return new WaitForSeconds(updatePeriod);

            if (currentTimeSeconds >= dayTimeSeconds)
            {
                currentTimeSeconds = 0;
            }

            currentTimeSeconds += addToSeconds;
        }
    }

    void FixedUpdate()
    {
        CheckScene();
        ControlTime();
        CheckDayCicleState();
        dustParticles.transform.position = Camera.main.transform.position;
    }

    private void CheckDayCicleState()
    {
        if (currentDayCicleState != tempDayCicleState)
        {
            Debug.Log(currentDayCicleState);
            tempDayCicleState = currentDayCicleState;
        }
    }

    private void ControlTime()
    {
        if (currentTimeSeconds >= dayTimeSeconds * 0.4f && currentTimeSeconds <= dayTimeSeconds * 0.5f)
        {
            currentDayCicleState = EDayCicleState.Transition;
            StartNight();
        }
        else if (currentTimeSeconds >= dayTimeSeconds * 0.9f && currentTimeSeconds <= dayTimeSeconds)
        {
            currentDayCicleState = EDayCicleState.Transition;
            StartDay();
        }
    }

    private void CheckScene()
    {
        if (!RightScene())
        {
            mainLight.intensity = insideLighting;
        }
        else 
        {
            mainLight.intensity = outsideLighting;
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
            intensity = minimNightLight;
            currentDayCicleState = EDayCicleState.Night;
        }

        outsideLighting = intensity;
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
            intensity = 1;
            currentDayCicleState = EDayCicleState.Day;
        }

        outsideLighting = intensity;
    }

    public EDayCicleState GetCicleState()
    {
        return currentDayCicleState;
    }
}
