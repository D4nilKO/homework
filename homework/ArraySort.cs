using System;

namespace homework;

internal static class ArraySort
{
    public static void Main1(string[] args)
    {
        Random random = new Random();

        int size = 30;
        int[] array = new int[size];

        int minRandomNumber = 0;
        int maxRandomNumber = 100;

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(minRandomNumber, maxRandomNumber + 1);
            Console.Write(array[i] + " ");
        }

        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i] > array[j])
                {
                    int temporary = array[i];
                    array[i] = array[j];
                    array[j] = temporary;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine();

        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
    }
}