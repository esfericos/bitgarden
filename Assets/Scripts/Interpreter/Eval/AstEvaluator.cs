using System.Linq;
using Interpreter.Eval.Type;
using Interpreter.Parser.Ast;

namespace Interpreter.Eval
{
    public class AstEvaluator : IExprVisitor<Value>
    {
        private readonly Evaluator _evaluator;

        public AstEvaluator(Evaluator evaluator)
        {
            _evaluator = evaluator;
        }
        
        public Value VisitProgram(Program node)
        {
            return node.Expr.Accept(this);
        }

        public Value VisitCall(Call node)
        {
            var args = node.Args.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Accept(this));
            return _evaluator.ExecCmd(node.Name, args);
        }

        public Value VisitLiteral(Literal node)
        {
            return node.Value;
        }
    }
}