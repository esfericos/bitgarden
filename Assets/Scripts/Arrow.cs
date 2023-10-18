using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float arrowSpeed = 5f;
    [SerializeField] private int arrowDamage = 1;
    [SerializeField] private float lifeTime = 3;

    private Transform _target;

    private void Update()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        //play your sound
        yield return new WaitForSeconds(lifeTime); //waits 3 seconds
        Destroy(gameObject); //this will work after 3 seconds.
    }

    public void SetTarget(Transform _tg)
    {
        _target = _tg;
    }

    private void FixedUpdate()
    {
        if (!_target) return;
        Vector2 direction = (_target.position - transform.position).normalized;
        rb.velocity = direction * arrowSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<EnemyBehaviour>();

        if (enemy)
        {
            enemy.TakeHit(arrowDamage);
        }
        
        Destroy(gameObject);
    }
}
