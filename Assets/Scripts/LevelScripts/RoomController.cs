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
            var gsLvl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().levelNum;
            SpawnEnemies(numEnemies + gsLvl);
            enemiesLeft = numEnemies + gsLvl;
        }        
    }

    public void SpawnEnemies(int numEnemies)
    {
        
        for (int i = 0; i < numEnemies; i++)
        {
            var position = new Vector3(Random.value * 1.8f - .9f, Random.value * 1.8f - .9f);
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
