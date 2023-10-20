namespace Interpreter.Parser
{
    public enum TokenKind
    {
        // An identifier
        Identifier,
        // A string literal
        String,
        // A number literal
        Number,
        // (
        LPar,
        // )
        RPar,
        // :
        Colon,
        // ,
        Comma,
        // End of file
        Eof,
    }
    
    public struct Token
    {
        public readonly ushort Start;
        public readonly ushort Len;
        public readonly TokenKind Kind;

        public Token(TokenKind kind, ushort start, ushort len)
        {
            Kind = kind;
            Start = start;
            Len = len;
        }

        public static Token Eof(ushort start)
        {
            return new Token(TokenKind.Eof, start, 0);
        }

        public override string ToString()
        {
            return $"Token({Kind} @ {Start}..{Start + Len})";
        }
    }
}