using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Tooltip("Does this Spawner counts more than 1 SpawnPoints fill this list")]
    public Transform[] OtherSpawnPoints;
    [Tooltip("If the Player is in one of this areas: Spawn definitely enemys as many DefinitelySpawnCount specified")]
    public Transform[] DefinitelySpawnPoints;
    [Tooltip("Add all Enemy Prefabs in this list")]
    public GameObject[] EnemyPrefabs;

    public bool SpawnActive = true;
    [Range(0, 20)]
    public float MinimumSpawnDelay = 0;
    [Range(0, 20)]
    public float MaximumSpawnDelay = 0;



    void Update()
    {

    }
}
