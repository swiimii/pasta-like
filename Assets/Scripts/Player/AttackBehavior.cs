using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public Transform player;
    public bool isAttacking = false;
    public int damageDealt = 1;

    private void Start()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        float time = 0;
        float range = .16f;
        float offset = .08f;
        float speed = 10;
        Vector2 direction = (Input.mousePosition - player.position - new Vector3(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight / 2)).normalized;
        float xdir = direction.x > 0 ? -90 : 90;
        var rotation = Quaternion.LookRotation(direction);
        print(rotation);
        transform.eulerAngles = new Vector3(0, 0, rotation.z * 180 + xdir);

        while (time < Mathf.PI / 2)
        {
            transform.position = player.transform.position + (Vector3)(direction * Mathf.Sin(time) * range + offset * direction);

            time += Time.deltaTime * speed;
            yield return null;
        }


        isAttacking = false;
        gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyBehavior>().Damage(damageDealt);
        Destroy(gameObject);
    }
}
