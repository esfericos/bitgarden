using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib
{
    public class CreateWall : MonoBehaviour
    {
        public GameObject obj;
        public Nil Exec(Number x, Number y)
        {
            GameObject wall = Instantiate(obj);
            wall.transform.position = new Vector3((float)x.AsNumber(), (float)y.AsNumber());
            return new Nil();
        }
    }
}