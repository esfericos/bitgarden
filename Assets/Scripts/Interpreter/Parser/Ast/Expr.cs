namespace Interpreter.Parser.Ast
{
    public abstract class Expr
    {
        public abstract T Accept<T>(IExprVisitor<T> visitor);
    }
    
    public interface IExprVisitor<out T>
    {
        public T VisitProgram(Ast.Program node);
        public T VisitCall(Ast.Call node);
        public T VisitLiteral(Ast.Literal node);
    }
}