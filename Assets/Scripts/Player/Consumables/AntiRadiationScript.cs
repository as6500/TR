using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AntiRadiationScript : MonoBehaviour
{
	private int numberOfFlasks = 5;
	public bool flaskTaken;
	[SerializeField] private float delayTimeSec = 2.0f;
	[SerializeField] private float amountDamageGiven;
	[SerializeField] private AntiRadiationTimer timer;
	[SerializeField] AntiRadiationFlaskUIScript antiRadiationFlaskUIScript;
	[SerializeField] private HealthScript healthScript;
	private bool isCoroutineActive;

	private void Start()
	{
		flaskTaken = false;
		isCoroutineActive = false;

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
        isCoroutineActive = true;
		
        
		while (isCoroutineActive)
		{
            flaskTaken = timer.TimeRemaining() > 0.0f;
            if (flaskTaken == false)
            {
                yield return new WaitForSeconds(delayTimeSec);
                healthScript.DamageFromRadiation(amountDamageGiven);
            }
            else
                isCoroutineActive = false;
        }
	}

	public void UpdateFlasks(int quantity)
	{
		numberOfFlasks += quantity;
		antiRadiationFlaskUIScript.UpdateTextFlasks();
	}
	
	public void IsThisTheRightScene()
	{
		if (SceneManager.GetActiveScene().name == "BunkerInside")
			StopAllCoroutines();
		else
			StartCoroutine(RadiationDamage());
	}

	public int FlasksQuantityReturn()
	{
		return numberOfFlasks;
	}

	public bool IsCoroutineActive()
	{
		return isCoroutineActive;
	}
}
