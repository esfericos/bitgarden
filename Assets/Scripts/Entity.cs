using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract Price Price { get; set; }
    public abstract void Start(); // has to initialize Price

    public virtual void Render(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
    }
}