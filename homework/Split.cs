using System;

namespace homework;

internal static class Split
{
    public static void Main1(string[] args)
    {
        string text = Console.ReadLine();

        char separatorSymbol = ' ';
            
        string[] words = text.Split(separatorSymbol);

        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }
}