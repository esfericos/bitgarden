using System.Collections.Generic;
using Interpreter.Eval.Type;

namespace Interpreter.Eval
{
    public class Evaluator
    {
        private Dictionary<string, CmdImpl> _cmdImpls = new Dictionary<string, CmdImpl>();
        private AstEvaluator _astEvaluator;

        public Evaluator()
        {
            _astEvaluator = new AstEvaluator(this);
        }

        public Evaluator AddCmd(object cmd)
        {
            var cmdImpl = new CmdImpl(cmd);
            _cmdImpls.Add(cmdImpl.Name, cmdImpl);
            return this;
        }

        /// <summary>
        /// Executes the given command with the provided arguments.
        ///
        /// You're probably should use `ExecProgram` instead—this is an internal function.
        /// </summary>
        public Value ExecCmd(string cmdName, IReadOnlyDictionary<string, Value> args)
        {
            if (!_cmdImpls.TryGetValue(cmdName, out var cmdImpl))
                throw new EvalException($"Can't call undefined function '{cmdName}'");
            return cmdImpl.Exec(args);
        }

        /// <summary>
        /// Executes the given program.
        /// </summary>
        public void ExecProgram(string source)
        {
            var program = new Parser.Parser(source).ParseProgram();
            _ = _astEvaluator.VisitProgram(program);
        }
    }
}