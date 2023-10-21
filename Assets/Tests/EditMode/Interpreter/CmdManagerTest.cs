using System.Collections.Generic;
using Interpreter.Eval;
using NUnit.Framework;

namespace Tests.EditMode.Interpreter
{
    public class CmdManagerTest
    {
        [Test]
        public void TestBasicExec()
        {
            var c = new RegisteredCmd(new TestCmd());
            var retValue = c.Exec(new Dictionary<string, Value>()
            {
                { "x", Value.WrapString("hello") },
                { "y", Value.WrapNumber(3.14) },
                { "z", Value.WrapString("bye") },
            });
            Assert.AreEqual("hello 3.14 String(\"bye\")", retValue.AsString);
        }
    }

    internal class TestCmd
    {
        public String Exec(String x, Number y, Value z)
        {
            return new String($"{x.Value} {y.Value} {z}");
        }
    }
}