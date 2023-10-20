using System;

namespace Interpreter.Eval
{
    // Yes, this is terribly inefficient, but who cares, anyway?
    
    public abstract class Value
    {
        public bool IsNumber => this is Number;
        public bool IsString => this is String;
        
        public double AsNumber => ThisAs<Number>().Value;
        public string AsString => ThisAs<String>().Value;

        public static Value WrapNumber(double value) => new Number(value);
        public static Value WrapString(string value) => new String(value);

        private T ThisAs<T>() where T : Value
        {
            try
            {
                return (T)this;
            }
            catch (InvalidCastException ex)
            {
                throw new EvalException($"Expected type {typeof(T).Name}, but got {this.GetType().Name}");
            } 
        }
    }

    public class Number : Value
    {
        public readonly double Value;
        
        public Number(double value)
        {
            Value = value;
        }
    }

    public class String : Value
    {
        public readonly string Value;

        public String(string value)
        {
            Value = value;
        }
    }
}