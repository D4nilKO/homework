using System;

namespace homework
{
    internal static class LargestElement
    {
        public static void Main1(string[] args)
        {
            int replacementNumber = 0;
            
            Random random = new Random();

            int maxRandomNumber = 9;
            int minRandomNumber = 0;

            int maxNumber = minRandomNumber;

            int size = 10;
            int[,] array = new int[size, size];
            
            Console.WriteLine("Исходный массив: ");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minRandomNumber, maxRandomNumber + 1);

                    if (array[i, j] > maxNumber)
                    {
                        maxNumber = array[i, j];
                    }

                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine($"Наибольшее число = {maxNumber}");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine("Измененный массив: ");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == maxNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        array[i, j] = replacementNumber;
                    }

                    Console.Write(array[i, j] + " ");

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }
        }
    }
}