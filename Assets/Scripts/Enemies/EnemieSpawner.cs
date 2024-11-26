using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

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

    [Header("Spawn Enemy Type")]
    [SerializeField] private EnemyType enemyChosen;

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

    void Start()
    {
        surfaceAI = GameObject.FindGameObjectWithTag("AINavMeshSurface").GetComponent<NavMeshSurface>();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnDelaySeconds);

            if (CanSpawnEnemy())
            {
                for (int i = 0; i < spawnRatePerSeconds; i++)
                {
                    GameObject spawner = GetRandomSpawnerCloseToPlayer();

                    if (spawner == null) break;

                    GameObject spawn = new GameObject();
                    spawn.transform.position = GetRandomSpawnPoint(spawner);
                    GameObject enemyPrefab = EnemyTypeToPrefab(enemyChosen);
                    GameObject enemy = Instantiate(enemyPrefab, enemyHolder.transform);
                    enemy.transform.position = spawn.transform.position;

                    if (EnemyType.MutantTree == enemyChosen)
                    {
                        surfaceAI.RemoveData();
                        surfaceAI.BuildNavMesh();
                    }
                    else if (EnemyType.SmallWorm == enemyChosen || EnemyType.BigWorm == enemyChosen)
                    {
                        Vector3 temp = new Vector3(spawner.transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z);
                        enemy.GetComponent<PatrolState>().SetPoints(spawn.transform.position, temp);
                    }
                }
            }
        }
    }

    private bool CanSpawnEnemy()
    {
        int enemiesSpawned = enemyHolder.transform.childCount;

        return enemiesSpawned < maxEnemiesInGame;
    }

    private GameObject EnemyTypeToPrefab(EnemyType enemyType)
    {
        if(enemyType == EnemyType.MutantTree && mutantTreePrefab != null)
        {
            return mutantTreePrefab;
        }
        else if (enemyType == EnemyType.MutantHuman && mutantHumanPrefab != null)
        {
            return mutantHumanPrefab;
        }
        else if (enemyType == EnemyType.SmallWorm && smallWormPrefab != null)
        {
            return smallWormPrefab;
        }
        else if (enemyType == EnemyType.BigWorm && bigWormPrefab != null)
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
