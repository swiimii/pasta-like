using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    public float damage = .5f;

    private void Start()
    {
        StartCoroutine("Death");
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().SetRotation(GetComponent<Rigidbody2D>().rotation + 8f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyBehavior>())
        {
            var gsAtk = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().playerAttackMagnitude;
            collision.gameObject.GetComponent<EnemyBehavior>().Damage(damage * ( 1f + gsAtk ) );
        }
        Destroy(gameObject);
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
