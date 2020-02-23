using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Death");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehavior>().Damage(1);
        }
        Destroy(gameObject);
    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
