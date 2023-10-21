using System;

namespace homework;

internal static class Multiples
{
    public static void Main1(string[] args)
    {
        int loverLimit = 1;
        int upperLimit = 27;
        int loverCycleLimit = 99;
        int upperCycleLimit = 1000;

        Random random = new Random();
        int number = random.Next(loverLimit, upperLimit + 1);
        Console.WriteLine($"Number = {number}");

        int count = 0;

        for (int i = number; i < upperCycleLimit; i += number)
        {
            if (i > loverCycleLimit)
            {
                Console.WriteLine(i);
                count++;
            }
        }

        Console.WriteLine($"Number = {number}; Count = {count}");
    }
}