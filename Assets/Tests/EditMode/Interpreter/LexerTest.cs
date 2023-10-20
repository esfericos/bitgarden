using System.Collections.Generic;
using System.Linq;
using Interpreter;
using Interpreter.Parser;
using NUnit.Framework;

namespace Tests.EditMode.Interpreter
{
    public class LexerTest
    {
        [TestCaseSource(nameof(SuccessCasesData))]
        public void SuccessCases((string, Token[]) tup)
        {
            var source = tup.Item1;
            var expected = tup.Item2;
            
            var tokens = new Lexer(source).Tokens().ToList();
            Assert.That(tokens, Is.EqualTo(expected));
        }
        
        public static IEnumerable<(string, Token[])> SuccessCasesData()
        {
            // identifiers
            yield return ("f", new[] {
                new Token(TokenKind.Identifier, 0, 1),
                Token.Eof(1),
            });
            yield return ("fo", new[] {
                new Token(TokenKind.Identifier, 0, 2),
                Token.Eof(2),
            });
            yield return ("foo", new[] {
                new Token(TokenKind.Identifier, 0, 3),
                Token.Eof(3),
            });
            // strings
            yield return ("\"o\"", new[] {
                new Token(TokenKind.String, 0, 3),
                Token.Eof(3),
            });
            yield return ("\"oi\"", new[] {
                new Token(TokenKind.String, 0, 4),
                Token.Eof(4),
            });
            // whitespace
            yield return (" a  b\nc\t  ", new[] {
                new Token(TokenKind.Identifier, 1, 1),
                new Token(TokenKind.Identifier, 4, 1),
                new Token(TokenKind.Identifier, 6, 1),
                Token.Eof(10),
            });
            // numbers
            yield return ("1", new[] {
                new Token(TokenKind.Number, 0, 1),
                Token.Eof(1),
            });
            yield return ("123", new[] {
                new Token(TokenKind.Number, 0, 3),
                Token.Eof(3),
            });
            yield return ("123.1", new[] {
                new Token(TokenKind.Number, 0, 5),
                Token.Eof(5),
            });
            yield return ("123.123", new[] {
                new Token(TokenKind.Number, 0, 7),
                Token.Eof(7),
            });
            // etc
            yield return ("():,", new[] {
                new Token(TokenKind.LPar, 0, 1),
                new Token(TokenKind.RPar, 1, 1),
                new Token(TokenKind.Colon, 2, 1),
                new Token(TokenKind.Comma, 3, 1),
                new Token(TokenKind.Eof, 4, 0),
            });
        }
        
        [TestCaseSource(nameof(ErrorCasesData))]
        public void ErrorCases((string, string) tup)
        {
            var source = tup.Item1;
            var expectedMessage = tup.Item2;
            
            var ex = Assert.Throws<ParseException>(() =>
            {
                _ = new Lexer(source).Tokens().ToList();
            });
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }
        
        public static IEnumerable<(string, string)> ErrorCasesData()
        {
            // strings
            yield return ("\"oi", "Unexpected unclosed string");
            // numbers
            yield return (".1", "Invalid character '.'");
            yield return (".123", "Invalid character '.'");
            yield return ("1.", "Invalid character '.'");
            yield return ("123.", "Invalid character '.'");
        }
    }
}