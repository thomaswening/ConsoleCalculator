using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    /// <summary>
    /// Defines the different kinds of symbols there are.
    /// Defines constants containing the string literals for symbols other than numbers.
    /// Groups symbols together in collections.
    /// </summary>
    internal struct SymbolKind
    {

        public const string NUMBER = "number";
        public const string OPERATOR = "operator";
        public const string BRACKET_LEFT = "bracket left";
        public const string BRACKET_RIGHT = "bracket right";
        public const string INDETERMINATE = "indeterminate";

        public const char SPACE = ' ';
        public const char DOT = '.';

        public const char ADD = '+';
        public const char SUBTRACT = '-';
        public const char MULTIPLY = '*';
        public const char DIVIDE = '/';

        static public List<string> Operators => new() { "+", "-", "*", "/" };
        static public List<char> PrecedentOperators => new() { '*', '/' };
        static public List<string> LeftBrackets => new() { "(", "[", "{" };
        static public List<string> RightBrackets => new() { ")", "]", "}" };
        static public List<string> Brackets
        {
            get
            {
                List<string> parentheses = new();
                parentheses.AddRange(LeftBrackets);
                parentheses.AddRange(RightBrackets);
                return parentheses;
            }
        }

        static public string FindSymbolKind(string pInput)
        {
            if (Char.IsDigit((char)pInput[0]) || pInput[0].Equals('.')) return NUMBER;
            else if (Operators.Contains(pInput)) return OPERATOR;
            else if (LeftBrackets.Contains(pInput)) return BRACKET_LEFT;
            else if (RightBrackets.Contains(pInput)) return BRACKET_RIGHT;
            else return INDETERMINATE;
        }
    }
}
