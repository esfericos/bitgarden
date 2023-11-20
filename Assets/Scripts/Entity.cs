using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract Price Price { get; set; }
    public abstract void Start(); // has to initialize Price

    public virtual void Render(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
    }

    public new virtual void Destroy(Object obj)
    {

        GameObject gameObject = obj as GameObject;

        if (gameObject != null)
        {
            Position position = new(
                x: (ushort)(gameObject.transform.position.x - 1),
                y: (ushort)(gameObject.transform.position.y - 1)
            );
            Graph grafo = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
            grafo.entities = grafo.entities.Where(ent => ent != position.ToId()).ToArray();
            Object.Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Failed to Destroy");
        }

    }
}