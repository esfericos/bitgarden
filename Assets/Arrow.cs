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

    private Transform _target;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(arrowDamage);
        Destroy(gameObject);
    }
}
