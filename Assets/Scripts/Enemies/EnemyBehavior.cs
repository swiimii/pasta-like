using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public float health, maxHealth = 3, maxHealthBarScale;
    public float moveSpeed = .04f;
    public GameObject healthBar;
    public RoomController container;

    private void Start()
    {
        health = maxHealth;
        maxHealthBarScale = healthBar.transform.localScale.x;
        var gsMg = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().enemySpeedMagnitude;
        moveSpeed *= (1 + gsMg);
    }
    public virtual void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed;
    }
    public void Damage(float damage)
    {
        health -= damage;
        healthBar.transform.localScale = new Vector3( health / maxHealth * maxHealthBarScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if( health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return null;
        container.SendMessage("EnemyDeath");
        Destroy(gameObject);
    }
}
