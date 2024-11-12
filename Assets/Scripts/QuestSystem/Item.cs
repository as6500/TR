using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
	public int id;
	[SerializeField] private string displayName;

	public void SetUpItem(int id, string displayName)
	{
		this.id = id;
		this.displayName = displayName;
	}

	public string GetDisplayName()
	{
		return this.displayName;
	}
}
