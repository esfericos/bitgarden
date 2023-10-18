using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public virtual void Render(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
    }
}