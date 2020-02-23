using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 direction;
    private void Start()
    {
        direction = new Vector3(Random.value, Random.value, 0).normalized;
    }

    void Update()
    {
        
    }

    public bool IsHittingWall()
    {
        // check all directions for hitting terrain

        return false;
    }
}
