using System;

namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main()
        {
            string input = "(100 - (2 * 50) + 10)/5";
            Expression expression = new(input);

            Console.Write(expression.Evaluate());
        }
    }
}