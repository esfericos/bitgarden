namespace Interpreter.Eval.Type
{
    public class Nil : Value
    {
        public override string ToString()
        {
            return $"Nil";
        }

        public override bool Equals(Value other)
        {
            if (other == null) return false;
            return GetType() == other.GetType();
        }
    }
}