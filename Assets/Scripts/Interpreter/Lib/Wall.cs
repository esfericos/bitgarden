using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall : MonoBehaviour
    {
        public Nil Exec(Number x, Number y)
        {
            //@TODO: check for resources

            ushort xf = (ushort)x.AsNumber();
            ushort yf = (ushort)y.AsNumber();

            Object wall = Instantiate(Resources.Load("Prefabs/Buildings/Wall"), new Vector3(xf, yf, 1), Quaternion.identity);
            
            return new Nil();
        }
    }
}