public class Parser
{
    private readonly Lexer lexer;
    private Token currentToken;
    private Dictionary<string, object> variables;

    public Parser(string input)
    {
        lexer = new Lexer(input);
        currentToken = lexer.GetNextToken();
        variables = new Dictionary<string, object>();
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

    public void Parse()
    {
        try
        {
            while (currentToken.Type != TokenType.EOF)
            {
                // Avanzar al siguiente token


                switch (currentToken.Type)
                {
                    case TokenType.Print:
                        
                        ParsePrintStatement();
                        break;
                    case TokenType.Variable:

                        ParseVariableStatement();
                        break;
                    default:
                        throw new ArgumentException($"Unexpected token: {currentToken.Type}");
                }
               
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
    }


    public void ParsePrintStatement()
    {
        if (currentToken.Type == TokenType.Print)
        {
            Eat(TokenType.Print);
            Eat(TokenType.LParen);

            if (currentToken.Type == TokenType.String)
            {
                Console.WriteLine(currentToken.Value.Trim('\''));
                Eat(TokenType.String);
            }
            else if (currentToken.Type == TokenType.Variable)
            {
                Console.WriteLine(variables[currentToken.Value]);
                 Eat(TokenType.Variable);
            }
            else
            {
                throw new ArgumentException("Expected a string or variable inside the print statement.");
            }

            
        }
        else
        {
            throw new ArgumentException("Expected a 'print' statement.");
        }

         currentToken = lexer.GetNextToken();
    }

    public void ParseVariableStatement()
    {

        if (currentToken.Type == TokenType.Variable)
        {
            string nombre = currentToken.Value;
            // Avanzar al siguiente token
            Eat(TokenType.Variable);

            // Verificar si el siguiente token es el operador de asignación '='
            if (currentToken.Type == TokenType.Assign)
            {
                // Avanzar al siguiente token
                Eat(TokenType.Assign);

                // Obtener el valor
                dynamic value;

                // Verificar el tipo del siguiente token
                switch (currentToken.Type)
                {
                    case TokenType.Integer:
                        value = int.Parse(currentToken.Value);
                        break;
                    case TokenType.String:
                        value = currentToken.Value.Trim('\'');
                        break;
                    case TokenType.Bool:
                        value = bool.Parse(currentToken.Value);
                        break;
                    default:
                        return;
                }


                if (variables.ContainsKey(nombre))
                {
                    variables[nombre] = value;
                }
                else
                {
                    variables.Add(nombre, value);
                }

            }
            else
            {
                throw new ArgumentException("Expected an assignment operator '=' in the variable assignment statement.");
            }
        }
        else
        {
            throw new ArgumentException("Expected a 'var' statement.");
        }

         currentToken = lexer.GetNextToken();
    }



}
