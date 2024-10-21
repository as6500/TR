using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationFlaskUIScript : MonoBehaviour
{
	private int numberOfFlasks = 5;
	private AntiRadiationScript antiRadiationScript;
	private HealthScript healthScript;
	private void Start()
	{
		antiRadiationScript = GameObject.FindGameObjectWithTag("Player").GetComponent<AntiRadiationScript>();
		healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
		gameObject.GetComponent<Text>().text = numberOfFlasks.ToString();
	}
	public void FlasksCount()
	{
		if (numberOfFlasks > 0)
		{
			numberOfFlasks--;
			gameObject.GetComponent<Text>().text = numberOfFlasks.ToString();
		}
	}
}
