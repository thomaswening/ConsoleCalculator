using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    /// <summary>
    /// Represents a single symbol in a mathematical expression.
    /// </summary>
    internal class Symbol
    {
        public string Content { get; set; }
        public string Kind { get; set; }

        public int Length => Content.Length;
        public Symbol(string pInput)
        {
            Content = pInput;
            Kind = SymbolKind.FindSymbolKind(Content);
        }
    }
}
