using System;
using Lib = Interpreter.Lib;
using Interpreter.Eval;
using Interpreter.Eval.Type;
using NUnit.Framework;

namespace Tests.EditMode.Interpreter
{
    public class EvaluatorTest
    {
        [Test]
        public void TestEvaluator()
        {
            var state = new State();
            var ev = new Evaluator()
                .AddCmd(new GetNum() { State = state })
                .AddCmd(new SetNum() { State = state })
                .AddCmd(new Lib.Math.Add());
            
            Assert.AreEqual(state.Num, 0);
            ev.ExecProgram("setNum(new: 2)");
            Assert.AreEqual(state.Num, 2);
            ev.ExecProgram("setNum(new: add(a: getNum(), b: getNum()))");
            Assert.AreEqual(state.Num, 4);

            var ex = Assert.Throws<EvalException>(() => ev.ExecProgram("undefined_func()"));
            Assert.AreEqual($"Can't call undefined function 'undefined_func'", ex.Message);
            
        }
    }

    internal class State
    {
        public double Num = 0;
    }

    internal class GetNum
    {
        public State State = new();

        public Number Exec()
        {
            return new Number(State.Num);
        }
    }

    internal class SetNum
    {
        public State State = new();
        
        public Nil Exec(Number @new)
        {
            State.Num = @new.AsNumber();
            return new Nil();
        }
    }
}