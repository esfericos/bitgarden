using Interpreter.Eval;

namespace Interpreter.Parser.Ast
{
    public class Literal : Expr
    {
        public readonly Value Value;

        public Literal(Value value)
        {
            Value = value;
        }
        
        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }
}