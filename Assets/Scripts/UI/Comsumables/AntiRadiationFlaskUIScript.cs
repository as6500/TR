using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationFlaskUIScript : MonoBehaviour
{
	[SerializeField] private AntiRadiationScript antiRadiationScript;
	private void Start()
	{
		UpdateTextFlasks();
	}

	public void UpdateTextFlasks()
	{
		gameObject.GetComponent<Text>().text = antiRadiationScript.FlasksQuantityReturn().ToString();
	}

}
