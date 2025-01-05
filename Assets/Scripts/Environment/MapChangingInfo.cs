using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangingInfo : Singleton<MapChangingInfo>
{
    private FadeInAndOutBlackScreen blackScreen; 
    [field:SerializeField] public EEntranceType entranceTypeToFind
	{
		get; set;
	}

	[field: SerializeField] public EBuildings buildingTypeToFind
	{
		get; set;
	}

	private void Start()
	{
		blackScreen = GameObject.FindGameObjectWithTag("FadeInAndOutBlackScreen").GetComponent<FadeInAndOutBlackScreen>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		EntranceLocator[] arrayOfAllEntrances = FindObjectsOfType<EntranceLocator>();
		Debug.Log("Scene Loaded");
		for (int i = 0; i < arrayOfAllEntrances.Length; ++i)
		{
			if (arrayOfAllEntrances[i].entranceType != entranceTypeToFind) 
				continue;

			if (entranceTypeToFind == EEntranceType.CityBuilding || entranceTypeToFind == EEntranceType.FarmBuilding)
			{
				if (arrayOfAllEntrances[i].buildingType == buildingTypeToFind)
				{
					GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
					Camera.main.transform.position = new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, Camera.main.transform.position.z);
					blackScreen.ResetAndStartFade(blackScreen.ChangeBlackScreenOpacityDown());
                    break;
				}
			}
			else if (this != null)
			{
                GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
				Camera.main.transform.position = new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, Camera.main.transform.position.z);

				if (blackScreen != null)
				{
                    blackScreen.ResetAndStartFade(blackScreen.ChangeBlackScreenOpacityDown());
                }
                break;
			}
		}
		
	}
}
