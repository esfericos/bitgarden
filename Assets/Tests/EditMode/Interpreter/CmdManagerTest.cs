using System;
using System.Collections.Generic;
using Interpreter.Eval;
using Interpreter.Eval.Type;
using NUnit.Framework;
using String = Interpreter.Eval.Type.String;

namespace Tests.EditMode.Interpreter
{
    public class CmdManagerTest
    {
        [TestCaseSource(nameof(SuccessCasesData))]
        public void SuccessCases((object, string, Dictionary<string, Value>, Value) tup)
        {
            var (cmd, expectedCmdName, userArgs, expectedReturnValue) = tup;
            var c = new RegisteredCmd(cmd);
            Assert.AreEqual(expectedCmdName, c.Name);
            var returnValue = c.Exec(userArgs);
            Assert.AreEqual(expectedReturnValue, returnValue);
        }

        [TestCaseSource(nameof(ConstructorErrorCasesData))]
        public void ConstructorErrorCases((object, Dictionary<string, Value>, string) tup)
        {
            var (cmd, _, expectedErrorMessage) = tup;
            var ex = Assert.Throws<Exception>(() => _ = new RegisteredCmd(cmd));
            Assert.That(ex.Message, Does.StartWith(expectedErrorMessage));
        }
        
        [TestCaseSource(nameof(ExecErrorCasesData))]
        public void ExecErrorCases((object, Dictionary<string, Value>, string) tup)
        {
            var (cmd, userArgs, expectedErrorMessage) = tup;
            var c = new RegisteredCmd(cmd);
            var ex = Assert.Throws<EvalException>(() => _ = c.Exec(userArgs));
            Assert.That(ex.Message, Does.StartWith(expectedErrorMessage));
        }

        public static IEnumerable<(object, string, Dictionary<string, Value>, Value)> SuccessCasesData()
        {
            yield return (
                    new TestCmd.BasicCmd(),
                    "basicCmd",
                    new Dictionary<string, Value>()
                    {
                        { "x", Value.WrapString("hello") },
                        { "y", Value.WrapNumber(3.14) },
                        { "z", Value.WrapString("bye") },
                    },
                    Value.WrapString("hello 3.14 String(\"bye\")"));
            yield return (
                new TestCmd.CmdWithoutArguments(),
                "cmdWithoutArguments",
                new Dictionary<string, Value>(),
                new Nil());
        }

        public static IEnumerable<(object, Dictionary<string, Value>, string)> ConstructorErrorCasesData()
        {
            var emptyDict = new Dictionary<string, Value>();
            
            yield return (
                new TestCmd.WithoutExec(),
                emptyDict,
                "Missing 'Exec' method in command class 'WithoutExec'");
            yield return (
                new TestCmd.WithNonValueReturnType(),
                emptyDict,
                "Return type of 'Exec' from command class 'WithNonValueReturnType' must derive from 'Value'");
            yield return (
                new TestCmd.WithNonValueArgumentType(),
                emptyDict,
                "Argument 'invalidArg' of command class 'WithNonValueArgumentType' must derive from 'Value'");
        }
        
        public static IEnumerable<(object, Dictionary<string, Value>, string)> ExecErrorCasesData()
        {
            yield return (
                new TestCmd.BasicCmd(),
                new Dictionary<string, Value>()
                {
                    { "x", Value.WrapString("hello") },
                    // missing y
                    { "z", Value.WrapString("bye") },
                },
                "Missing argument 'y' in call to 'basicCmd'");
            yield return (
                new TestCmd.BasicCmd(),
                new Dictionary<string, Value>()
                {
                    { "x", Value.WrapString("hello") },
                    { "y", Value.WrapString("!!!wrong!!!") }, // <-- should be number
                    { "z", Value.WrapString("bye") },
                },
                "Type mismatch in argument 'y' in call to 'basicCmd': expected type 'Number', but got type 'String'");
            yield return (
                new TestCmd.BasicCmd(),
                new Dictionary<string, Value>()
                {
                    { "x", Value.WrapString("hello") },
                    { "y", Value.WrapNumber(3.14) },
                    { "z", Value.WrapString("bye") },
                    { "extraArg", Value.WrapString("extra") }, // <-- extra argument
                },
                "Argument 'extraArg' is not needed in call to 'basicCmd'");
        }
    }

    namespace TestCmd
    {
        internal class BasicCmd
        {
            public String Exec(String x, Number y, Value z)
            {
                return new String($"{x.Value} {y.Value} {z}");
            }
        }

        internal class CmdWithoutArguments
        {
            public Nil Exec()
            {
                return new Nil();
            }
        }

        internal class WithoutExec
        {
            public String Foo(String x)
            {
                throw new NotImplementedException();
            }
        }

        internal class WithNonValueReturnType
        {
            public void Exec(String _)
            {
                throw new NotImplementedException();
            }
        }

        internal class WithNonValueArgumentType
        {
            public String Exec(bool invalidArg)
            {
                _ = invalidArg;
                throw new NotImplementedException();
            }
        }
    }
}