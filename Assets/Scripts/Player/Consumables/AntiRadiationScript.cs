using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRadiationScript : MonoBehaviour
{
    private bool flaskTaken;
    private AntiRadiationTimer timer;

	public void Start()
	{
		flaskTaken = false;
	}

	public void GainImmunity()
    {
        flaskTaken = true;
	}
}
