using System;

namespace homework;

internal static class UserUtils
{
    private static Random s_random = new();

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static int GetRandomNumber(int max)
    {
        return s_random.Next(max);
    }

    public static int GetNumberFromRange(int max)
    {
        return GetNumberFromRange(0, max);
    }

    public static int GetNumberFromRange(int min, int max)
    {
        if (min > max)
        {
            Console.WriteLine($"min > max | {min} > {max}");
        }

        bool isLookingResult = true;
        int result = 0;

        Console.WriteLine($"Введите число от {min} до {max} включительно.");

        while (isLookingResult)
        {
            if (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.WriteLine("Некорректный ввод!");
                continue;
            }

            if (result < min || result > max)
            {
                Console.WriteLine("Введенное число не входит в диапазон!");
                continue;
            }

            isLookingResult = false;
        }

        return result;
    }
}