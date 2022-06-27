namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main()
        {
            bool repeat = false;

            Console.Write("Please, enter a mathematical expression to evaluate.\n>> ");
            do
            {
                try
                {
                    string? rawExpression = Console.ReadLine();
                    Console.WriteLine("\n");
                    if (string.IsNullOrEmpty(rawExpression)) throw new ArgumentException("Please enter something.");

                    Console.WriteLine($"\n>> {new Calculation(rawExpression).Evaluate()}");

                    bool isYOrN;
                    Console.Write("Do you want to evaluate another expression? (y/n)\n>> ");
                    do
                    {
                        try
                        {
                            string? input = Console.ReadLine();
                            Console.WriteLine("\n");

                            isYOrN = true;
                            if (string.IsNullOrEmpty(input)) throw new ArgumentException("Please enter something.");
                            else if (input == "y") repeat = true;
                            else if (input == "n") repeat = false;
                            else throw new ArgumentException("Please enter either 'y' or 'n'.");
                        }
                        catch (Exception e)
                        {
                            isYOrN = false;
                            Console.WriteLine(e.Message);
                        }

                    } while (!isYOrN);

                }
                catch (Exception e)
                {
                    repeat = true;
                    Console.WriteLine(e.Message);
                }

            } while (repeat);

        }
    }
}