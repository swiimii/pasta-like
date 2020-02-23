using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    public bool invulnerable;

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed);
    }

    public void Damage(int damage)
    {
        if(!invulnerable)
        {
            var pc = GetComponent<PlayerController>();
            pc.currentHealth -= damage;

            if (pc.currentHealth <= 0)
            {
                StartCoroutine("Death");
            }
            else
            {
                StartCoroutine("Invulnerable");
            }
        }
    }
    
    public IEnumerator Invulnerable()
    {
        invulnerable = true;

        float invulnerableTime = 2;
        invulnerable = true;
        while (invulnerableTime > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            yield return new WaitForSeconds(.15f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.15f);
            invulnerableTime -= .3f;
        }
        invulnerable = false;
    }

    public IEnumerator Death()
    {
        GetComponent<PlayerController>().enabled = false;
        float delayTime = 1.5f;
        while (delayTime > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            yield return new WaitForSeconds(.15f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.15f);
            delayTime -= .3f;
        }
        GetComponent<SpriteRenderer>().enabled = false;
        var gs = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        gs.ShowDeathScreen();
    }

}
