public class Interpreter
{
    public void Interpret(string input)
    {
        Parser parser = new Parser(input);
        parser.ParsePrintStatement();

    }
}
