using System;

namespace homework
{
    internal static class NumberSum
    {
        public static void Main1(string[] args)
        {
            int firstDivisor = 3;
            int secondDivisor = 5;

            var random = new Random();
            int number = random.Next(0, 100);

            int sum = 0;

            Console.WriteLine($"number = {number}");

            for (int i = 1; i <= number; i++)
            {
                if ((i % firstDivisor == 0) || (i % secondDivisor == 0))
                {
                    Console.Write($"{sum} + {i} = ");
                    sum += i;
                    Console.WriteLine(sum);
                }
            }

            Console.WriteLine($"sum = {sum}");
        }
    }
}