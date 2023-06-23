
string input = "print('pasen la') ";

Lexer lexer = new Lexer(input);

Console.WriteLine("Tokens:");
Token token = lexer.GetNextToken();
while (token != null)
{
    Console.WriteLine($"Type: {token.Type}, Value: {token.Value}");
    token = lexer.GetNextToken();
}

Interpreter interpreter = new Interpreter();
interpreter.Interpret(input);









