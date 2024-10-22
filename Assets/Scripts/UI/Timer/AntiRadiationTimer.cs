using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationTimer : MonoBehaviour
{
	private float timeRemaining = 10;
	private bool isTimerOn = false;
	private Text timerText;

	public void Start()
	{
		isTimerOn = true;
	}

	public void Update()
	{
		if (isTimerOn == true)
		{
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
				timeDisplayed(timeRemaining);
			}
			else
			{
				timeRemaining = 0;
				isTimerOn = false;
			}
		}
	}

	public void timeDisplayed(float timeOnScreen)
	{
		float minutes = Mathf.FloorToInt(timeOnScreen / 60); //calculate the minutes
		float seconds = Mathf.FloorToInt(timeOnScreen % 60); //calculate the seconds

		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
