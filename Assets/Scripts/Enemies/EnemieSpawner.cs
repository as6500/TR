using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject mutantTreePrefab;
    [SerializeField] private GameObject mutantHumanPrefab;
    [SerializeField] private GameObject smallWormPrefab;
    [SerializeField] private GameObject bigWormPrefab;
    [SerializeField] private GameObject[] enemiesChosen = new GameObject[4];

    [Header("Spawn Enemy Type")]
    [SerializeField] private bool mutantTree;
    [SerializeField] private bool mutantHuman;
    [SerializeField] private bool smallWorm;
    [SerializeField] private bool bigWorm;

    [Header("Spawners")]
    [SerializeField] private GameObject[] spawnPoint;

    [Header("Spawner Settings")]
    [SerializeField] private int spawnRatePerSeconds = 1;
    [SerializeField] private int spawnDelaySeconds = 1;
    [SerializeField] private int maxDistFromPlayer = 10;
    [SerializeField] private int maxEnemiesInGame = 10;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelaySeconds);

        if (CanSpawnEnemy())
        {
            CheckEnemyType();
            for (int i = 0; i < spawnRatePerSeconds; i++)
            {
                GameObject enemy = GetRandomEnemy();
                GameObject spawn = GetRandomSpawnPointCloseToPlayer();

                if (spawn != null) Instantiate(enemy, spawn.transform);
            }
        }

        StartCoroutine(SpawnEnemy());
    }

    private bool CanSpawnEnemy()
    {
        int enemiesSpawned = 0;

        for(int i = 0; i < spawnPoint.Length; i++)
        {
            enemiesSpawned += spawnPoint[i].transform.childCount;
        }

        return enemiesSpawned < maxEnemiesInGame;
    }

    private GameObject GetRandomEnemy()
    {
        int tempEnemy = Random.Range(0, enemiesChosen.Length);

        Debug.Log(tempEnemy);

        if (enemiesChosen[tempEnemy] != null)
        {
            return enemiesChosen[tempEnemy];
        }

        return GetRandomEnemy();
    }

    private GameObject GetRandomSpawnPointCloseToPlayer()
    {
        GameObject[] tempSpawnPoint = new GameObject[spawnPoint.Length];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject chosenSpawnPoint = null;

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            float currentDistance = Mag(spawnPoint[i].transform.position - player.transform.position);

            if (currentDistance <= maxDistFromPlayer)
            {
                tempSpawnPoint[i] = spawnPoint[i];
            }
        }

        while (chosenSpawnPoint == null)
        {
            chosenSpawnPoint = tempSpawnPoint[Random.Range(0, tempSpawnPoint.Length)];
        }

        return chosenSpawnPoint;
    }

    private void CheckEnemyType()
    {
        if (mutantTree && mutantTreePrefab != null)
        {
            enemiesChosen[0] = mutantTreePrefab;
        }
        else if (mutantHuman && mutantHumanPrefab != null)
        {
            enemiesChosen[1] = mutantHumanPrefab;
        }
        else if (smallWorm && smallWormPrefab != null)
        {
            enemiesChosen[2] = smallWormPrefab;
        }
        else if (bigWorm && bigWormPrefab != null)
        {
            enemiesChosen[3] = bigWormPrefab;
        }
    }

    private Vector3 Norm(Vector3 vec)
    {
        float mag = Mag(vec);

        if (mag != 0)
        {
            return vec / mag;
        }
        return vec;
    }

    private float Mag(Vector3 vec)
    {
        return Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y));
    }
}
