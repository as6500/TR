using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateConnection : APIRequests
{
    [SerializeField] private APIRequests APIReq;
    [SerializeField] private PlayerStatsMetalDetector playerStats;
    private Action firstCallback;
    private Action secondCallback;

    private void Start()
    {
        APIReq = gameObject.GetComponent<APIRequests>();
    }

    public void CreateNewConnection(Action firstCallback = null, Action secondCallback = null)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("unity_connection_id", APIReq.unity_connection_id);
        this.firstCallback = firstCallback;
        this.secondCallback = secondCallback;

        StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/unityConnection/create", formData, StartSearchForConnection));
    }

    private void StartSearchForConnection()
    {
        firstCallback();
        APIReq.unity_connection_id = APIReq.response.unity_connection_id;
        APIReq.unity_connection_code = APIReq.response.unity_connection_code;
        SearchingForConnection();
    }

    public void SearchingForConnection() 
    {
        WWWForm formData = new WWWForm();
        formData.AddField("unity_connection_id", APIReq.unity_connection_id);
        formData.AddField("unity_connection_code", APIReq.unity_connection_code);
        StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/unityConnection/search", formData, CheckConnection));
    }

    private void CheckConnection()
    {
        if (APIReq.response.connection_successful)
        {
            APIReq.unity_android_connection_id = APIReq.response.unity_android_connection_id;
            secondCallback();
            playerStats.CreatePlayerStats();
        }
        else
        {
            SearchingForConnection();
        }
    }
}
