using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangingInfo : MonoBehaviour
{
	public EEntranceType entranceTypeToFind
	{
		get; set;
	}

	public EBuildings buildingTypeToFind
	{
		get; set;
	}

	private void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		EntranceLocator[] arrayOfAllEntrances = FindObjectsOfType<EntranceLocator>();
		for (int i = 0; i < arrayOfAllEntrances.Length; ++i)
		{
			if (arrayOfAllEntrances[i].entranceType == entranceTypeToFind)
			{
				if (arrayOfAllEntrances[i].entranceType == EEntranceType.CityBuilding)
				{
					if (arrayOfAllEntrances[i].buildingType == buildingTypeToFind)
						transform.position = arrayOfAllEntrances[i].transform.position;
				}
				else
					transform.position = arrayOfAllEntrances[i].transform.position;
			}
		}
	}
}
