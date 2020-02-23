using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAttackController : MonoBehaviour
{
    public GameObject attackPrefab, target;
    public float delay = 3f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("PeriodicAttack");
    }


    public IEnumerator PeriodicAttack()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            Attack();
        }
    }

    public void Attack()
    {
        var atk = Instantiate(attackPrefab);
        atk.GetComponent<EnemyAttack>();
        atk.transform.position = transform.position;
        atk.GetComponent<Rigidbody2D>().velocity = (target.transform.position - atk.transform.position).normalized * 3f;
    }

}
