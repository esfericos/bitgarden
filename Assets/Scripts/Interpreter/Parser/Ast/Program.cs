namespace Interpreter.Parser.Ast
{
    public class Program : Expr
    {
        public readonly Expr Expr;
        
        public Program(Expr expr)
        {
            Expr = expr;
        }
        
        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitProgram(this);
        }
    }
}