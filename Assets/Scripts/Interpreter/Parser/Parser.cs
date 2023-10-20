using System.Collections.Generic;
using System.Linq;
using Interpreter.Eval;

namespace Interpreter.Parser
{
    public class Parser
    {
        private readonly string _source;
        private readonly IReadOnlyList<Token> _tokens;
        private int _cursor = 0;

        public Parser(string source)
        {
            _source = source;
            _tokens = new Lexer(source).Tokens().ToList();
        }

        // <program> ::= <expr>
        public Ast.Program ParseProgram()
        {
            var program = new Ast.Program(ParseExpr());
            ExpectConsume(TokenKind.Eof);
            return program;
        }

        // <expr> ::= <call> | <lit>
        private Ast.Expr ParseExpr()
        {
            return Peek().Kind == TokenKind.Identifier
                ? ParseCall()
                : ParseLiteral();
        }

        // <call> ::= <ident> "(" <call_args>? ")"
        private Ast.Call ParseCall()
        {
            var nameToken = ExpectConsume(TokenKind.Identifier);
            var name = GetTokenSource(nameToken);
            if (!MatchConsume(TokenKind.LPar, out _))
                throw new ParseException($"Missing open paren to open function call after identifier '{name}'");
            var args = Peek().Kind == TokenKind.Identifier
                ? ParseCallArgs()
                : new Dictionary<string, Ast.Expr>();
            if (!MatchConsume(TokenKind.RPar, out _))
                throw new ParseException($"Missing close paren on function call '{name}'");
            return new Ast.Call(name, args);
        }

        // <call_args> ::= <call_arg> ( "," <call_arg> )* ","?
        private IReadOnlyDictionary<string, Ast.Expr> ParseCallArgs()
        {
            var dict = new Dictionary<string, Ast.Expr>();
            do
            {
                // If it is trailing comma, there won't be an identifier next, so we stop.
                if (Peek().Kind != TokenKind.Identifier) break;
                var (ident, expr) = ParseCallArg();
                if (dict.ContainsKey(ident))
                    throw new ParseException($"Duplicate argument name '{ident}' in function call");
                dict.Add(ident, expr);
            } while (MatchConsume(TokenKind.Comma, out _));
            return dict;
        }

        // <call_arg> ::= <ident> ":" <expr>
        private (string, Ast.Expr) ParseCallArg()
        {
            var ident = GetTokenSource(ExpectConsume(TokenKind.Identifier));
            if (!MatchConsume(TokenKind.Colon, out _))
                throw new ParseException($"Missing colon (:) after argument name '{ident}'");
            var expr = ParseExpr();
            return (ident, expr);
        }

        // <lit> ::= <number> | <string>
        private Ast.Literal ParseLiteral()
        {
            var curr = Peek();
            switch (curr.Kind)
            {
                case TokenKind.String:
                    var str = GetTokenSource(curr)[1..^1]; // Exclude quotes
                    Advance();
                    return new Ast.Literal(Value.WrapString(str));
                case TokenKind.Number:
                    if (!double.TryParse(GetTokenSource(curr), out var num))
                        throw new ParseException("Invalid number literal");
                    Advance();
                    return new Ast.Literal(Value.WrapNumber(num));
                case TokenKind.Eof:
                case TokenKind.Identifier:
                case TokenKind.LPar:
                case TokenKind.RPar:
                case TokenKind.Colon:
                case TokenKind.Comma:
                default:
                    throw new ParseException($"Unexpected token {curr.Kind}, expected literal (String or Number)");
            }
        }

        /// <summary>
        /// Returns the current token and advances the cursor.
        /// </summary>
        private Token Advance()
        {
            return _tokens[_cursor++];
        }

        /// <summary>
        /// Attempts to consume a token of the given kind, advancing the cursor.
        /// If the current token doesn't match, raises a parsing error.
        /// </summary>
        private Token ExpectConsume(TokenKind kind)
        {
            if (!MatchConsume(kind, out var token))
                throw new ParseException($"Expected {kind}, but got {Peek().Kind}");
            return token;
        }

        /// <summary>
        /// Attempts to advance if the given token kind matches the current one.
        /// Returns a boolean indicating if any advancement was performed.
        /// </summary>
        private bool MatchConsume(TokenKind kind, out Token token)
        {
            var curr = Peek();
            var ok = curr.Kind == kind;
            token = Token.Eof(0);
            if (!ok) return false;
            token = Advance();
            return true;

        }

        /// <summary>
        /// Returns the current token without changing the cursor.
        /// </summary>
        private Token Peek()
        {
            return _tokens[_cursor];
        }

        private string GetTokenSource(Token token)
        {
            return _source.Substring(token.Start, token.Len);
        }
    }
}