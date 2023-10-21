namespace Interpreter.Eval.Type
{
    public class String : Value
    {
        public readonly string Value;

        public String(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"String(\"{Value}\")";
        }

        public override bool Equals(Value other)
        {
            if (other == null) return false;
            if (GetType() != other.GetType()) return false;
            return Value == ((String)other).Value;
        }
    }
}