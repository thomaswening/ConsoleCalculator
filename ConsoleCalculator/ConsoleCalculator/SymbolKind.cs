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
        public const string PARENTHESIS_LEFT = "parenthesis left";
        public const string PARENTHESIS_RIGHT = "parenthesis right";

        public const char SPACE = ' ';
        public const char DOT = '.';

        public const string INDETERMINATE = "indeterminate";

        static public List<string> Operators => new() { "+", "-", "*", "/" };
        static public List<string> LeftParentheses => new() { "(", "[", "{" };
        static public List<string> RightParentheses => new() { ")", "]", "}" };

        static public List<string> Parentheses
        {
            get
            {
                List<string> parentheses = new();
                parentheses.AddRange(LeftParentheses);
                parentheses.AddRange(RightParentheses);
                return parentheses;
            }
        }

        static public string FindSymbolKind(string pInput)
        {
            if (Char.IsDigit((char)pInput[0]) || pInput[0].Equals('.')) return NUMBER;
            else if (Operators.Contains(pInput)) return OPERATOR;
            else if (LeftParentheses.Contains(pInput)) return PARENTHESIS_LEFT;
            else if (RightParentheses.Contains(pInput)) return PARENTHESIS_RIGHT;
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
