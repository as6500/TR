using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangingInfo : MonoBehaviour
{
    private FadeInAndOutBlackScreen blackScreen; 
    [field:SerializeField] public EEntranceType entranceTypeToFind
	{
		get; set;
	}

	[field:SerializeField] public EBuildings buildingTypeToFind
	{
		get; set;
	}
	private void Awake()
	{
        SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start()
	{
		blackScreen = GameObject.FindGameObjectWithTag("FadeInAndOutBlackScreen").GetComponent<FadeInAndOutBlackScreen>();
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
					Camera.main.transform.position = new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, Camera.main.transform.position.z);
                    StartCoroutine(blackScreen.ChangeBlackScreenOpacityDown());
                    return;
				}
			}
			else
			{ 
				GetComponent<Rigidbody2D>().position = arrayOfAllEntrances[i].transform.position;
				Camera.main.transform.position = new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, Camera.main.transform.position.z);

				if (blackScreen != null)
				{
					StartCoroutine(blackScreen.ChangeBlackScreenOpacityDown());
				}
                return;
			}
		}
		
	}
}
