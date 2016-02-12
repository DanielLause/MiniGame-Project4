using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public bool PlacedItems = true;
    public List<GameObject> Items;
    public List<Transform> SpawnPoints;
    public int ItemCount = 1;
    private List<Transform> constSpawnPoints;
    private List<Transform> alreadyInUse;
    private int chosedItem;
    private int chosedSpawn;

    private void Awake()
    {
        constSpawnPoints = SpawnPoints;
        alreadyInUse = new List<Transform>();
    }

    private void Update()
    {
        if (PlacedItems)
        {
            PlacedItems = false;
            randomSpawn();
            resetList();
        }
    }

    private void resetList()
    {
        SpawnPoints = constSpawnPoints;
        alreadyInUse.Clear();
    }

    private void randomSpawn()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            if (SpawnPoints.Count == 0)
                return;

            chosedSpawn = Random.Range(0, SpawnPoints.Count);
            chosedItem = Random.Range(0, Items.Count);

            Instantiate(Items[chosedItem], SpawnPoints[chosedSpawn].position, Quaternion.identity);
            alreadyInUse.Add(SpawnPoints[chosedSpawn]);
            SpawnPoints.Remove(SpawnPoints[chosedSpawn]);
        }
    }
}