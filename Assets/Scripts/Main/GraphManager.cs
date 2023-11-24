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
    private CameraMove camera;
    public Entity turret;
    public EnemyCastle enemyCastle;
    public Core core;
    public Entity wall;
    public GameObject tower;

    public bool newWave = false;
    // private int qtyEnemy = 0; // quantidade a mais de inimigos por rodada


    public TextAsset jsonFile;
    // Start is called before the first frame update
    public void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("MapPainterHandler").GetComponent<MapPainter>();
        graph = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
        store = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Store>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();

        string jsonString = jsonFile.ToString();
        RawWorld rawWorld = JsonConvert.DeserializeObject<RawWorld>(jsonString);

        graph.Initialize(rawWorld.Topology, rawWorld.Meta, newWave);

        int[] posCore = new int[2]; // y = 0 e x = 1
        var aux = 0;

        foreach (var rawCore in rawWorld.Core)
        {
            posCore[aux++] = rawCore.Value;
        }

        int[] posSpawners = new int[2];

        foreach (var rawSpawners in rawWorld.Spawners)
        {
            aux = 0;
            foreach (var raw in rawSpawners)
            {
                posSpawners[aux++] = raw.Value;
            }

            Position portalPosition = new Position(x: (ushort)posSpawners[0], y: (ushort)posSpawners[1]);
            AddEntity(enemyCastle, portalPosition);
            enemyCastle.SpawnEnemies(new Position(x: (ushort)(posSpawners[0] - 1), y: (ushort)(posSpawners[1] - 1)));
        }

        // Define camera max range
        int top = 0;
        int bottom = 100;
        int left = 100;
        int right = 0;

        foreach (var tile in graph.AllMeta())
        {
            tilemap.Paint(tile);
            if (tile.Kind == BgTileKind.None) continue;
            var pos = tile.Position;
            if (pos.X > right) right = pos.X;
            if (pos.X < left) left = pos.X;
            if (pos.Y > top) top = pos.Y;
            if (pos.Y < bottom) bottom = pos.Y;
        }

        // Set camera max range
        camera.SetRange(top, bottom, right, left);
        // portalPosition = new Position(x: 28, y: 23);

        // AddEntity(turret, new Position(x: 13, y: 23));
        // AddEntity(turret, new Position(x: 24, y: 26));
        // AddEntity(turret, new Position(x: 43, y: 20));
        // AddEntity(wall, new Position(x: 22, y: 25));
        // AddEntity(wall, new Position(x: 22, y: 26));
        // AddEntity(wall, new Position(x: 22, y: 24));
        // AddEntity(wall, new Position(x: 23, y: 24));
        // AddEntity(wall, new Position(x: 24, y: 24));
        // AddEntity(wall, new Position(x: 25, y: 24));
        // AddEntity(wall, new Position(x: 25, y: 25));
        // AddEntity(wall, new Position(x: 25, y: 26));
        // AddEntity(wall, new Position(x: 22, y: 27));
        // AddEntity(wall, new Position(x: 23, y: 27));
        // AddEntity(wall, new Position(x: 24, y: 27));
        // AddEntity(turret, new Position(x: 11, y: 27));
        // AddEntity(turret, new Position(x: 15, y: 27));
        // AddEntity(turret, new Position(x: 17, y: 27));
        // AddEntity(core, new Position(x: (ushort)posCore[1], y: (ushort)posCore[0]));
        AddEntity(core, new Position(x: (ushort)(posCore[1]), y: (ushort)(posCore[0])));

        // AddEntity(wall, new Position(x: 10, y: 14));
        // AddEntity(enemyCastle, new Position(x: 25, y: 11));
        // enemyCastle.SpawnEnemies(new Position(x: 25, y: 11));
    }

    public void NewWave()
    {
        graph = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
        newWave = true;
        Start();
        newWave = false;
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

    public void AddTowerGambiarra2(ushort xf, ushort yf)
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

        [JsonProperty("core")]
        public Dictionary<char, int> Core { get; set; }

        [JsonProperty("spawners")]
        public List<Dictionary<char, int>> Spawners { get; set; }


    }

    public class RawMetadataEntry
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}