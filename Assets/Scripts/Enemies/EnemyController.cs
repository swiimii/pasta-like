using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 originOffset;
    private bool delayed = false;
    private void Start()
    {
        direction = new Vector3(Random.value, Random.value, 0).normalized;
        if(direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void FixedUpdate()
    {
        GetComponent<EnemyBehavior>().Move(direction);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!delayed)
        {
            StartCoroutine("NewDirection");
        }
    }

    public IEnumerator NewDirection()
    {
        delayed = true;
        direction = new Vector3(Random.value - .5f, Random.value - .5f, 0).normalized;
        yield return new WaitForSeconds(.5f);
        delayed = false;
    }

}
