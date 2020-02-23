﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    public float damage = .5f;

    private void Start()
    {
        StartCoroutine("Death");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyBehavior>())
        {
            collision.gameObject.GetComponent<EnemyBehavior>().Damage(damage);            
        }
        Destroy(gameObject);
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}