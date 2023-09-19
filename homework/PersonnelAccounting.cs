using System;

namespace homework
{
    internal static class PersonnelAccounting
    {
        public static void Main1(string[] args)
        {
            const string CommandAddDossier = "1";
            const string CommandDisplayAllDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandSearchByLastName = "4";
            const string CommandExit = "exit";

            string menuText1 = $"{CommandAddDossier} - Добавить досье";
            string menuText2 = $"{CommandDisplayAllDossiers} - Вывести все досье";
            string menuText3 = $"{CommandDeleteDossier} - Удалить досье";
            string menuText4 = $"{CommandSearchByLastName} - Поиск по фамилии";
            string menuTextExit = $"{CommandExit} - Выйти из программы";

            string[] fullNames = new string[0];
            string[] positions = new string[0];

            string desiredOperation = "";

            while (desiredOperation != CommandExit)
            {
                Console.WriteLine();

                Console.WriteLine(menuText1);
                Console.WriteLine(menuText2);
                Console.WriteLine(menuText3);
                Console.WriteLine(menuText4);
                Console.WriteLine(menuTextExit);

                Console.Write("Выберете необходимую операцию: ");
                desiredOperation = Console.ReadLine();

                Console.WriteLine();

                switch (desiredOperation)
                {
                    case CommandAddDossier:
                        AddDossier(ref fullNames, ref positions);
                        break;

                    case CommandDisplayAllDossiers:
                        DisplayAllDossiers(fullNames, positions);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(ref fullNames, ref positions);
                        break;

                    case CommandSearchByLastName:
                        SearchByLastName(fullNames, positions);
                        break;

                    case CommandExit:
                        Console.WriteLine("Выход...");
                        break;

                    default:
                        Console.WriteLine("Введена неверная операция.");
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу... ");
                Console.ReadKey();
            }
        }

        private static string[] AddElementAndGetArray(string[] array, string value)
        {
            string[] temporaryArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                temporaryArray[i] = array[i];
            }

            temporaryArray[temporaryArray.Length - 1] = value;

            return temporaryArray;
        }

        private static string[] RemoveElementAndGetArray(string[] array, int index)
        {
            var temporaryArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
            {
                temporaryArray[i] = array[i];
            }

            for (int i = index; i < temporaryArray.Length; i++)
            {
                temporaryArray[i] = array[i + 1];
            }

            return temporaryArray;
        }

        private static void AddDossier(ref string[] _fullNames, ref string[] _positions)
        {
            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();
            _fullNames = AddElementAndGetArray(_fullNames, fullName);

            Console.Write("Введите должность: ");
            string position = Console.ReadLine();
            _positions = AddElementAndGetArray(_positions, position);

            Console.WriteLine("Досье успешно добавлено.");

            Console.WriteLine();
        }

        private static void DeleteDossier(ref string[] _fullNames, ref string[] _positions)
        {
            int count = Math.Min(_fullNames.Length, _positions.Length);

            if (count != 0)
            {
                Console.Write("Введите номер досье, которое хотите удалить: ");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;

                if (index >= 0 && index < count)
                {
                    _fullNames = RemoveElementAndGetArray(_fullNames, index);
                    _positions = RemoveElementAndGetArray(_positions, index);

                    Console.WriteLine($"Вы удалили досье под номером {index + 1}");
                }
                else
                {
                    Console.WriteLine("Вы ввели неверный номер.");
                }
            }
            else
            {
                Console.WriteLine("Нет ни одного досье.");
            }

            Console.WriteLine();
        }

        private static void DisplayDossier(int number, string _fullName, string _position)
        {
            Console.WriteLine($"({number + 1}) {_fullName} - {_position}");
        }

        private static void DisplayAllDossiers(string[] _fullNames, string[] _positions)
        {
            int count = Math.Min(_fullNames.Length, _positions.Length);

            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("Все досье:");
                    DisplayDossier(i, _fullNames[i], _positions[i]);
                }
            }
            else
            {
                Console.WriteLine("Нет ни одного досье.");
            }

            Console.WriteLine();
        }

        private static void SearchByLastName(string[] _fullNames, string[] _positions)
        {
            int count = Math.Min(_fullNames.Length, _positions.Length);

            if (count != 0)
            {
                Console.Write("Введите фамилию для поиска: ");
                string lastName = Console.ReadLine();

                Console.WriteLine();

                bool isFound = false;

                for (int i = 0; i < count; i++)
                {
                    char separator = ' ';
                    string[] substringFullNames = _fullNames[i].Split(separator);

                    if (string.Equals(substringFullNames[0].ToLower(), lastName.ToLower(),
                            StringComparison.CurrentCultureIgnoreCase))
                    {
                        isFound = true;

                        Console.Write($"Искомое досье: ");
                        DisplayDossier(i, _fullNames[i], _positions[i]);
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
}