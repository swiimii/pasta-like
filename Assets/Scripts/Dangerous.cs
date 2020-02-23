using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehavior>().Damage(damage);
        }
    }
}
