using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

struct UnJasonedData
{
    public string MessageID;
}

public class APIRequests : MonoBehaviour
{
    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest WebRequest = UnityWebRequest.Get(uri);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result == UnityWebRequest.Result.Success)
        {
            // Parse Information
            Debug.Log(WebRequest.downloadHandler.text);

            UnJasonedData data = (UnJasonedData)JsonUtility.FromJson(WebRequest.downloadHandler.text, typeof(UnJasonedData));

            Debug.Log(data.MessageID);
        }
        else
        {
            Debug.Log("Error While Sending: " + WebRequest.error);
        }
    }

    IEnumerator PostRequest(string uri, WWWForm composedMessage)
    {
        UnityWebRequest WebRequest = UnityWebRequest.Post(uri, composedMessage);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result == UnityWebRequest.Result.Success)
        {
            // Parse Information
            Debug.Log(WebRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error While Sending: " + WebRequest.error);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            WWWForm formData = new WWWForm();
            formData.AddField("code", 40); // For Posts

            //StartCoroutine(PostRequest("http://localhost:3308/connect", formData));
            StartCoroutine(PostRequest("https://the-rumble-server.vercel.app/connect", formData));
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //StartCoroutine(GetRequest("http://localhost:3308/create"));
            StartCoroutine(GetRequest("https://the-rumble-server.vercel.app/create"));
        }
    }
}