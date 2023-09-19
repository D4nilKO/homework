using System;

namespace homework
{
    internal static class DynamicArray
    {
        public static void Main1(string[] args)
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            string menuTextNumber = $"любое число - добавить это число к сумме";
            string menuTextSum = $"{CommandSum} - Вывести сумму чисел";
            string menuTextExit = $"{CommandExit} - Выйти из программы";

            int[] numbers = new int[0];

            string desiredOperation = "";

            Console.WriteLine(menuTextNumber);
            Console.WriteLine(menuTextSum);
            Console.WriteLine(menuTextExit);

            while (desiredOperation != CommandExit)
            {
                desiredOperation = Console.ReadLine();

                switch (desiredOperation)
                {
                    case CommandSum:
                        int sum = 0;

                        foreach (var number in numbers)
                        {
                            sum += number;
                        }

                        Console.WriteLine($"Сумма чисел = {sum}");
                        break;

                    case CommandExit:
                        Console.WriteLine("Выход...");
                        break;

                    default:
                        int[] temporaryNumbers = new int[numbers.Length + 1];

                        for (int i = 0; i < numbers.Length; i++)
                        {
                            temporaryNumbers[i] = numbers[i];
                        }

                        temporaryNumbers[temporaryNumbers.Length - 1] = Convert.ToInt32(desiredOperation);

                        numbers = temporaryNumbers;
                        break;
                }
            }
        }
    }
}