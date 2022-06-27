namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Please, enter a mathematical expression to evaluate.\n>> ");
            string input = "100. - (2.5 - .5 * ((-1)*(-40.) + 10))"; // Console.ReadLine();
            Calculation calc = new(input);

            Console.WriteLine($"\n{new Calculation(input).Evaluate()}");

        }
    }
}