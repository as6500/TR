using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangingInfo : MonoBehaviour
{
	public EEntranceType EntranceTypeToFind
	{
		get;
		set;
	}

	private void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		EntranceLocator[] ArrayOfALlEntrances = FindObjectsOfType<EntranceLocator>();
		for (int i = 0; i < ArrayOfALlEntrances.Length; ++i)
		{
			transform.position = ArrayOfALlEntrances[i].transform.position;
		}
	}
}
