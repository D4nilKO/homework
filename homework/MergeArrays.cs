using System;
using System.Collections.Generic;

namespace homework;

internal static class MergeArrays
{
    public static void Main1(string[] args)
    {
        string[] array1 = { "1", "2", "1" };
        string[] array2 = { "3", "2" };

        List<string> mergedList = new();

        mergedList = Merge(mergedList, array1, array2);

        foreach (var substring in mergedList)
        {
            Console.Write(substring + " ");
        }
    }

    private static List<string> Merge(List<string> list, string[] array1, string[] array2)
    {
        list = AddNonRepeatingElements(list, array1);
        list = AddNonRepeatingElements(list, array2);

        return list;
    }

    private static List<string> AddNonRepeatingElements(List<string> list, string[] array)
    {
        foreach (string element in array)
        {
            bool isRepeated = list.Contains(element);

            if (isRepeated == false)
            {
                list.Add(element);
            }
        }

        return list;
    }
}