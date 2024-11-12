using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;
using UnityEngine.UIElements;

public enum EnemyType { MutantTree, MutantHuman, SmallWorm, BigWorm }

public class EnemieSpawner : MonoBehaviour
{
    [Header("AI navigation Configurations")]
    [SerializeField] private NavMeshSurface surfaceAI;

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
    [SerializeField] private GameObject enemyHolder;

    [Header("Spawner Settings")]
    [SerializeField] private int spawnRatePerSeconds = 1;
    [SerializeField] private int spawnDelaySeconds = 1;
    [SerializeField] private int spawnArea = 7;
    [SerializeField] private int minSpawnDistFromPlayer = 16;
    [SerializeField] private int maxSpawnDistFromPlayer = 36;
    [SerializeField] private int maxEnemiesInGame = 10;

    private EnemyType enemyType;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnDelaySeconds);

            if (CanSpawnEnemy())
            {
                CheckEnemyType();
                for (int i = 0; i < spawnRatePerSeconds; i++)
                {
                    GameObject spawner = GetRandomSpawnerCloseToPlayer();

                    if (spawner == null) break;

                    Vector3 spawn = GetRandomSpawnPoint(spawner);
                    GameObject enemyPrefab = GetRandomEnemy();
                    GameObject enemy = Instantiate(enemyPrefab, enemyHolder.transform);
                    enemy.transform.position = spawn;
                }
                surfaceAI.RemoveData();
                surfaceAI.BuildNavMesh();
            }
        }
    }

    private bool CanSpawnEnemy()
    {
        int enemiesSpawned = enemyHolder.transform.childCount;

        return enemiesSpawned < maxEnemiesInGame;
    }

    private GameObject GetRandomEnemy()
    {
        int tempEnemy = Random.Range(0, enemiesChosen.Length);

        if (enemiesChosen[tempEnemy] != null)
        {
            return enemiesChosen[tempEnemy];
        }

        return GetRandomEnemy();
    }

    private GameObject EnemyTypeToPrefab(EnemyType enemyType)
    {
        if(enemyType == EnemyType.MutantTree)
        {
            return mutantTreePrefab;
        }
        else if (enemyType == EnemyType.MutantHuman)
        {
            return mutantHumanPrefab;
        }
        else if (enemyType == EnemyType.SmallWorm)
        {
            return smallWormPrefab;
        }
        else if (enemyType == EnemyType.BigWorm)
        {
            return bigWormPrefab;
        }

        Debug.Log($"Enemy {enemyType} doesn't exist.");

        return null;
    }

    private GameObject GetRandomSpawnerCloseToPlayer()
    {
        GameObject[] tempSpawnPoint = new GameObject[spawnPoint.Length];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject chosenSpawnPoint = null;
        bool noSpawnAvailable = true;

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            float currentDistance = Mag(spawnPoint[i].transform.position - player.transform.position);

            //In further progress instead of being it always a circle collider, i want to know if the object is inside an area (circled or not)
            if (currentDistance <= maxSpawnDistFromPlayer && currentDistance >= minSpawnDistFromPlayer)
            {
                tempSpawnPoint[i] = spawnPoint[i];
                noSpawnAvailable = false;
            }
        }

        if (!noSpawnAvailable)
        {
            while (chosenSpawnPoint == null)
            {
                chosenSpawnPoint = tempSpawnPoint[Random.Range(0, tempSpawnPoint.Length)];
            }
        }

        return chosenSpawnPoint;
    }

    private Vector3 GetRandomSpawnPoint(GameObject spawner)
    {
        Vector3 randomSpawn = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

        randomSpawn = spawner.transform.position + (Norm(randomSpawn) * Random.Range(0f, spawnArea));

        return randomSpawn;
    }

    private void CheckEnemyType()
    {
        if (mutantTree && mutantTreePrefab != null)
        {
            enemiesChosen[0] = mutantTreePrefab;
        }

        if (mutantHuman && mutantHumanPrefab != null)
        {
            enemiesChosen[1] = mutantHumanPrefab;
        }
        
        if (smallWorm && smallWormPrefab != null)
        {
            enemiesChosen[2] = smallWormPrefab;
        }
        
        if (bigWorm && bigWormPrefab != null)
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
