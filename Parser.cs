public class Parser
{
    private readonly Lexer lexer;
    private Token currentToken;
    

    public Parser(string input)
    {
        lexer = new Lexer(input);
        currentToken = lexer.GetNextToken();
    }

    private void Eat(TokenType expectedTokenType)
    {
        if (currentToken.Type == expectedTokenType)
        {
            currentToken = lexer.GetNextToken();
        }
        else
        {
            throw new ArgumentException($"Unexpected token. Expected {expectedTokenType}, but got {currentToken.Type}.");
        }
    }

    public void ParsePrintStatement()
    {
        // Verificar si el token actual es 'print'
        if (currentToken.Type == TokenType.Print)
        {
            // Avanzar al siguiente token
            Eat(TokenType.Print);

            // Verificar si el siguiente token es un paréntesis izquierdo '('
            Eat(TokenType.LParen);

            // Verificar si el siguiente token es un string
            if (currentToken.Type == TokenType.String)
            {
                // Imprimir el valor del string sin las comillas
                Console.WriteLine(currentToken.Value.Trim('\''));
                
                // Avanzar al siguiente token
                Eat(TokenType.String);
            }
            else
            {
                throw new ArgumentException("Expected a string inside the print statement.");
            }

            // Verificar si el siguiente token es un paréntesis derecho ')'
            Eat(TokenType.RParen);
        }
        else
        {
            throw new ArgumentException("Expected a 'print' statement.");
        }
    }
}
