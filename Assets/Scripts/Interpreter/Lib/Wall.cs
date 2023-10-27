using Interpreter.Eval.Type;
using UnityEngine;

namespace Interpreter.Lib.Wall
{
    public class CreateWall
    {
        private readonly GraphManager _gm;
        public Entity wall;

        public CreateWall(GraphManager gm)
        {
            this._gm = gm;
        }
        public Nil Exec(Number x, Number y)
        {
            //@TODO: check for resources
            
            ushort xf = (ushort) x.AsNumber();
            ushort yf = (ushort) y.AsNumber();
            
            _gm.AddEntity(wall, new Position(xf, yf));

            return new Nil();
        }
    }
}