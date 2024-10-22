using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillsUIScript : MonoBehaviour
{
	[SerializeField] private PillsScript pillsScript;

	private void Start()
	{
		//pillsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PillsScript>();
		UpdateUIText();
	}

	public void UpdateUIText()
	{
		gameObject.GetComponent<Text>().text = pillsScript.PillsQuantityReturn().ToString();
	}
}
