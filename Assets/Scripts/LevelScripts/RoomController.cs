using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool isFirstRoom = false;
    public List<GameObject> enemyPrefabs;
    public int numEnemies = 5;
    public int enemiesLeft;
    private void Start()
    {
        if(!isFirstRoom)
        {
            print("Hello");
            // create enemies
            SpawnEnemies(numEnemies);
            enemiesLeft = numEnemies;
        }        
    }

    public void SpawnEnemies(int numEnemies)
    {
        for(int i = 0; i < numEnemies; i++)
        {
            var position = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1);
            var enemy = GetComponent<EnemySpawner>().SpawnEnemy(enemyPrefabs[i % enemyPrefabs.Count], position, transform.position);
            enemy.GetComponent<EnemyBehavior>().container = this;
        }
    }

    public void EnemyDeath()
    {
        enemiesLeft -= 1;
        if(enemiesLeft == 0)
        {
            GetComponent<LockedRoom>().Unlock();
        }
    }
}
