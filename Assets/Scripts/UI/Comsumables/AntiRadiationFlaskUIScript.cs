using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationFlaskUIScript : MonoBehaviour
{
	private int numberOfFlasks = 5;
	private AntiRadiationScript antiRadiationScript;
	private void Start()
	{
		antiRadiationScript = GameObject.FindGameObjectWithTag("Player").GetComponent<AntiRadiationScript>();
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
