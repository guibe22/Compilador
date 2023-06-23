using System.Text;

public class Lexer
{
    private readonly string[] separators = { "+", "-", "*", "/", "(", ")", "=", "==", "!=", "<", "<=", ">", ">=" };
    private readonly string input;
    private int position;
    private string[] tokens;

    public Lexer(string input)
    {
        this.input = input;
        position = 0;
        tokens = TokenizeInput();
    }
    

    private string[] TokenizeInput()
    {
        List<string> tokenList = new List<string>();
        StringBuilder currentToken = new StringBuilder();
        bool withinQuotes = false;

        foreach (char c in input)
        {
            if (c == '\'')
            {
                withinQuotes = !withinQuotes;
                currentToken.Append(c);
            }
            else if (withinQuotes)
            {
                currentToken.Append(c);
            }
            else if (char.IsWhiteSpace(c))
            {
                if (currentToken.Length > 0)
                {
                    tokenList.Add(currentToken.ToString());
                    currentToken.Clear();
                }
            }
            else if (separators.Contains(c.ToString()))
            {
                if (currentToken.Length > 0)
                {
                    tokenList.Add(currentToken.ToString());
                    currentToken.Clear();
                }
                tokenList.Add(c.ToString());
            }
            else
            {
                currentToken.Append(c);
            }
        }

        if (currentToken.Length > 0)
        {
            tokenList.Add(currentToken.ToString());
        }

        return tokenList.ToArray();
    }

    public Token GetNextToken()
    {
        if (position >= tokens.Length)
        {
            return null;
        }

        string currentToken = tokens[position];
        position++;

        if (char.IsDigit(currentToken[0]))
        {
            return ReadInteger(currentToken);
        }
        else if (currentToken.Length >= 2 && currentToken[0] == '\'' && currentToken[currentToken.Length - 1] == '\'')
        {
            // Es un string entre comillas simples
            return new Token(TokenType.String, currentToken.Trim('\''));
        }
        else if (currentToken == "+")
        {
            return new Token(TokenType.Plus, currentToken);
        }
        else if (currentToken == "-")
        {
            return new Token(TokenType.Minus, currentToken);
        }
        else if (currentToken == "*")
        {
            return new Token(TokenType.Multiply, currentToken);
        }
        else if (currentToken == "/")
        {
            return new Token(TokenType.Divide, currentToken);
        }
        else if (currentToken == "(")
        {
            return new Token(TokenType.LParen, currentToken);
        }
        else if (currentToken == ")")
        {
            return new Token(TokenType.RParen, currentToken);
        }
        else if (currentToken == "=")
        {
            if (position < tokens.Length && tokens[position] == "=")
            {
                position++;
                return new Token(TokenType.Equal, "==");
            }
            else
            {
                return new Token(TokenType.Assign, currentToken);
            }
        }
        else if (currentToken == "!=")
        {
            return new Token(TokenType.NotEqual, currentToken);
        }
        else if (currentToken == "<")
        {
            if (position < tokens.Length && tokens[position] == "=")
            {
                position++;
                return new Token(TokenType.LessThanOrEqual, "<=");
            }
            else
            {
                return new Token(TokenType.LessThan, currentToken);
            }
        }
        else if (currentToken == ">")
        {
            if (position < tokens.Length && tokens[position] == "=")
            {
                position++;
                return new Token(TokenType.GreaterThanOrEqual, ">=");
            }
            else
            {
                return new Token(TokenType.GreaterThan, currentToken);
            }
        }
        else if (currentToken == "print")
        {
            return new Token(TokenType.Print, currentToken);
        }
        else if (currentToken == "true")
        {
            return new Token(TokenType.Bool, currentToken);
        }
        else if (currentToken == "false")
        {
            return new Token(TokenType.Bool, currentToken);
        }
        else if (!string.IsNullOrEmpty(currentToken))
        {
            return new Token(TokenType.Variable, currentToken);
        }
        else
        {
            return new Token(TokenType.Identifier, currentToken);
        }

    }

    private Token ReadInteger(string currentToken)
    {
        if (int.TryParse(currentToken, out int value))
        {
            return new Token(TokenType.Integer, value.ToString());
        }
        else
        {
            throw new ArgumentException($"Invalid integer token: {currentToken}");
        }
    }
}
