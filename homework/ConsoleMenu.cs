using System;

namespace homework;

internal static class ConsoleMenu
{
    public static void Main1(string[] args)
    {
        const string CommandSetName = "1";
        const string CommandChangeConsoleColor = "2";
        const string CommandSetPassword = "3";
        const string CommandEnterPassword = "4";
        const string CommandWriteName = "5";
        const string CommandExit = "exit";

        string menuText1 = $"{CommandSetName} - Установить имя";
        string menuText2 = $"{CommandChangeConsoleColor} - Поменять цвет консоли";
        string menuText3 = $"{CommandSetPassword} - Установить пароль";
        string menuText4 = $"{CommandEnterPassword} - Ввести пароль";
        string menuText5 = $"{CommandWriteName} - Вывести имя (доступно только после ввода пароля)";
        string menuTextExit = $"{CommandExit} - Выйти из программы";

        string name = "";
        string password = "";
        string testPassword;
        string desiredOperation = "";

        bool accessIsAllowed = false;
        bool nameIsEntered = false;
        bool passwordIsSet = false;

        while (desiredOperation != CommandExit)
        {
            Console.WriteLine(menuText1);
            Console.WriteLine(menuText2);
            Console.WriteLine(menuText3);
            Console.WriteLine(menuText4);
            Console.WriteLine(menuText5);
            Console.WriteLine(menuTextExit);

            Console.Write("Выберете необходимую операцию: ");
            desiredOperation = Console.ReadLine();

            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandSetName:
                    Console.Write("Новое имя: ");
                    name = Console.ReadLine();
                    nameIsEntered = true;
                    Console.WriteLine();
                    break;

                case CommandChangeConsoleColor:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case CommandSetPassword:
                    Console.WriteLine("Введите новый пароль");
                    password = Console.ReadLine();
                    passwordIsSet = true;

                    Console.WriteLine($"Пароль установлен. Новый пароль : {password}");
                    break;

                case CommandEnterPassword:
                    if (passwordIsSet)
                    {
                        Console.Write("Введите пароль: ");
                        testPassword = Console.ReadLine();

                        if (testPassword == password)
                        {
                            Console.WriteLine("Доступ разрешен");
                            accessIsAllowed = true;
                        }
                        else
                        {
                            Console.WriteLine("Пароль не совпадает");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Пароль не задан. Сначала задайте пароль!");
                    }

                    Console.WriteLine();
                    break;

                case CommandWriteName:
                    if (accessIsAllowed && nameIsEntered)
                    {
                        Console.WriteLine(name + "\n");
                    }

                    break;

                case CommandExit:
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Вы ввели неверную команду");
                    break;
            }
        }
    }
}