using System;

namespace homework
{
    internal static class ShiftingArrayValues
    {
        public static void Main1(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4 };

            int shift = 1;

            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }

            for (int i = 0; i < shift; i++)
            {
                int firstElement = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    numbers[j] = numbers[j + 1];
                }

                numbers[numbers.Length - 1] = firstElement;
            }

            Console.WriteLine();

            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
        }
    }
}