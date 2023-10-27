using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall
    {
        private readonly GraphManager _gm;
        public Entity wall;
        private GraphManager GraphHandler;

        public CreateWall(GraphManager gm)
        {
            this._gm = gm;
        }


        public Nil Exec(Number x, Number y)
        {
            GraphHandler = GameObject.FindGameObjectWithTag("GraphData").GetComponent<GraphManager>();
            //@TODO: check for resources

            ushort xf = (ushort)x.AsNumber();
            ushort yf = (ushort)y.AsNumber();

            GraphHandler.AddTowerGambiarra(xf, yf);

            return new Nil();
        }
    }
}