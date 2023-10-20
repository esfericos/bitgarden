using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Parser.Ast
{
    public class AstFlattener : IExprVisitor<bool>
    {
        private readonly IList<string> _descriptors = new List<string>();
        
        public AstFlattener(Expr p)
        {
            p.Accept(this);
        }

        public IReadOnlyList<string> Descriptors => _descriptors.ToArray();
        
        public bool VisitProgram(Program node)
        {
            D("Program", () => node.Expr.Accept(this));
            return true;
        }

        public bool VisitCall(Call node)
        {
            D($"Call '{node.Name}'", () =>
            {
                foreach (var (argName, val) in node.Args)
                {
                    D($"Argument '{argName}'", () => val.Accept(this));
                }
            });
            return true;
        }

        public bool VisitLiteral(Literal node)
        {
            D($"Literal {node.Value}");
            return true;
        }

        private void D(string description)
        {
            _descriptors.Add(description);
        }

        private void D(string description, Action fn)
        {
            _descriptors.Add($"(begin) {description}");
            fn();
            _descriptors.Add($"(end) {description}");
        }
    }
}