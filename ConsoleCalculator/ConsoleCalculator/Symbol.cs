using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal class Symbol
    {
        private string kind;
        public string Content { get; set; }
        public int Length => Content.Length;
        public string Kind
        {
            get => kind;
            private set => kind = SymbolKind.FindSymbolKind(Content);
        }
        public Symbol(string pInput)
        {
            Content = pInput;
            Kind = Content;
        }
    }
}
