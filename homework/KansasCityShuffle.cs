using System;

namespace homework;

internal static class KansasCityShuffle
{
    public static void Main1(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
            
        Display(numbers);
            
        Shuffle(numbers);

        Display(numbers);
    }

    private static void Shuffle(int[] array)
    {
        Random random = new Random();
            
        for (int firstNumber = array.Length - 1; firstNumber > 0; firstNumber--)
        {
            int secondNumber = random.Next(firstNumber + 1);
                
            Swap(array, firstNumber, secondNumber);
        }
    }

    private static void Swap(int[] array, int firstNumber, int secondNumber)
    {
        int temporaryNumber = array[firstNumber];
            
        array[firstNumber] = array[secondNumber];
        array[secondNumber] = temporaryNumber;
    }

    private static void Display(int[] array)
    {
        foreach (int number in array)
        {
            Console.Write($"{number} ");
        }

        Console.WriteLine();
    }
}