using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void SpawnEnemy(GameObject enemy, Vector3 position, Transform parent)
    {
        var output = Instantiate(enemy, parent);
        output.transform.localPosition= new Vector3(position.x, position.y, output.transform.position.z);
        
    }
}
