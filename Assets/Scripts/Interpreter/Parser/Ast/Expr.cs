namespace Interpreter.Parser.Ast
{
    public abstract class Expr
    {
        public abstract T Accept<T>(IExprVisitor<T> visitor);
    }
    
    public interface IExprVisitor<out T>
    {
        T VisitProgram(Ast.Program node);
        T VisitCall(Ast.Call node);
        T VisitLiteral(Ast.Literal node);
    }
}