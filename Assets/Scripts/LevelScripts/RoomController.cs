using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool isFirstRoom = false;
    public List<GameObject> enemyPrefabs;
    private void Start()
    {
        if(!isFirstRoom)
        {
            print("Hello");
            // create enemies
            SpawnEnemies(1);
        }        
    }

    public void SpawnEnemies(int numEnemies)
    {
        for(int i = 0; i < numEnemies; i++)
        {
            var position = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1);
            GetComponent<EnemySpawner>().SpawnEnemy(enemyPrefabs[i % enemyPrefabs.Count], position, transform);
        }
    }
}
