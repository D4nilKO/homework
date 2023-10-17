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
}