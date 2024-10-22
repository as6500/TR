using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationTimer : MonoBehaviour
{
	[SerializeField] private float timeRemainingMin = 10.0f;
	private bool isTimerOn = false;


	public void Start()
	{
		isTimerOn = true;
		timeRemainingMin *= 60;
	}

	public void Update()
	{
		if (isTimerOn == true)
		{
			if (timeRemainingMin > 0)
			{
				timeRemainingMin -= Time.deltaTime;
				timeDisplayed(timeRemainingMin);
			}
			else
			{
				timeRemainingMin = 0;
				timeDisplayed(0);
				isTimerOn = false;
			}
		}
	}

	public void timeDisplayed(float timeOnScreen)
	{
		float minutes = Mathf.FloorToInt(timeOnScreen / 60); //calculate the minutes
		float seconds = Mathf.FloorToInt(timeOnScreen % 60); //calculate the seconds

		gameObject.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
