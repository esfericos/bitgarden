using System.Collections.Generic;
using UnityEngine;

// Trem q tem q fazer ainda
// Nao iniciar a fase de uma vez, colocar tipo um botao de play
// Habilitar o sistema de comprar antes de comecar a partida para poder colocar as estruturas
// Deixar habilitado todas as funcaoes para gerar as estruturas 
// Gerar a posicao do portal e o core de forma randomica
// funcao para pegar a entidade mais proxima 
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
    private float randomMovement;
    private Graph graph;
    private int quantityEntities;
    private Position endPosition;
    public Vector3[] path;

    // private void Start()
    // {
    //     target = path[pathIndex];
    // }

    // private void Update()
    // {
    //     if (Vector2.Distance(target.position, transform.position) <= 0.1f)
    //     {
    //         pathIndex++;

    //         if (pathIndex == path.Length)
    //         {
    //             pathIndex = 0;
    //             return;
    //         }
    //         else
    //         {
    //             target = path[pathIndex];
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
        graph = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
        quantityEntities = graph.entities.Length;
        Position current = new Position(
            x: (ushort)(gameObject.transform.position.x - 1),
            y: (ushort)(gameObject.transform.position.y - 1));
        Position end = getClosestEntity(current);
        path = pathFinding.FindPath(current, end).ToArray();

        randomMovement = Random.Range(0.3f, 0.7f);
        pathIndex = 0;
        target = path[pathIndex];
    }

    void Update()
    {
        if (path.Length <= pathIndex) { }
        else if (Vector3.Distance(target, transform.position) < distance)
        {
            pathIndex++;
            if (pathIndex < 0)
                pathIndex = 0;

            target = path[pathIndex % path.Length] + new Vector3(randomMovement, randomMovement);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        if (quantityEntities != graph.entities.Length)
        {
            if (graph.entities.Length != 0)
            {
                quantityEntities = graph.entities.Length;
                Position start = new Position(
                    x: (ushort)(gameObject.transform.position.x),
                    y: (ushort)(gameObject.transform.position.y));
                Position end = getClosestEntity(start);
                path = pathFinding.FindPath(start, end)
                    .ToArray();
                // randomMovement = Random.Range(0.3f, 0.7f);
                pathIndex = 0;
                target = path[pathIndex];
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            foreach (Vector3 waypoint in path)
            {
                Gizmos.DrawSphere(waypoint, 0.1f);
            }
        }
    }
    // Calcular a distancia do no inicial para todas as entidades depois retornar a menor
    private Position getClosestEntity(Position start)
    {
        int closest = 9999999;
        Position end = new();
        foreach (var pos in graph.entities)
        {
            int distance =
                pathFinding.CalculateDistanceCost(graph.walkables[start], graph.walkables[Position.FromId(pos)]);
            if (closest > distance)
            {
                closest = distance;
                end = Position.FromId(pos);
            }
        }

        return end;
    }
}
