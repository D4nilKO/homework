using System;

namespace homework
{
    internal static class ParenthesisExpression
    {
        public static void Main1(string[] args)
        {
            char openBracket = '(';
            char closeBracket = ')';

            int currentDepth = 0;
            int maxDepth = 0;

            bool isCorrectParenthesisExpression = true;

            Console.WriteLine("Введите скобочное выражение");
            string expression = Console.ReadLine();

            if (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine("Ошибка ввода");
            }

            foreach (char symbol in expression)
            {
                if (symbol == openBracket)
                {
                    currentDepth++;
                    maxDepth = Math.Max(maxDepth, currentDepth);
                }
                else if (symbol == closeBracket)
                {
                    currentDepth--;
                }

                if (currentDepth < 0)
                {
                    isCorrectParenthesisExpression = false;
                    break;
                }
            }

            if (currentDepth == 0 && isCorrectParenthesisExpression)
            {
                Console.WriteLine($"Глубина = {maxDepth}");
            }
            else
            {
                Console.WriteLine("Строка не является корректным скобочным выражением.");
            }
        }
    }
}