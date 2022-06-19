using System;

namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main()
        {
            string input = "1000.";
            Expression expression = new(input);

            foreach (Symbol symbol in expression.Symbols)
            {
                Console.WriteLine($"\"{symbol.Content}\"\t{symbol.Kind}");
            }
        }
    }
}