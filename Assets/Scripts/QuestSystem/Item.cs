using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
	public int id;
	[SerializeField] private string displayName;

	public void SetUpItem(int id)
	{
		this.id = id;
	}
}
