using System;

namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Please, enter a mathematical expression to evaluate.\n>> ");
            string input = Console.ReadLine();
            Expression expression = new(input);

            try
            {
                Console.Write(expression.Evaluate());
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}