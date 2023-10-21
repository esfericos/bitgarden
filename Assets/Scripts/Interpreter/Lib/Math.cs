using Interpreter.Eval.Type;

// ReSharper disable once CheckNamespace
namespace Interpreter.Lib.Math
{
    public class Add
    {
        public Number Exec(Number a, Number b)
        {
            return new Number(a.AsNumber() + b.AsNumber());
        }
    }
    
    public class Sub
    {
        public Number Exec(Number a, Number b)
        {
            return new Number(a.AsNumber() - b.AsNumber());
        }
    }
    
    public class Mul
    {
        public Number Exec(Number a, Number b)
        {
            return new Number(a.AsNumber() * b.AsNumber());
        }
    }
    
    public class Div
    {
        public Number Exec(Number a, Number b)
        {
            return new Number(a.AsNumber() / b.AsNumber());
        }
    } 
}