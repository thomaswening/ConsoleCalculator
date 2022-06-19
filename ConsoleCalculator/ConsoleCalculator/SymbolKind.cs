using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal struct SymbolKind
    {
        public const string NUMBER = "number";
        public const string OPERATOR = "operator";
        public const string BRACKET_LEFT = "bracket left";
        public const string BRACKET_RIGHT = "bracket right";

        public const char SPACE = ' ';
        public const char DOT = '.';

        public const char ADD = '+';
        public const char SUBTRACT = '-';
        public const char MULTIPLY = '*';
        public const char DIVIDE = '/';

        public const string INDETERMINATE = "indeterminate";

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

        static public string FindSymbolKind(char pInput) => FindSymbolKind(Convert.ToString(pInput));

        static public bool IsOfSameKind(string pInput1, string pInput2)
        {
            return FindSymbolKind(pInput1).Equals(FindSymbolKind(pInput2));
        }

        static public bool IsOfSameKind(char pInput1, string pInput2)
        {
            return FindSymbolKind(pInput1).Equals(FindSymbolKind(pInput2));
        }

        static public bool IsOfSameKind(string pInput1, char pInput2)
        {
            return FindSymbolKind(pInput1).Equals(FindSymbolKind(pInput2));
        }

        static public bool IsOfSameKind(char pInput1, char pInput2)
        {
            return FindSymbolKind(pInput1).Equals(FindSymbolKind(pInput2));
        }
    }
}
