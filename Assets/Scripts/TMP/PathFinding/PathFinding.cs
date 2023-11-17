using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PathFinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 1;

    private List<Position> walkables;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private Graph graph;

    public void Update()
    {
        graph = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
        if (graph._initialized)
        {
            IEnumerable<Position> topology = graph.WalkableNodes();
            walkables = new List<Position>();
            foreach (var t in topology)
            {
                if (graph.GetMeta(t).IsWalkable)
                {
                    walkables.Add(t);
                }
            }
        }
    }

    public List<Vector3> FindPath(Position start)
    {
        Position end = new(x: 16, y: 24);
        // achar posicao de entidade mais proxima
        /*
        {
            new Vector3(13, 14),
            new Vector3(11, 13),
            new Vector3(8, 14),
            new Vector3(7, 15)
        }
        */
        // return new List<Vector3> { new Vector3(start.X, start.Y), new Vector3(end.X, end.Y), };
        return FindPath(start, end);
    }

    public List<Vector3> FindPath(Position start, Position end)
    {
        List<PathNode> path = FindPath(new PathNode(graph.GetMeta(start)), new PathNode(graph.GetMeta(end)));
        Debug.Log("Caminho ENCONTRADO: " + (path == null ? "null" : path.ToString()));
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            Debug.Log("Tamanho caminho: " + vectorPath.Count);
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y));
            }
            return vectorPath;
        }
    }

    public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
    {
        Debug.Log("Terceiro FindPath PathNodes: start > " + startNode + " end > " + endNode);
        if (startNode == null || endNode == null)
        {
            Debug.Log("Caminho invalido");
            // Invalid Path
            return null;
        }

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        // for (int x = 0; x < grid.GetWidth(); x++)
        // {
        //     for (int y = 0; y < grid.GetHeight(); y++)
        //     {
        //         PathNode pathNode = grid.GetGridObject(x, y); // get no atual
        //         pathNode.gCost = 99999999;
        //         pathNode.CalculateFCost();
        //         pathNode.cameFromNode = null;
        //     }
        // }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        Debug.Log("hCost: " + startNode.hCost);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                // Reached final node
                Debug.Log("The end: " + endNode);
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            Debug.Log("Neighbors de " + currentNode + ": " + GetNeighbourList(currentNode).Count);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.bgTile.IsWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        IEnumerable<Position> neighbors;

        if (walkables.Contains(currentNode.bgTile.Position))
        {
            neighbors = graph.WalkableNeighbors(currentNode.bgTile.Position);

            foreach (var n in neighbors)
            {
                if (graph.GetMeta(n).IsWalkable)
                {
                    neighbourList.Add(new PathNode(graph.GetMeta(n)));
                }
            }
        }

        return neighbourList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        Debug.Log("Entrou");
        List<PathNode> path = new List<PathNode>
        {
            endNode
        };
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        Debug.Log(path);
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

}
