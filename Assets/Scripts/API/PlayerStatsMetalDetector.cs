using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsMetalDetector : APIRequests
{
    [SerializeField] private APIRequests APIReq;
    [SerializeField] private int secondsBetweenStatsUpdate = 1;
    private GameObject player;

    private void Start()
    {
        APIReq = gameObject.GetComponent<APIRequests>();
        FindPlayerInScene();
    }

    private IEnumerator UpdatePlayerStats()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenStatsUpdate);
            if (SceneManager.GetActiveScene().name != "Main Menu")
            {
                if(player == null)
                {
                    FindPlayerInScene();
                }

                WWWForm formData = new WWWForm();
                formData.AddField("player_position_x", player.transform.position.x.ToString());
                formData.AddField("player_position_y", player.transform.position.y.ToString());
                formData.AddField("player_regionOfMap", SceneNameToInt(SceneManager.GetActiveScene().name));
                formData.AddField("unity_android_connection_id", APIReq.unity_android_connection_id.ToString());
                StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/playerStats/updatePositions", formData, ShowResponseMessage));
            }
        }
    }

    private void FindPlayerInScene()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void ShowResponseMessage()
    {
        Debug.Log(APIReq.response.message);
    }

    public void CreatePlayerStats()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("player_position_x", 0);
        formData.AddField("player_position_y", 0);
        formData.AddField("player_regionOfMap", SceneNameToInt(SceneManager.GetActiveScene().name));
        formData.AddField("unity_android_connection_id", APIReq.unity_android_connection_id.ToString());
        StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/playerStats/createPositions", formData));
        StartCoroutine(UpdatePlayerStats());
    }

    private int SceneNameToInt(string sceneName)
    {
        int sceneNum = 6;

        if(sceneName == "BunkerInside")
        {
            sceneNum = 1;
        }
        else if(sceneName == "BunkerOutside")
        {
            sceneNum = 2;
        }
        else if (sceneName == "Road")
        {
            sceneNum = 3;
        }
        else if (sceneName == "Farm")
        {
            sceneNum = 4;
        }
        else if (sceneName == "City")
        {
            sceneNum = 5;
        }

        return sceneNum;
    }
}
