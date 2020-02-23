using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = .04f;
    public virtual void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed;
    }
}
