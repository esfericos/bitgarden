using System;

namespace Interpreter.Eval.Type
{
    public abstract class Value : IEquatable<Value>
    {
        public bool IsNumber => this is Number;
        public bool IsString => this is String;
        public bool IsNil => this is Nil;
        
        public double AsNumber() => ThisAs<Number>().Value;
        public string AsString() => ThisAs<String>().Value;
        public void AsNil()
        {
            ThisAs<Nil>();
        }

        public static Value WrapNumber(double value) => new Number(value);
        public static Value WrapString(string value) => new String(value);
        public static Value WrapNil() => new Nil();

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

        public static bool IsValueType(System.Type type)
        {
            return type == typeof(Value) || type.IsSubclassOf(typeof(Value));
        }

        public abstract bool Equals(Value other);
    }
}