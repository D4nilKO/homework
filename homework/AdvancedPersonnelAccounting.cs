using System;
using System.Collections.Generic;
using System.Linq;

namespace homework;

internal static class AdvancedPersonnelAccounting
{
    public static void Main1(string[] args)
    {
        const string CommandAddDossier = "1";
        const string CommandDisplayAllDossiers = "2";
        const string CommandDeleteDossier = "3";
        const string CommandSearchByLastName = "4";
        const string CommandExit = "exit";

        Dictionary<string, string> menu = new()
        {
            { CommandAddDossier, "Добавить досье" },
            { CommandDisplayAllDossiers, "Вывести все досье" },
            { CommandDeleteDossier, "Удалить досье" },
            { CommandSearchByLastName, "Поиск по фамилии" },
            { CommandExit, "Выйти из программы" }
        };

        Dictionary<string, string> persons = new();

        bool isContinue = true;


        while (isContinue)
        {
            Console.WriteLine();

            Console.WriteLine("\nМеню:");

            foreach (KeyValuePair<string, string> item in menu)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            Console.Write("Выберете необходимую операцию: ");
            string desiredOperation = Console.ReadLine();

            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandAddDossier:
                    AddDossier(persons);
                    break;

                case CommandDisplayAllDossiers:
                    DisplayAllDossiers(persons);
                    break;

                case CommandDeleteDossier:
                    DeleteDossier(persons);
                    break;

                case CommandSearchByLastName:
                    SearchByLastName(persons);
                    break;

                case CommandExit:
                    isContinue = false;
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Введена неверная операция.");
                    break;
            }

            if (isContinue)
            {
                Console.Write("Для продолжения нажмите любую клавишу... ");
                Console.ReadKey();
            }
        }
    }

    private static void AddDossier(Dictionary<string, string> persons)
    {
        Console.Write("Введите ФИО: ");
        string fullName = Console.ReadLine();

        Console.Write("Введите должность: ");
        string position = Console.ReadLine();

        persons.Add(fullName, position);

        Console.WriteLine("Досье успешно добавлено.");

        Console.WriteLine();
    }

    private static void DeleteDossier(Dictionary<string, string> persons)
    {
        DisplayAllDossiers(persons);

        int count = persons.Keys.Count;

        if (count != 0)
        {
            Console.Write("Введите номер досье, которое хотите удалить: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                index--;

                if (index >= 0 && index < count)
                {
                    persons.Remove(persons.ElementAt(index).Key);

                    Console.WriteLine($"Вы удалили досье под номером {index + 1}");
                }
                else
                {
                    Console.WriteLine("Вы ввели неверный номер.");
                }
            }
            else
            {
                Console.WriteLine("Ожидалось целое число.");
            }
        }

        Console.WriteLine();
    }

    private static void DisplayAllDossiers(Dictionary<string, string> persons)
    {
        int count = persons.Keys.Count;

        if (count != 0)
        {
            Console.WriteLine("Все досье:");

            int index = 1;

            foreach (KeyValuePair<string, string> person in persons)
            {
                Console.WriteLine($"{index} - {person.Key} - {person.Value}");
                index++;
            }
        }
        else
        {
            Console.WriteLine("Нет ни одного досье.");
        }

        Console.WriteLine();
    }

    private static void SearchByLastName(Dictionary<string, string> persons)
    {
        int count = persons.Keys.Count;

        if (count != 0)
        {
            Console.Write("Введите фамилию для поиска: ");
            string lastName = Console.ReadLine();

            Console.WriteLine();

            bool isFound = false;

            for (int i = 0; i < count; i++)
            {
                char separator = ' ';

                KeyValuePair<string, string> person = persons.ElementAt(i);

                string[] substringFullNames = person.Key.Split(separator);

                if (string.Equals(substringFullNames[0].ToLower(), lastName?.ToLower(),
                        StringComparison.CurrentCultureIgnoreCase))
                {
                    isFound = true;

                    Console.Write($"Искомое досье: ");

                    Console.WriteLine($"{i + 1} - {person.Key} - {person.Value}");
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("Досье с такой фамилией не найдено.");
            }
        }
        else
        {
            Console.WriteLine("Нет ни одного досье.");
        }

        Console.WriteLine();
    }
}