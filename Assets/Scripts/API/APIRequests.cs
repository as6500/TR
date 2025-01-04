using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public struct UnJasonedData
{
    public string message;
    public string error;
    public int unity_connection_id;
    public int unity_android_connection_id;
    public int unity_connection_code;
    public bool connection_successful;
    public int collectibles_ids;
}

public class APIRequests : MonoBehaviour
{
    public int unity_connection_id { get; set; }
    public int unity_android_connection_id { get; set; }
    public int unity_connection_code { get; set; }
    public UnJasonedData response { 
        get; 
        private set; }

    private void Start()
    {
        unity_connection_id = 0;
    }

    public IEnumerator GetRequest(string uri)
    {
        UnityWebRequest WebRequest = UnityWebRequest.Get(uri);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result == UnityWebRequest.Result.Success)
        {
            UnJasonedData data = (UnJasonedData)JsonUtility.FromJson(WebRequest.downloadHandler.text, typeof(UnJasonedData));

            response = data;
        }
        else
        {
            Debug.Log("Error While Sending: " + WebRequest.error);
        }
    }

    public IEnumerator PostRequest(string uri, WWWForm composedMessage, Action callback = null)
    {
        UnityWebRequest WebRequest = UnityWebRequest.Post(uri, composedMessage);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result == UnityWebRequest.Result.Success)
        {
            UnJasonedData data = (UnJasonedData)JsonUtility.FromJson(WebRequest.downloadHandler.text, typeof(UnJasonedData));
            response = data;

            if(callback != null)
            {
                callback();
            }
        }
        else
        {
            Debug.Log("Error While Sending: " + WebRequest.error);
        }
    }   
}