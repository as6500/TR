using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextHealthBarScript : MonoBehaviour
{
	[SerializeField] private HealthScript healthScript;

	private void Start()
	{
		UpdateHealthBarText();
	}

	private void Update()
	{
		UpdateHealthBarText();
	}

	private void UpdateHealthBarText()
	{
		gameObject.GetComponent<Text>().text = healthScript.CurrentHealthReturn().ToString() + "/100";
	}
}
