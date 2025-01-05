using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class QuestTypeFetch : MonoBehaviour
{
	[SerializeField] private bool isInRange;
	[SerializeField] private QuestData quest;
	[SerializeField] private QuestManager questManager;
	private Vector2 targetPosition;
	private EBuildings building;
	private Item item;
	
	private void Start()
	{
		item = GetComponent<Item>();
		questManager = FindFirstObjectByType<QuestManager>();
	}
	
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void Update()
	{
		if (isInRange && Input.GetKeyDown(KeyCode.E))
		{
			QuestManager.OnQuestAction.Invoke();
			SceneManagement.Instance.RemoveObjectFromScene(gameObject);
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isInRange = true;
			questManager.interactedItem = item;
		}
	}
	
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
			isInRange = false;
		
		if (questManager.interactedItem == item)
			questManager.interactedItem = null;
	}

	public void InitiateItem(QuestData activeQuest)
	{
		SceneManagement.Instance.AddObjectToScene(gameObject, activeQuest.sceneNameForItems);
		building = activeQuest.buildingTypeForItems;
	}
	
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!SceneManagement.Instance.ObjectBelongsToScene(gameObject, scene.name))
			return;

		if (building != EBuildings.INVALID)
		{
			targetPosition = ItemLocations.FindItemLocation(building);
			transform.position = targetPosition + new Vector2(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, -0.5f));
		}
	}	
}
