using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBunker : MonoBehaviour
{
	// public GameObject currentLocationText;
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			SceneManager.LoadScene("Bunker");
			// currentLocationText.SetActive(true);
			collision.transform.position = new Vector3(-10, 2, 0);
		}
	}
}
