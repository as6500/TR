using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct UnJasonedIdsData
{
    public int player_collectibles_id;
}

public class PlayerStatsMetalDetector : APIRequests
{
    [SerializeField] private APIRequests APIReq;
    [SerializeField] private SpawnTreasure treasures;
    [SerializeField] private int secondsBetweenStatsUpdate = 1;
    private GameObject player;

    private void Start()
    {
        APIReq = gameObject.GetComponent<APIRequests>();
        FindPlayerInScene();
    }

    public void SetTreasures(SpawnTreasure treas)
    {
        treasures = treas;
    }

    private IEnumerator UpdatePlayerStats()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenStatsUpdate);
            if (SceneManager.GetActiveScene().name != "Main Menu")
            {
                SendUpdatedPlayerStats();
            }
        }
    }

    public void SendUpdatedPlayerStats()
    {
        if (player == null)
        {
            FindPlayerInScene();
        }

        bool isdead = false;

        if (0 >= player.GetComponent<HealthScript>().GetCurrentHealth())
        {
            isdead = true;
        }

        WWWForm formData = new WWWForm();
        formData.AddField("player_position_x", player.transform.position.x.ToString());
        formData.AddField("player_position_y", player.transform.position.y.ToString());
        formData.AddField("player_isdead", isdead.ToString());
        formData.AddField("player_regionOfMap", SceneNameToInt(SceneManager.GetActiveScene().name));
        formData.AddField("unity_android_connection_id", APIReq.unity_android_connection_id.ToString());
        StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/playerStats/updatePositions", formData));
    }

    private void UpdateTreasuresFound()
    {
        ShowResponseMessage();
        if (APIReq.response.collectible_found == true)
        {
            List<int?> idsList = new List<int?>();

            idsList.Add(APIReq.response.collectible_id1);
            idsList.Add(APIReq.response.collectible_id2);
            idsList.Add(APIReq.response.collectible_id3);
            idsList.Add(APIReq.response.collectible_id4);
            idsList.Add(APIReq.response.collectible_id5);
            idsList.Add(APIReq.response.collectible_id6);
            idsList.Add(APIReq.response.collectible_id7);
            idsList.Add(APIReq.response.collectible_id8);
            idsList.Add(APIReq.response.collectible_id9);
            idsList.Add(APIReq.response.collectible_id10);
            idsList.Add(APIReq.response.collectible_id11);
            idsList.Add(APIReq.response.collectible_id12);

            treasures.ShowFoundTreasures(idsList);
        }
    }

    public void CaughtTreasure(int id)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("unity_android_connection_id", APIReq.unity_android_connection_id.ToString());
        formData.AddField("collectible_id", id.ToString());
        StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/collectiblesStats/collectibleCaught", formData));
    }

    private IEnumerator CheckTreasureFound()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenStatsUpdate);
            if (SceneManager.GetActiveScene().name != "Main Menu")
            {
                if (player == null)
                {
                    FindPlayerInScene();
                }

                WWWForm formData = new WWWForm();
                formData.AddField("unity_android_connection_id", APIReq.unity_android_connection_id.ToString());
                StartCoroutine(APIReq.PostRequest("https://the-rumble-server.vercel.app/collectiblesStats/foundCollectible", formData, UpdateTreasuresFound));
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
        StartCoroutine(CheckTreasureFound());
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
