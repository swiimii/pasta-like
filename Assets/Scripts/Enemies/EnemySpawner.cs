using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    // Start is called before the first frame update
    public GameObject SpawnEnemy(GameObject enemy, Vector3 position, Vector3 roomPosition)
    {
        var output = Instantiate(enemy);
        output.transform.position = new Vector3(position.x + roomPosition.x, position.y + roomPosition.y, output.transform.position.z);
        return output;
        
    }
}
