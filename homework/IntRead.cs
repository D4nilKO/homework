using System;

namespace homework
{
    internal static class IntRead
    {
        public static void Main1(string[] args)
        {
            Console.WriteLine($"Конвертированное число: {ReadInt()}");
        }

        private static int ReadInt()
        {
            Console.WriteLine("Введите число: ");

            bool success = false;
            int number = 0;

            while (success == false)
            {
                string message = Console.ReadLine();

                success = int.TryParse(message, out number);
            }

            return number;
        }
    }
}