using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed);
    }

}
