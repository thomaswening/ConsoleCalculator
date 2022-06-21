using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal class Calculation
    {
        public Expression Expression { get; set; }

        public Calculation(string pInput) => Expression = new Expression(pInput);

        public Calculation(Expression pExpression) => Expression = pExpression;

        public double Evaluate()
        {
            List<Expression> subExpressions = Expression.GetSubExpressions();

            if (subExpressions.Count > 0)
            {
                subExpressions.Reverse();
                foreach (Expression subExpression in subExpressions)
                {
                    Expression.Symbols.RemoveRange(subExpression.StartIndex + 1, subExpression.Length + 1);
                    Expression.Symbols[subExpression.StartIndex].Content = new Calculation(subExpression).Evaluate().ToString();
                    Expression.Symbols[subExpression.StartIndex].Kind = SymbolKind.NUMBER;
                }
            }

            return EvaluatePrimitiveExpression(Expression);
        }
        private double EvaluatePrimitiveExpression(Expression pExpression)
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
    }
}
