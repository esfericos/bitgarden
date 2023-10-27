using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall : MonoBehaviour
    {
        public GameObject obj;
        public Nil Exec(Number x1, Number y1)
        {
            //@TODO: check for resources
            
            // float x1f = (float) x1.AsNumber();
            // float y1f = (float) y1.AsNumber();
            
            // GameObject wall = Instantiate(obj);
            // wall.transform.position = new Vector3(x1f, y1f);
            
            Debug.Log("Creating wall at" + x1 + ","+y1);

            return new Nil();
        }
    }
}