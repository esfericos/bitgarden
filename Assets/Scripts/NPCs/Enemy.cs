using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Interfaces;

public class Enemy : MonoBehaviour, IDamageable
{
    public AudioSource HitSound;
    public int Hitpoints;
    public int MaxHitpoints = 5;
    public HealthBarBehaviour HealthBar;
    public int EnemyDamage = 2;
    public bool canDamage = true;
    private bool _canTakeDamage = true;
    private float coolDown = 1.25f;
    private Animator _animator;
    private Inventory inventory;

    public void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        inventory = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Inventory>();
    }

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {
            Hitpoints -= damage;
            HitSound.Play();
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);

            if (Hitpoints <= 0)
            {
                _canTakeDamage = false;
                inventory.IncreasesGoldBy(1);
                _animator.SetTrigger("Die");
                Destroy(gameObject, 1f);
            }
        }
    }

    private IEnumerator OnCollisionStay2D(Collision2D other)
    {

        IDamageable structure = other.gameObject.GetComponent<IDamageable>();

        if (other.gameObject.CompareTag("Structure") && canDamage)
        {
            canDamage = false;

            structure.TakeDamage(EnemyDamage);

            yield return new WaitForSeconds(coolDown);

            canDamage = true;

        }

    }
}