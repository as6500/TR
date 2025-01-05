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
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                collectibles.Add(transform.GetChild(i).GetChild(j).gameObject);
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
            }        
        }

        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStatsMetalDetector>();
        playerStats.SetTreasures(this);
    }

    public void ShowFoundTreasures(List<int?> treasuresIds)
    {
        for (int i = 0; i < treasuresIds.Count; i++)
        {
            Debug.Log(treasuresIds[i]);
            if (treasuresIds[i] != null)
            {
                for (int j = 0; j < collectibles.Count; j++)
                {
                    Treasure collectibleStats = collectibles[j].GetComponent<Treasure>();
                    Debug.Log(collectibles[j]);
                    if (treasuresIds[i] == collectibleStats.id)
                    {
                        if (SceneManager.GetActiveScene().name == collectibleStats.sceneName && !collectibleStats.caught)
                        {
                            collectibles[j].SetActive(true);
                        }
                        else
                        {
                            collectibles[j].SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
