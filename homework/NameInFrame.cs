using System;

namespace homework;

internal static class NameInFrame
{
    public static void Main1(string[] args)
    {
        Console.WriteLine("Введие имя");
        string name = Console.ReadLine();

        Console.WriteLine("Введие символ");
        char frameChar = Convert.ToChar(Console.Read());

        string midFramePart = frameChar + name + frameChar;
            
        int frameSize = midFramePart.Length;

        string edgeFramePart = new string(frameChar, frameSize);
        edgeFramePart = edgeFramePart.Insert(0, "\n");
        edgeFramePart += "\n";
            
        Console.WriteLine(edgeFramePart + midFramePart + edgeFramePart);
    }
}