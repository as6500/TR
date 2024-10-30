using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationTimer : MonoBehaviour
{
	[SerializeField] private HealthScript healthScript;
	[SerializeField] private float timeRemainingMin = 10.0f;
	[SerializeField] private float amountDamageGiven = 2.0f;
	[SerializeField] private RawImage TimerAntiRadiation;
	[SerializeField] private Text TimerText;
	private bool isTimerOn = false;


	public void Start()
	{
		isTimerOn = false;
		timeRemainingMin *= 60;
		Debug.Log(TimerAntiRadiation);
		TimerAntiRadiation.enabled = false;
		TimerText.enabled = false;
	}

	public void Update()
	{
		Debug.Log(isTimerOn);
		if (isTimerOn == true)
		{
			if (timeRemainingMin > 0)
			{
				timeRemainingMin -= Time.deltaTime;
				TimeDisplayed(timeRemainingMin);
				TimerAntiRadiation.enabled = true;
				TimerText.enabled = true;
			}
			else
			{
				timeRemainingMin = 0;
				TimeDisplayed(0);
				isTimerOn = false;
				healthScript.DamageFromRadiation(amountDamageGiven);
				TimerAntiRadiation.enabled = false;
				TimerText.enabled = false;
			}
		}
	}

	private void TimeDisplayed(float timeOnScreen)
	{
		float minutes = Mathf.FloorToInt(timeOnScreen / 60); //calculate the minutes
		float seconds = Mathf.FloorToInt(timeOnScreen % 60); //calculate the seconds

		gameObject.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	public bool TimerSwitch(bool timerOnOff)
	{
		return isTimerOn = timerOnOff;
	}

	public float TimeRemaining ()
	{
		return timeRemainingMin;
	}
}
