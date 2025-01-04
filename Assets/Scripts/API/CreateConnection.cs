using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateConnection : APIRequests
{
    [SerializeField] private PlayerStatsMetalDetector playerStats;
    private Action firstCallback;
    private Action secondCallback;

    public void CreateNewConnection(Action firstCallback = null, Action secondCallback = null)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("unity_connection_id", unity_connection_id);
        this.firstCallback = firstCallback;
        this.secondCallback = secondCallback;

        StartCoroutine(PostRequest("https://the-rumble-server.vercel.app/unityConnection/create", formData, StartSearchForConnection));
    }

    private void StartSearchForConnection()
    {
        firstCallback();
        unity_connection_id = response.unity_connection_id;
        unity_connection_code = response.unity_connection_code;
        SearchingForConnection();
    }

    public void SearchingForConnection() 
    {
        WWWForm formData = new WWWForm();
        formData.AddField("unity_connection_id", unity_connection_id);
        formData.AddField("unity_connection_code", unity_connection_code);
        StartCoroutine(PostRequest("https://the-rumble-server.vercel.app/unityConnection/search", formData, CheckConnection));
    }

    private void CheckConnection()
    {
        if (response.connection_successful)
        {
            unity_android_connection_id = response.unity_android_connection_id;
            secondCallback();
            playerStats.CreatePlayerStats(unity_android_connection_id);
        }
        else
        {
            SearchingForConnection();
        }
    }

    private void CloseConnection()
    {
        //CloseConnection
    }
}
