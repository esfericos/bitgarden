using UnityEngine;
using UnityEditor;
using Interfaces;

public class Turret : Entity, IDamageable
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private HealthBarBehaviour HealthBar;
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private AudioSource shootingSound;

    [Header("Attribute")]
    [SerializeField] private float range = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;
    [SerializeField] private int Hitpoints;
    [SerializeField] private int MaxHitpoints = 5;

    private Transform _target;
    private float _timeUntilFire;

    public override Price Price { get; set; }
    public override void Start()
    {
        Price = new Price(gold: 25);
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);

    }

    private void Update()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();
        if (!CheckTargetInRange())
        {
            _target = null;
        }
        else
        {
            _timeUntilFire += Time.deltaTime;
            if (_timeUntilFire >= 1f / bps)
            {
                Shoot();
                _timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        shootingSound.Play();
        GameObject arrowObj = Instantiate(arrowPrefab, turretRotationPoint.position, Quaternion.identity);
        Arrow arrowScript = arrowObj.GetComponent<Arrow>();
        arrowScript.SetTarget(_target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            _target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg + 45f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= range;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, Vector3.forward, range);
    }
    
    public void TakeDamage(int damage)
    {   

        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}