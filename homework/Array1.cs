using System;

namespace homework;

internal static class Array1
{
    public static void Main1(string[] args)
    {
        int sum = 0;
        int multiplication = 1;

        int rowSumIndex = 1;
        int columnMultiplicationIndex = 0;

        int[,] array = new int[4, 3]
        {
            { 1, 2, 3 },
            { 9, 8, 4 },
            { 2, 3, 7 },
            { 5, 2, 8 }
        };

        for (int i = 0; i < array.GetLength(0); i++)
        {
            multiplication *= array[i, columnMultiplicationIndex];
        }

        for (int j = 0; j < array.GetLength(1); j++)
        {
            sum += array[rowSumIndex, j];
        }

        Console.WriteLine($"\nСумма второй строки = {sum}");
        Console.WriteLine($"Произведение первого столбца = {multiplication}");
    }
}