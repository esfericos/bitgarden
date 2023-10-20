using System.Collections.Generic;

namespace Interpreter.Parser.Ast
{
    public class Call : Expr
    {
        /// <summary>
        /// The function arguments.
        /// </summary>
        public readonly IReadOnlyDictionary<string, Expr> Args;
        /// <summary>
        /// The function name.
        /// </summary>
        public readonly string Name;

        public Call(string name, IReadOnlyDictionary<string, Expr> args)
        {
            Args = args;
            Name = name;
        }
        
        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitCall(this);
        }
    }
}