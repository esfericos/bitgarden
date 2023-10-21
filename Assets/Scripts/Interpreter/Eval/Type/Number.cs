using System;

namespace Interpreter.Eval.Type
{
    public class Number : Value
    {
        public readonly double Value;
        
        public Number(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Number({Value})";
        }

        public override bool Equals(Value other)
        {
            if (other == null) return false;
            if (GetType() != other.GetType()) return false;
            return Math.Abs(Value - ((Number)other).Value) < double.Epsilon;
        }
    }
}