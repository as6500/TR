using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTreasure : Singleton<SpawnTreasure>
{
    [SerializeField] private PlayerStatsMetalDetector playerStats;
    [SerializeField] private List<GameObject> collectibles;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            collectibles.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.gameObject.SetActive(false);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (collectibles.Count > 0)
        {
            for (int i = 0; i < collectibles.Count; i++)
            {
                if (collectibles[i] != null)
                {
                    Treasure collectibleStats = collectibles[i].GetComponent<Treasure>();
                    if (arg0.name == collectibleStats.sceneName && !collectibleStats.caught)
                    {
                        collectibles[i].SetActive(true);
                    }
                    else
                    {
                        collectibles[i].SetActive(false);
                    }
                }
            }
        }
    }
}
