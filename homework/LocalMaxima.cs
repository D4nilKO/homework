using System;

namespace homework;

internal static class LocalMaxima
{
    public static void Main1(string[] args)
    {
        Random random = new Random();

        int maxRandomNumber = 9;
        int minRandomNumber = 0;

        int size = 30;
        int[] array = new int[size];

        Console.WriteLine("Массив:");

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(minRandomNumber, maxRandomNumber + 1);

            Console.Write(array[i] + " ");
        }

        Console.WriteLine("\nЛокальные максимумы: ");

        Console.ForegroundColor = ConsoleColor.Red;

        if (array[0] > array[1])
        {
            Console.Write(array[0] + " ");
        }
        else
        {
            Console.Write("  ");
        }

        for (int i = 1; i < array.Length - 1; i++)
        {
            int middle = array[i];
            int left = array[i - 1];
            int right = array[i + 1];

            if (middle > left && middle > right)
            {
                Console.Write(array[i] + " ");
            }
            else
            {
                Console.Write("  ");
            }
        }

        if (array[array.Length - 1] > array[array.Length - 2])
        {
            Console.Write(array[array.Length - 1] + " ");
        }
        else
        {
            Console.Write("  ");
        }
    }
}