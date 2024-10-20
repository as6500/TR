using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillsUIScript : MonoBehaviour
{
	private int pillsQuantity = 10;
	private bool healed = false;
	private PillsScript pillsScript;

	private void Start()
	{
		pillsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PillsScript>();
		gameObject.GetComponent<Text>().text = pillsQuantity.ToString();
	}

	public void PillCount()
	{
		if (pillsScript.Healed(true) && pillsQuantity > 0)
		{
			pillsQuantity--;
			gameObject.GetComponent<Text>().text = pillsQuantity.ToString();
		}
	}
}
