using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : APIRequests
{
    [SerializeField] private GameObject[] collectibles;
    [SerializeField] private int[] collectiblesIds;
    [SerializeField] private List<Vector2> collectiblesPositions;
    [SerializeField] private List<int> collectiblesRegionOfMap;

    void Start()
    {
#if UNITY_EDITOR
        collectiblesIds = new int[collectibles.Length];
        CheckCollectiblesStats();
#else

#endif
    }

    private void CheckCollectiblesStats()
    {
        for(int i = 0; i < collectibles.Length; i++)
        {
            Treasure collectible = collectibles[i].GetComponent<Treasure>();
            collectiblesIds[i] = collectible.id;
            collectiblesPositions.Add(collectible.position);
            collectiblesRegionOfMap.Add((int) collectible.mapRegion + 1);

            WWWForm formData = new WWWForm();
            formData.AddField("collectible_id", collectiblesIds[i].ToString());
            formData.AddField("collectible_positionX", collectiblesPositions[i].x.ToString());
            formData.AddField("collectible_positionY", collectiblesPositions[i].y.ToString());
            formData.AddField("collectible_regionOfMap", collectiblesRegionOfMap[i].ToString());
            StartCoroutine(PostRequest("https://the-rumble-server.vercel.app/collectiblesStats/setPositions", formData));
        }
    }
}
