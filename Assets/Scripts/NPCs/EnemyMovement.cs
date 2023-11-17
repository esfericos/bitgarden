using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    public float distance = 0.01f;    //distance required to move to next position

    private Vector3 target;
    private int pathIndex = 0;
    private PathFinding pathFinding;

    // private void Start()
    // {
    //     target = LevelManager.main.path[pathIndex];
    // }

    // private void Update()
    // {
    //     if (Vector2.Distance(target.position, transform.position) <= 0.1f)
    //     {
    //         pathIndex++;

    //         if (pathIndex == LevelManager.main.path.Length)
    //         {
    //             pathIndex = 0;
    //             return;
    //         }
    //         else
    //         {
    //             target = LevelManager.main.path[pathIndex];
    //         }
    //     }
    // }

    // private void FixedUpdate()
    // {
    //     Vector2 direction = (target.position - transform.position).normalized;

    //     rb.velocity = direction * moveSpeed;
    // }


    void Start()
    {
        pathFinding = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<PathFinding>();
        LevelManager.main.path = pathFinding
            .FindPath(new Position(
                x: (ushort)(gameObject.transform.position.x - 1),
                y: (ushort)(gameObject.transform.position.y - 1)))
            .ToArray();
        Debug.Log("CAMINHO RECEBIDO: " + LevelManager.main.path.ToString());

        // LevelManager.main.path = new List<Vector3>() {
        //         new Vector3(13, 14),
        //         new Vector3(11, 13),
        //         new Vector3(8, 14),
        //         new Vector3(7, 15)
        //     }.ToArray();



        pathIndex = 0;
        target = LevelManager.main.path[pathIndex];
    }

    void Update()
    {
        if (LevelManager.main.path.Length <= pathIndex) { }
        else if (Vector3.Distance(target, transform.position) < distance)
        {
            pathIndex++;
            if (pathIndex < 0)
                pathIndex = 0;

            target = LevelManager.main.path[pathIndex % LevelManager.main.path.Length]
                    + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (LevelManager.main.path != null)
        {
            foreach (Vector3 waypoint in LevelManager.main.path)
            {
                Gizmos.DrawSphere(waypoint, 0.1f);
            }
        }
    }
}
