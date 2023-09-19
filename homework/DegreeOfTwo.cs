using System;

namespace homework
{
    internal static class DegreeOfTwo
    {
        public static void Main1(string[] args)
        {
            var random = new Random();

            int maxNumber = 1000;
            int number = random.Next(0, maxNumber);
            Console.WriteLine($"Number = {number}");

            int baseOfPower = 2;
            int result = 1;
            int power = 0;
            
            while (result <= number)
            {
                power++;
                result *= baseOfPower;
                Console.WriteLine(result);
            }
            
            Console.WriteLine($"Number = {number}; Result = {result}; Power = {power}");
        }
    }
}