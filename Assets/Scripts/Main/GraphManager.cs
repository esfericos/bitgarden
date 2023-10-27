using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class GraphManager : MonoBehaviour
{

    private MapPainter tilemap;
    private Graph graph;
    private Store store;
    public Grid gridGamb;

    public Entity turret;
    public EnemyCastle enemyCastle;
    public GameObject wall;
    public GameObject tower;

    public TextAsset jsonFile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("MapPainterHandler").GetComponent<MapPainter>();
        graph = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
        store = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Store>();

        string jsonString = jsonFile.ToString();
        RawWorld rawWorld = JsonConvert.DeserializeObject<RawWorld>(jsonString);

        graph.Initialize(rawWorld.Topology, rawWorld.Meta);

        foreach (var tile in graph.AllMeta()) tilemap.Paint(tile);

        AddEntity(turret, new Position(x: 7, y: 15));
        AddEntity(turret, new Position(x: 7, y: 12));
        AddEntity(enemyCastle, new Position(x: 12, y: 14));
        // AddEntity(wall, new Position(x: 10, y: 14));
        enemyCastle.SpawnEnemies(new Position(x: 12, y: 14));
    }

    /// <summary>
    /// Adds an entity to the graph and renders it on the map.
    /// </summary>
    public void AddEntity(Entity entity, Position pos)
    {
        entity.Start();
        if (graph.IsAvailableToBuild(pos))
        {
            if (store.Buy(entity.Price))
            {
                graph.AddEntity(entity, pos);
                tilemap.PaintEntity(entity, pos);
            }
            else
            {
                throw new Exception("Not enough resources to build");
            }
        }
        else
        {
            throw new Exception($"Tile at {pos} unavailable");
        }
    }

    public void AddTowerGambiarra(ushort xf, ushort yf)
    {

        Vector3 posicao = new Vector3(xf + 0.55f, yf + 0.5f, 0);
        Instantiate(wall, posicao, Quaternion.identity);
    }

    public void AddTower2Gambiarra(ushort xf, ushort yf)
    {

        Vector3 posicao = new Vector3(xf + 0.55f, yf + 1f, 0);
        Instantiate(tower, posicao, Quaternion.identity);
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