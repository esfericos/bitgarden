using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonParser : MonoBehaviour
{

    MapPainter tilemap;
    public TextAsset jsonFile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("MapPainterHandler").GetComponent<MapPainter>();

        string jsonString = jsonFile.ToString();
        TopologyData data = JsonConvert.DeserializeObject<TopologyData>(jsonString);

        foreach(var metaEntry in data.Meta)
        {
            int x = metaEntry.Value.Position[0];
            int y = metaEntry.Value.Position[1];
            string type = metaEntry.Value.Type;
            Debug.Log($"posicao[{x},{y}] e type: {type}");

            tilemap.Paint(type, x, y);
        }
    }

    public class TopologyData
    {
        [JsonProperty("topology")]
        public Dictionary<long, string> Topology { get; set; }

        [JsonProperty("meta")]
        public Dictionary<int, MetaData> Meta { get; set; }
    }

    public class MetaData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("position")]
        public List<int> Position { get; set; }
    }


}
