using Interpreter.Eval.Type;

namespace Interpreter.Lib.Math
{
    public class Add
    {
        public Number Exec(Number a, Number b)
        {
            return new Number(a.AsNumber() + b.AsNumber());
        }
    }
}