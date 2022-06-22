using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    /// <summary>
    /// Defines a mathematical expression that may be evaluated.
    /// </summary>
    internal class Expression
    {
        public List<Symbol> Symbols = new();
        public int Length => Symbols.Count;
        public int StartIndex { get; set; } = 0;

        public Expression() { }
        public Expression(string pInput)
        {
            Symbols = ConvertToSymbolList(pInput);    
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (Symbol symbol in Symbols)
            {
                sb.Append(symbol.Content);
            }

            return sb.ToString();
        }

        public void Clear() => Symbols.Clear();
        public void AddSymbol(Symbol pSymbol) => Symbols.Add(pSymbol);
        public void AddSymbols(List<Symbol> pSymbols) => Symbols.AddRange(pSymbols);
        
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
                        || Char.IsDigit(nextElement)
                        || SymbolKind.Brackets.Contains(Convert.ToString(nextElement)))
                    {
                        stringBuilder.Append(nextElement);
                        symbols.Add(new Symbol(stringBuilder.ToString()));
                        stringBuilder.Clear();
                    }
                }
            }

            return symbols;
        }

        /// <summary>
        /// Checks if two chars can be part of a number in that order.
        /// </summary>
        /// <param name="pElement1">Predecessor char to be checked.</param>
        /// <param name="pElement2">Successor char to be checked.</param>
        /// <returns>True if the chars can be part of a number in that order, false otherwise.</returns>
        private bool CanMakeUpNumber(char pElement1, char pElement2)
        {
            return (Char.IsDigit(pElement1) && Char.IsDigit(pElement2))
                || (Char.IsDigit(pElement1) && pElement2.Equals('.'))
                || (pElement1.Equals(SymbolKind.DOT) && Char.IsDigit(pElement2));
        }
        
        private void RemoveOuterBrackets()
        {
            Symbols.RemoveAt(0);
            Symbols.RemoveAt(Length - 1);
        }
        private Expression Copy()
        {
            Expression copy = new();
            copy.AddSymbols(Symbols);
            copy.StartIndex = StartIndex;

            return copy;
        }
        
        /// <summary>
        /// Iterates through the expression's symbols and checks how many 
        /// brackets are opened and then closed in order to determine
        /// how many subexpressions are enclosed in brackets within the expression.
        /// </summary>
        /// <returns>A list of all subexpressions within the expression.</returns>
        public List<Expression> GetSubExpressions()
        {
            int openBrackets = 0;
            int i = 0;

            List<Expression> subExpressions = new();
            Expression subExpression = new();

            foreach (Symbol symbol in Symbols)
            {
                if (symbol.Kind == SymbolKind.BRACKET_LEFT)
                {
                    if (openBrackets == 0) subExpression.StartIndex = i;
                    openBrackets++;
                }

                if (openBrackets > 0) subExpression.AddSymbol(symbol);

                if (symbol.Kind == SymbolKind.BRACKET_RIGHT) openBrackets--;

                if (openBrackets == 0 && subExpression.Length > 0)
                {
                    subExpression.RemoveOuterBrackets();
                    subExpressions.Add(subExpression.Copy());
                    subExpression.Clear();
                }

                i++;
            } 
            
            return subExpressions;
        }

        /// <summary>
        /// Evaluates the expression's subexpressions first, then replaces them with their
        /// value and then evaluates the remaining primitive expression.
        /// </summary>
        /// <returns>The expression's value.</returns>
        public double Evaluate()
        {
            List<Expression> subExpressions = GetSubExpressions();

            if (subExpressions.Count > 0)
            {
                // The subexpressions must be replaced by their values from right to left
                // in order to maintain the starting indices of the remaining subexpressions
                subExpressions.Reverse();
                foreach (Expression subExpression in subExpressions)
                {
                    Symbols.RemoveRange(subExpression.StartIndex + 1, subExpression.Length + 1);
                    Symbols[subExpression.StartIndex].Content = subExpression.Evaluate().ToString();
                    Symbols[subExpression.StartIndex].Kind = SymbolKind.NUMBER;
                }
            }

            return EvaluatePrimitiveExpression(this);
        }

        /// <summary>
        /// Evaluates an expression without subexpressions iteratively. First determines the 
        /// indices of the leftmost precedent operator and then evaluates its binary expression.
        /// When there are none left, it evaluates the remaining expression from left to right.
        /// </summary>
        /// <param name="pExpression">Primitive expression to be evaluated.</param>
        /// <returns>The primitive expression's value.</returns>
        public double EvaluatePrimitiveExpression(Expression pExpression)
        {
            int iFirstPrecedentOperator;
            Symbol num1;
            Symbol num2;
            Symbol operatorSymbol;

            while (pExpression.Length > 1)
            {
                iFirstPrecedentOperator = GetFirstPrecedentOperator(pExpression.Symbols);
                num1 = pExpression.Symbols[iFirstPrecedentOperator - 1];
                num2 = pExpression.Symbols[iFirstPrecedentOperator + 1];
                operatorSymbol = pExpression.Symbols[iFirstPrecedentOperator];

                pExpression.Symbols[iFirstPrecedentOperator - 1].Content = EvaluateBinaryExpression(num1, num2, operatorSymbol).ToString();
                pExpression.Symbols.RemoveRange(iFirstPrecedentOperator, 2);
            }

            return Convert.ToDouble(pExpression.Symbols[0].Content);
        }

        private double EvaluateBinaryExpression(Symbol pNum1, Symbol pNum2, Symbol pOperator)
        {
            double num1 = Convert.ToDouble(pNum1.Content);
            double num2 = Convert.ToDouble(pNum2.Content);

            switch (Convert.ToChar(pOperator.Content))
            {
                case SymbolKind.ADD:
                    return num1 + num2;

                case SymbolKind.SUBTRACT:
                    return num1 - num2;

                case SymbolKind.MULTIPLY:
                    return num1 * num2;

                case SymbolKind.DIVIDE:
                    if (num2 == 0) throw new DivideByZeroException();
                    else return num1 / num2;

                default: throw new NotImplementedException();
            }
        }

        private int GetFirstPrecedentOperator(List<Symbol> pSymbols)
        {
            int i = 0;
            foreach (Symbol symbol in pSymbols)
            {
                if (!symbol.Kind.Equals(SymbolKind.NUMBER))
                {
                    if (SymbolKind.PrecedentOperators.Contains(Convert.ToChar(symbol.Content))) return i;
                }

                i++;
            }

            return 1;
        }

        /// <summary>
        /// Checks for unpaired brackets and prints a message if there are any.
        /// </summary>
        /// <returns>True if there are no unpaired brackets, false otherwise.</returns>
        private bool CheckBrackets()
        {
            int openBrackets = 0;

            foreach (Symbol symbol in Symbols)
            {
                if (symbol.Kind.Equals(SymbolKind.BRACKET_LEFT)) openBrackets++;
                if (symbol.Kind.Equals(SymbolKind.BRACKET_RIGHT)) openBrackets--;
            }

            switch (openBrackets)
            {
                case 0:
                    return true;

                default:
                    Console.WriteLine("\nThere are unpaired brackets in your expression. Please revise.");
                    return false;
            }
        }
    }
}
