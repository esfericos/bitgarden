using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class GraphManager : MonoBehaviour
{

    private MapPainter tilemap;
    private Graph graph;

    public Entity turret;
    public EnemyCastle enemyCastle;

    public TextAsset jsonFile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("MapPainterHandler").GetComponent<MapPainter>();
        graph = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();

        string jsonString = jsonFile.ToString();
        RawWorld rawWorld = JsonConvert.DeserializeObject<RawWorld>(jsonString);

        graph.Initialize(rawWorld.Topology, rawWorld.Meta);

        foreach (var tile in graph.AllMeta()) tilemap.Paint(tile);

        AddEntity(turret, new Position(x: 8, y: 7));
        AddEntity(turret, new Position(x: 8, y: 5));
        AddEntity(enemyCastle, new Position(x: 2, y: 11));
        enemyCastle.SpawnEnemies(new Position(x: 2, y: 11));
    }

    /// <summary>
    /// Adds an entity to the graph and renders it on the map.
    /// </summary>
    public void AddEntity(Entity entity, Position pos)
    {
        if (graph.IsAvailableToBuild(pos))
        {
            graph.AddEntity(entity, pos);
            tilemap.PaintEntity(entity, pos);
        }
        else
        {
            throw new Exception($"Tile at {pos} unavailable");
        }
    }

    public class RawWorld
    {
        [JsonProperty("topology")]
        public Dictionary<string, string> Topology { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, RawMetadataEntry> Meta { get; set; }
    }

    public class RawMetadataEntry
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}