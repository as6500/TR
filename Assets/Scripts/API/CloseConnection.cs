using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseConnection : APIRequests
{
    [SerializeField] private APIRequests APIReq;

    private void Start()
    {
        APIReq = gameObject.GetComponent<APIRequests>();
    }

    public void CloseExistingConnection()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("unity_connection_id", APIReq.unity_connection_id);
        StartCoroutine(PostRequest("https://the-rumble-server.vercel.app/unityConnection/close", formData));
    }
}
