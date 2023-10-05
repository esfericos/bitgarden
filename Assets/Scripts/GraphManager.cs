using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GraphManager : MonoBehaviour
{

    private MapPainter tilemap;
    private Graph graph;
    
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