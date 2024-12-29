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

	public EFloors floorToFind
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
			if (arrayOfAllEntrances[i].entranceType != entranceTypeToFind) 
				continue;

			if (entranceTypeToFind == EEntranceType.CityBuilding || entranceTypeToFind == EEntranceType.FarmBuilding)
			{
				if (arrayOfAllEntrances[i].buildingType == buildingTypeToFind)
				{
					GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
					return;
				}
				
				if (arrayOfAllEntrances[i].floorType == floorToFind)
				{
					GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
					return;
				}
			}
			else
			{
				GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
				return;
			}
		}
	}
}
