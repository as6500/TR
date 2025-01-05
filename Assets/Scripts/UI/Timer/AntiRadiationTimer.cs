using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationTimer : MonoBehaviour
{
	[SerializeField] private float timeRemainingMin;
	private float timeRemainingSec;
	[SerializeField] public RawImage TimerAntiRadiation;
	[SerializeField] private Text TimerText;
	[SerializeField] private AntiRadiationScript antiRadiationScript;
	private bool isTimerOn = false;


	public void Start()
	{
		TimerReset(false);
		TimerAntiRadiation.enabled = false;
		TimerText.enabled = false;
		timeRemainingSec = 0;
	}

	public void Update()
	{
		if (isTimerOn)
		{
			if (timeRemainingSec > 0)
			{
				timeRemainingSec -= Time.deltaTime;
				TimeDisplayed(timeRemainingSec);
				TimerAntiRadiation.enabled = true;
				TimerText.enabled = true;
			}
			else 
			{
				timeRemainingSec = 0;
				TimeDisplayed(0);
				isTimerOn = false;
				TimerAntiRadiation.enabled = false;
				TimerText.enabled = false;
				antiRadiationScript.IsThisTheRightScene();
			}
		}
	}

	private void TimeDisplayed(float timeOnScreen)
	{
		float minutes = Mathf.FloorToInt(timeOnScreen / 60); //calculate the minutes
		float seconds = Mathf.FloorToInt(timeOnScreen % 60); //calculate the seconds

		gameObject.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	public void TimerReset(bool timerOnOff)
	{
		isTimerOn = timerOnOff;
		timeRemainingSec = timeRemainingMin * 60;
	}

	public float TimeRemaining ()
	{
		return timeRemainingSec;
	}
	
	public void HideRadiationUI()
	{
		TimerAntiRadiation.enabled = false;
		TimerText.enabled = false;
	}

	public void ShowRadiationUI()
	{
		if (TimerAntiRadiation)
		{
			TimerAntiRadiation.enabled = true;
			TimerText.enabled = true;
		}
	}
}
