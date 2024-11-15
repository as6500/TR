using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBunker : MonoBehaviour
{
	private SceneNames scene;

	private void Start()
	{
		scene = GetComponent<SceneNames>();
	}

	public IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds(0.5f);
		Debug.Log("Hello!");
		scene.FadeIn();
		
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			SceneManager.LoadScene("Bunker");
			collision.transform.position = new Vector3(-10, 2, 0);
			StartCoroutine(ChangeScene());
		}
	}
}
