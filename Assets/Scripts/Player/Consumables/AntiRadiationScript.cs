using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRadiationScript : MonoBehaviour
{
	private int numberOfFlasks = 5;
	private bool flaskTaken;
    [SerializeField] private AntiRadiationTimer timer;
	[SerializeField] AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;

	private void Start()
	{
		flaskTaken = false;
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.E)) //flasks taken
		{
			GainImmunity();
			FlasksCount();
			timer.TimerSwitch(true);
		}
	}
	public void GainImmunity()
    {
        flaskTaken = true;
	}

	public void FlasksCount()
	{
		if (numberOfFlasks > 0)
		{
			numberOfFlasks--;
			antiRadiationFlaskUIScript.UpdateTextFlasks();
		}
	}

	public int FlasksQuantityReturn()
	{
		return numberOfFlasks;
	}
}
