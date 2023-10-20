using System.Collections.Generic;
using System.Linq;
using Interpreter.Eval;
using Interpreter.Parser;
using Interpreter.Parser.Ast;
using NUnit.Framework;

namespace Tests.EditMode.Interpreter
{
    public class ParserTest
    {
        [TestCaseSource(nameof(SuccessCasesData))]
        public void SuccessCases((string, string[]) tup)
        {
            var source = tup.Item1;
            var expected = tup.Item2;

            var program = new Parser(source).ParseProgram();
            var descriptors = new AstFlattener(program).Descriptors;
            Assert.That(descriptors, Is.EqualTo(expected));
        }
        
        public static IEnumerable<(string, string[])> SuccessCasesData()
        {
            // literals
            yield return ("3.14", new[] {
                "(begin) Program",
                    "Literal Number(3.14)",
                "(end) Program",
            });
            yield return ("\"hello\"", new[] {
                "(begin) Program",
                    "Literal String(\"hello\")",
                "(end) Program",
            });
            // function calls
            yield return ("fn()", new[] {
                "(begin) Program",
                    "(begin) Call 'fn'",
                    "(end) Call 'fn'",
                "(end) Program",
            });
            yield return ("fn(arg: 1)", new[] {
                "(begin) Program",
                    "(begin) Call 'fn'",
                        "(begin) Argument 'arg'",
                            "Literal Number(1)",
                        "(end) Argument 'arg'",
                    "(end) Call 'fn'",
                "(end) Program",
            });
            yield return ("fn(arg1: 1, arg2: 2, )", new[] {
                "(begin) Program",
                    "(begin) Call 'fn'",
                        "(begin) Argument 'arg1'",
                            "Literal Number(1)",
                        "(end) Argument 'arg1'",
                        "(begin) Argument 'arg2'",
                            "Literal Number(2)",
                        "(end) Argument 'arg2'",
                    "(end) Call 'fn'",
                "(end) Program",
            });
            yield return ("outer_fn(arg1: inner_fn(arg2: 2))", new[] {
                "(begin) Program",
                    "(begin) Call 'outer_fn'",
                        "(begin) Argument 'arg1'",
                            "(begin) Call 'inner_fn'",
                                "(begin) Argument 'arg2'",
                                    "Literal Number(2)",
                                "(end) Argument 'arg2'",
                            "(end) Call 'inner_fn'",
                        "(end) Argument 'arg1'",
                    "(end) Call 'outer_fn'",
                "(end) Program",
            });
        }
        
        [TestCaseSource(nameof(ErrorCasesData))]
        public void ErrorCases((string, string) tup)
        {
            var source = tup.Item1;
            var expectedErrorMessage = tup.Item2;

            var ex = Assert.Throws<ParseException>(() =>
            {
                _ = new Parser(source).ParseProgram();
            });
            Assert.AreEqual(expectedErrorMessage, ex.Message);
        }
        
        public static IEnumerable<(string, string)> ErrorCasesData()
        {
            // invalid function calls
            yield return ("ident", "Missing open paren to open function call after identifier 'ident'");
            yield return ("ident(", "Missing close paren on function call 'ident'");
            yield return ("ident(name", "Missing colon (:) after argument name 'name'");
            yield return ("fn(a: 1, b: 2, a: 3)", "Duplicate argument name 'a' in function call");
            yield return ("fn())", "Expected Eof, but got RPar");
            yield return ("fn(())", "Missing close paren on function call 'fn'");
            // etc
            yield return ("(", "Unexpected token LPar, expected literal (String or Number)");
        }
    }
}