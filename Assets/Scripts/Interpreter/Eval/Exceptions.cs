using System;

namespace Interpreter.Eval
{
    public class EvalException : Exception
    {
        public EvalException(string message) : base(message) {}
    }

    public class ParseException : EvalException
    {
        public ParseException(string message) : base(message) {}
    }
}