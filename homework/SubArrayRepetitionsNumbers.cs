using System;

namespace homework
{
    internal static class SubArrayRepetitionsNumbers
    {
        public static void Main1(string[] args)
        {
            Random random = new Random();

            int size = 30;
            int[] array = new int[size];

            int minRandomNumber = 0;
            int maxRandomNumber = 4;

            int currentNumber;
            int maxFrequentNumber = -1;

            int count = 1;
            int maxCount = 1;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRandomNumber, maxRandomNumber + 1);
                Console.Write(array[i] + " ");
            }

            Console.WriteLine();

            currentNumber = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == currentNumber)
                {
                    count++;

                    if (count > maxCount)
                    {
                        maxCount = count;
                        maxFrequentNumber = array[i];
                    }
                }
                else
                {
                    currentNumber = array[i];
                    count = 1;
                }
            }

            if (maxFrequentNumber == -1)
            {
                Console.WriteLine("В ряде чисел нет повторений");
            }
            else
            {
                Console.WriteLine($"число {maxFrequentNumber} повторяется {maxCount} раз подряд");
            }
        }
    }
}