using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawner : MonoBehaviour
{
    public static ItemRandomSpawner itemRandomSpawner = null;

    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public int maxItemsToSpawn = 10;
    public int currentItemsCount = 0;

    private void Awake()
    {
        itemRandomSpawner = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentItemsCount < maxItemsToSpawn)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-37, -10), 1, Random.Range(-18, 8)); ;
            GameObject itemSpawned = Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Count)], randomSpawnPosition, Quaternion.Euler(-90, 0, 0));
            currentItemsCount++;
        }
    }
}
