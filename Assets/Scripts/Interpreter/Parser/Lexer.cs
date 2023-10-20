using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter.Parser
{
    public class Lexer
    {
        private string Source { get; }
        private ushort _start = 0;
        private ushort _cursor = 0;

        public Lexer(string source)
        {
            const int max = ushort.MaxValue;
            if (max <= source.Length)
            {
                throw new ArgumentException($"Source can't have length greater than {max}");
            }
            Source = source;
        }

        public IEnumerable<Token> Tokens()
        {
            while (_cursor < Source.Length)
            {
                var c = Advance();
                switch (c)
                {
                    case ' ':
                    case '\n':
                    case '\f':
                    case '\r':
                    case '\t':
                        StartNextToken();
                        continue;
                    case '(':
                        yield return T(TokenKind.LPar);
                        break;
                    case ')':
                        yield return T(TokenKind.RPar);
                        break;
                    case ':':
                        yield return T(TokenKind.Colon);
                        break;
                    case ',':
                        yield return T(TokenKind.Comma);
                        break;
                    case '"':
                    {
                        // Consume while within the string
                        while (Peek(0) != '"' && !Exhausted()) Advance();
                        // Consume closing quote
                        if (Exhausted())
                            throw new ParseException($"Unexpected unclosed string");
                        Advance();
                        yield return T(TokenKind.String);
                        break;
                    }
                    default:
                    {
                        // Check for identifier
                        if (Utils.IsIdentStart(c))
                        {
                            while (Utils.IsIdentBody(Peek(0))) Advance();
                            yield return T(TokenKind.Identifier);
                            break;
                        }

                        // Check for number literal
                        // ReSharper disable once InvertIf
                        if (Utils.IsAsciiDigit(c))
                        {
                            // Consume digits before an eventual decimal separator (.)
                            while (Utils.IsAsciiDigit(Peek(0))) Advance();
                            // Checking for a decimal separator
                            if (Peek(0) == '.' && Utils.IsAsciiDigit(Peek(1)))
                            {
                                // Consume decimal separator
                                Advance();
                                // Consume digits after the decimal separator
                                while (Utils.IsAsciiDigit(Peek(0))) Advance();
                            }

                            yield return T(TokenKind.Number);
                            break;
                        }

                        throw new ParseException($"Invalid character '{c}'");
                    }
                }
            }
            yield return new Token(TokenKind.Eof, _cursor, 0);
        }

        private bool Exhausted()
        {
            return !(_cursor < Source.Length);
        }
        
        /// <summary>
        /// Returns the current character and advances the cursor.
        /// </summary>
        /// <returns></returns>
        private char Advance()
        {
            _cursor++;
            return Peek(-1);
        }

        /// <summary>
        /// Returns the next character at the given offset or `\0`
        /// if out of bounds.
        /// </summary>
        private char Peek(short offset)
        {
            return _cursor + offset < Source.Length
                ? Source[_cursor + offset]
                : '\0';
        }

        /// <summary>
        /// Produces a new token for the given kind. Resets the token accumulator.
        /// </summary>
        private Token T(TokenKind kind)
        {
            // Cursor is always greater than start, so casting is safe.
            var len = (ushort)(_cursor - _start);
            return new Token(kind, StartNextToken(), len);
        }

        private ushort StartNextToken()
        {
            var prevStart = _start;
            _start = _cursor;
            return prevStart;
        }
    }

    internal static class Utils
    {
        public static bool IsAsciiDigit(char c)
        {
            return c is >= '0' and <= '9';
        }

        public static bool IsIdentStart(char c)
        {
            var l = char.ToLower(c);
            return l is >= 'a' and <= 'z' or '_';
        }

        public static bool IsIdentBody(char c)
        {
            return IsIdentStart(c) || IsAsciiDigit(c);
        }
    }
}