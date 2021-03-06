﻿//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Tooltip("Does this Spawner counts more than 1 SpawnPoints fill this list")]
    public Transform[] OtherSpawnPoints;

    [Tooltip("If the Player is in one of this areas: Spawn definitely enemys as many DefinitelySpawnCount specified")]
    public List<Transform> DefinitelySpawnPoints;

    [Tooltip("Add all Enemy Prefabs in this list")]
    public List<GameObject> EnemyPrefabs;

    public Transform Player;

    public bool DefinitelySpawn = false;
    public bool SpawnActive = false;
    [Range(0, 20)]
    public float MinimumSpawnDelay = 0;
    [Range(0, 20)]
    public float MaximumSpawnDelay = 0;
    public bool StartWithoutDistance = false;
    public bool StartWithDistance = false;
    public float PlayerDistanceForSpawn = 10;

    private GameObject currentEnemy;
    private GameObject enemys;
    private GameTime gameTime;
    private bool paused;

    void Awake()
    {
        gameTime = GameObject.Find("GlobalScripts").GetComponent<GameTime>();
        enemys = new GameObject(transform.name + "_enemys");
        enemys.transform.SetParent(transform);
    }

    void Update()
    {
        if (gameTime.PlayTime == 0)
        {
            paused = true;
        }
        else
        {
            if (paused)
            {
                paused = false;
                StartCoroutine(SpawnDelay(setDelay()));
            }

            checkDefinitelySpawn();

            if (StartWithoutDistance && !StartWithDistance)
            {
                StartWithDistance = false;
                StartWithoutDistance = false;
                SpawnActive = true;
                spawn();
            }
            else if (StartWithDistance && !StartWithoutDistance)
            {
                StartWithoutDistance = false;
                if (playerInDistance() && !SpawnActive)
                {
                    SpawnActive = true;
                    spawn();
                }
                if (!playerInDistance() && SpawnActive)
                {
                    SpawnActive = false;
                }
            }
        }
    }


    bool playerInDistance()
    {
        return (Vector3.Distance(transform.position, Player.position) <= PlayerDistanceForSpawn);
    }

    void checkDefinitelySpawn()
    {
        if (DefinitelySpawnPoints.Count == 0)
            return;

        if (DefinitelySpawn)
        {
            currentEnemy = (GameObject)Instantiate(EnemyPrefabs[0], transform.position, Quaternion.identity);
            currentEnemy.transform.SetParent(enemys.transform);
            DefinitelySpawn = false;
            if (OtherSpawnPoints.Length > 0)
            {
                for (int i = 0; i < OtherSpawnPoints.Length; i++)
                {
                    currentEnemy = (GameObject)Instantiate(EnemyPrefabs[0], OtherSpawnPoints[i].position, Quaternion.identity);
                    currentEnemy.transform.SetParent(enemys.transform);
                }
            }
        }
    }
    float setDelay()
    {
        return Random.Range(MinimumSpawnDelay, MaximumSpawnDelay);
    }

    void spawn()
    {
        if (gameTime.PlayTime == 1)
        {
            if (OtherSpawnPoints.Length > 0)
            {
                for (int i = 0; i < OtherSpawnPoints.Length; i++)
                {
                    currentEnemy = (GameObject)Instantiate(EnemyPrefabs[0], OtherSpawnPoints[i].position, Quaternion.identity);
                }
            }
            currentEnemy = (GameObject)Instantiate(EnemyPrefabs[0], transform.position, Quaternion.identity);
            currentEnemy.transform.SetParent(enemys.transform);
            StartCoroutine(SpawnDelay(setDelay()));
        }
    }

    IEnumerator SpawnDelay(float delay)
    {
        if (gameTime.PlayTime == 1)
        {
            yield return new WaitForSeconds(delay);
            if (SpawnActive)
            {
                if (OtherSpawnPoints.Length > 0)
                {
                    for (int i = 0; i < OtherSpawnPoints.Length; i++)
                    {
                        currentEnemy = (GameObject)Instantiate(EnemyPrefabs[0], OtherSpawnPoints[i].position, Quaternion.identity);
                    }
                }
                currentEnemy = (GameObject)Instantiate(EnemyPrefabs[0], transform.position, Quaternion.identity);
                currentEnemy.transform.SetParent(enemys.transform);
                StartCoroutine(SpawnDelay(setDelay()));
            }
        }
    }
}
