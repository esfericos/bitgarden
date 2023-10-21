using System;

namespace Interpreter.Eval
{
    // Yes, this is terribly inefficient, but who cares, anyway?
    
    public abstract class Value : IEquatable<Value>
    {
        public bool IsNumber => this is Number;
        public bool IsString => this is String;
        
        public double AsNumber => ThisAs<Number>().Value;
        public string AsString => ThisAs<String>().Value;

        public static Value WrapNumber(double value) => new Number(value);
        public static Value WrapString(string value) => new String(value);

        public abstract override string ToString();

        private T ThisAs<T>() where T : Value
        {
            try
            {
                return (T)this;
            }
            catch (InvalidCastException)
            {
                throw new EvalException($"Expected type {typeof(T).Name}, but got {this.GetType().Name}");
            } 
        }

        public static bool IsValueType(Type type)
        {
            return type == typeof(Value) || type.IsSubclassOf(typeof(Value));
        }

        public abstract bool Equals(Value other);
    }

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