using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Tooltip("Does this Spawner counts more than 1 SpawnPoints fill this list")]
    public List<Transform> OtherSpawnPoints;

    [Tooltip("If the Player is in one of this areas: Spawn definitely enemys as many DefinitelySpawnCount specified")]
    public List<Transform> DefinitelySpawnPoints;

    [Tooltip("Add all Enemy Prefabs in this list")]
    public List<GameObject> EnemyPrefabs;

    public Transform Player;

    public bool DefinitelySpawn = false;
    public bool SpawnActive = true;
    [Range(0, 20)]
    public float MinimumSpawnDelay = 0;
    [Range(0, 20)]
    public float MaximumSpawnDelay = 0;
    public float PlayerDistanceForSpawn = 10;
    public bool StartSpawn = false;
    private GameObject currentEnemy;

    void Awake()
    {
        GameObject enemys = new GameObject("Enemys");
    }

    void Update()
    {
        if (StartSpawn)
        {
            StartSpawn = false;
            SpawnActive = true;
            spawn();
        }
    }


    bool playerInDistance()
    {
        return Vector3.Distance(transform.position, Player.position) >= PlayerDistanceForSpawn;
    }

    void checkDefinitelySpawn()
    {
        if (DefinitelySpawnPoints.Count == 0)
            return;

        if (DefinitelySpawn)
        {
            spawn();
            DefinitelySpawn = false;
        }
    }
    float setDelay()
    {
        return Random.Range(MinimumSpawnDelay, MaximumSpawnDelay);
    }

    void spawn()
    {
        //if (SpawnActive)
        //{
            currentEnemy = GameObject.Instantiate(EnemyPrefabs[0]);
            StartCoroutine(SpawnDelay(setDelay()));
        //}
    }

    IEnumerator SpawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (SpawnActive)
        {
            currentEnemy = GameObject.Instantiate(EnemyPrefabs[0]);
            StartCoroutine(SpawnDelay(setDelay()));
        }
    }
}
