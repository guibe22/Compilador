Console.WriteLine("ingresa el codigo");
string input = Console.ReadLine();

Lexer lexer = new Lexer(input);


Parser parser = new Parser(input);
parser.Parse();









