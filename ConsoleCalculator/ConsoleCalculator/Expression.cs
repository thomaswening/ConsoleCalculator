using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal class Expression
    {
        public List<Symbol> Symbols = new();
        public int Length => Symbols.Count;

        public Expression(string pInput)
        {
            Symbols = ConvertToSymbolList(pInput);
        }

        public List<Symbol> ConvertToSymbolList(string pInput)
        {
            StringBuilder stringBuilder = new();
            List<Symbol> symbols = new();

            char currentElement;
            char nextElement;
            for (int i = 1; i < pInput.Length; i++)
            {
                currentElement = pInput[i - 1];
                nextElement = pInput[i];

                if (!currentElement.Equals(SymbolKind.SPACE))
                {
                    stringBuilder.Append(currentElement);

                    if (!CanMakeUpNumber(currentElement, nextElement))
                    {
                        symbols.Add(new Symbol(stringBuilder.ToString()));
                        stringBuilder.Clear();
                    }
                }

                if (i == pInput.Length - 1)
                {
                    if ((CanMakeUpNumber(currentElement, nextElement) && !nextElement.Equals(SymbolKind.DOT))
                        || Char.IsDigit(nextElement))
                    {
                        stringBuilder.Append(nextElement);
                        symbols.Add(new Symbol(stringBuilder.ToString()));
                        stringBuilder.Clear();
                    }
                }
            }

            return symbols;
        }

        private bool CanMakeUpNumber(char pElement1, char pElement2)
        {
            return (Char.IsDigit(pElement1) && Char.IsDigit(pElement2))
                || (Char.IsDigit(pElement1) && pElement2.Equals('.'))
                || (pElement1.Equals(SymbolKind.DOT) && Char.IsDigit(pElement2));
        }
    }
}
