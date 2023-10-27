using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Tower
{
    public class CreateTower : MonoBehaviour
    {
        public GameObject obj;
        private GraphManager GraphHandler;

        public Nil Exec(Number x, Number y)
        {
            GraphHandler = GameObject.FindGameObjectWithTag("GraphData").GetComponent<GraphManager>();

            //@TODO: check for resources (waiting @ClaraOMello)

            ushort xf = (ushort)x.AsNumber();
            ushort yf = (ushort)y.AsNumber();

            GraphHandler.AddTowerGambiarra2(xf, yf);

            return new Nil();
        }
    }
}