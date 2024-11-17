using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiRadiationScript : MonoBehaviour
{
	private int numberOfFlasks = 5;
	public bool flaskTaken;
	[SerializeField] private float delayTimeSec = 2.0f;
	[SerializeField] private float amountDamageGiven;
	[SerializeField] private AntiRadiationTimer timer;
	[SerializeField] AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;
	[SerializeField] private HealthScript healthScript;

	private void Start()
	{
		flaskTaken = false;
		
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && flaskTaken == false) //flasks taken
		{
			FlasksCount();
			timer.TimerReset(true);
		}
	}

	public void FlasksCount()
	{
		if (numberOfFlasks > 0)
		{
			flaskTaken = true;
			numberOfFlasks--;
			antiRadiationFlaskUIScript.UpdateTextFlasks();
		}
	}
	public IEnumerator RadiationDamage()
	{
		flaskTaken = timer.TimeRemaining() > 0.0f;
		if (flaskTaken == false)
		{
			healthScript.DamageFromRadiation(amountDamageGiven);
			yield return new WaitForSeconds(delayTimeSec);
			StartCoroutine(RadiationDamage());
		}
	}
	public void UpdateFlasks(int quantity)
	{
		numberOfFlasks += quantity;
		antiRadiationFlaskUIScript.UpdateTextFlasks();
	}

	public int FlasksQuantityReturn()
	{
		return numberOfFlasks;
	}
	

}
