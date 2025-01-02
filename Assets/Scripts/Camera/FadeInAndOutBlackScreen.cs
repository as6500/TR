using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAndOutBlackScreen : MonoBehaviour
{
    private SpriteRenderer blackScreen;
    [Header("Time")]
    [SerializeField] private int timeToFadeSeconds = 3;
    [SerializeField] private float timeBetweenOpacityChanged = 0.1f;
    [SerializeField] private float currentTimeSeconds;
    [SerializeField] private float opacityChangePerTick;

    void Start()
    {
        opacityChangePerTick = 1 / (timeToFadeSeconds / timeBetweenOpacityChanged);
        blackScreen = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeBlackScreenOpacityDown());
    }

    public IEnumerator ChangeBlackScreenOpacityUp()
    {
        while (currentTimeSeconds < timeToFadeSeconds)
        {
            blackScreen.color = new Color(0, 0, 0, blackScreen.color.a + opacityChangePerTick);
            yield return new WaitForSeconds(timeBetweenOpacityChanged);
            currentTimeSeconds += timeBetweenOpacityChanged;
        }
        currentTimeSeconds = 0;
    }

    public IEnumerator ChangeBlackScreenOpacityDown()
    {
        while (currentTimeSeconds < timeToFadeSeconds)
        {
            blackScreen.color = new Color(0, 0, 0, blackScreen.color.a - opacityChangePerTick);
            yield return new WaitForSeconds(timeBetweenOpacityChanged);
            currentTimeSeconds += timeBetweenOpacityChanged;
        }
        currentTimeSeconds = 0;
    }
}
