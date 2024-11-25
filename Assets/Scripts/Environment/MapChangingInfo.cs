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
			if (arrayOfAllEntrances[i].entranceType != entranceTypeToFind) 
				continue;

			if (entranceTypeToFind == EEntranceType.CityBuilding)
			{

				if (arrayOfAllEntrances[i].buildingType == buildingTypeToFind)
				{
					GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
					return;
				}
			}
			else
			{
				Debug.Log("target: " + arrayOfAllEntrances[i].transform.position);
				GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
				Debug.Log("position: " + transform.position);
				return;
			}

		}
	}
}
