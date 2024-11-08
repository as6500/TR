using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour , IInteractable
{
	private bool interactedWith = false;
	public void Interact()
	{
		Debug.Log("NPC interacted with!!!");
		interactedWith=true;
	}

	public bool Interacted (bool trueFalse)
	{
		return interactedWith;
	}
}
