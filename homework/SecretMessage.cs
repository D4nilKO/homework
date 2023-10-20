using System;

namespace homework
{
    internal static class SecretMessage
    {
        public static void Main1(string[] args)
        {
            string password = "123";
            string secretMessage = "Убийца - Дворецкий!";

            int attempts = 3;

            for (int i = attempts; i > 0; i--)
            {
                Console.WriteLine("Введите Пароль: ");
                string testPassword = Console.ReadLine();

                if (testPassword == password)
                {
                    Console.WriteLine(secretMessage);
                    break;
                }

                if (i - 1 != 0)
                {
                    Console.WriteLine($"У вас осталось {i - 1} попыток, попробуйте еще");
                }
            }
        }
    }
}