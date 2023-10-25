using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.HospitalAnarchy.UserUtils;

namespace homework.LINQ.HospitalAnarchy;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Hospital().Work();
    }
}

class Hospital
{
    private List<Patient> _patients = new();
    private List<string> _names = new();
    private List<string> _diagnoses = new();

    public Hospital()
    {
        _names.Add("Киселева Александра Давидовна");
        _names.Add("Антонова Валерия Кирилловна");
        _names.Add("Антонова Амина Кирилловна");
        _names.Add("Давыдова Александра Степановна");
        _names.Add("Суворова Ева Александровна");
        _names.Add("Иванова Оливия Ивановна");
        _names.Add("Волошина Анна Всеволодовна");
        _names.Add("Морозова Мирослава Ивановна");
        _names.Add("Дементьева Анна Кирилловна");
        _names.Add("Молчанова Ясмина Марковна");
        _names.Add("Александрова Маргарита Демьяновна");
        _names.Add("Сергеева Стефания Александровна");
        _names.Add("Морозова Анастасия Валерьевна");
        _names.Add("Ковалева Милана Мироновна");
        _names.Add("Сорокина Полина Георгиевна");
        _names.Add("Смирнова Мирослава Никитична");
        _names.Add("Гончарова Александра Степановна");
        _names.Add("Цветкова Анастасия Арсентьевна");
        _names.Add("Меркулова Арина Михайловна");
        _names.Add("Тарасова Эмма Евгеньевна");

        _diagnoses.Add("ВСД");
        _diagnoses.Add("Идиопатический ангидроз");
        _diagnoses.Add("Синдром Горнера");
        _diagnoses.Add("Ювенильный остеохондроз");
        _diagnoses.Add("Инсульт");
        _diagnoses.Add("Шепелявость");
        _diagnoses.Add("Катаракта");
        _diagnoses.Add("Проказа");

        CreatePatients();
    }

    public void Work()
    {
        const string CommandSortByName = "1";
        const string CommandSortByAge = "2";
        const string CommandShowByDiagnosis = "3";
        const string CommandShowAll = "4";
        const string CommandExit = "Exit";

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandSortByName, "Отсортировать всех больных по фио" },
            { CommandSortByAge, "Отсортировать всех больных по возрасту" },
            { CommandShowByDiagnosis, "Вывести больных с определенным заболеванием" },
            { CommandShowAll, "Вывести всех больных" },
            { CommandExit, "Выйти из программы" }
        };

        bool isContinue = true;

        while (isContinue)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\nМеню:");

            foreach (KeyValuePair<string, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write("\nВыберете необходимую операцию: ");
            string desiredOperation = Console.ReadLine();
            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandSortByName:
                    ShowPatients(GetPatientsByName());
                    break;

                case CommandSortByAge:
                    ShowPatients(GetPatientsByAge());
                    break;

                case CommandShowByDiagnosis:
                    ShowPatients(GetFilterPatientsByDiagnosis());
                    break;
                
                case CommandShowAll:
                    ShowPatients(_patients);
                    break;

                case CommandExit:
                    isContinue = false;
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Повторите ввод.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения... ");
            Console.ReadKey();
        }
    }

    private List<Patient> GetPatientsByName()
    {
        return _patients.OrderBy(patient => patient.Name).ToList();
    }

    private List<Patient> GetPatientsByAge()
    {
        return _patients.OrderBy(patient => patient.Age).ToList();
    }

    private List<Patient> GetFilterPatientsByDiagnosis()
    {
        Console.WriteLine("Вот все пациенты нашей клиники:\n");
        ShowPatients(_patients);

        Console.WriteLine("Введите диагноз, по которому хотите отобрать пациентов:");
        string diagnosis = Console.ReadLine();

        Console.WriteLine();
        
        List<Patient> filteredPatients = _patients.Where(patient => string.Equals(patient.Diagnosis, diagnosis, StringComparison.CurrentCultureIgnoreCase)).ToList();

        if (filteredPatients.Count == 0)
        {
            Console.WriteLine("Никого не удалось найти с таким диагнозом...");
        }
        
        return filteredPatients;
    }

    private void CreatePatients()
    {
        int minAge = 18;
        int maxAge = 65;

        foreach (Patient patient in from name in _names
                 let age = GetRandomNumber(minAge, maxAge)
                 let diagnose = _diagnoses[GetRandomNumber(_diagnoses.Count)]
                 select new Patient(name, diagnose, age))
        {
            _patients.Add(patient);
        }
    }

    private void ShowPatients(List<Patient> patients)
    {
        foreach (Patient patient in patients)
        {
            patient.ShowInfo();
        }

        Console.WriteLine();
    }
}

class Patient
{
    public Patient(string name, string diagnosis, int age)
    {
        Name = name;
        Diagnosis = diagnosis;
        Age = age;
    }

    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Diagnosis { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine("{0} | {1} лет | Диагноз: {2}", Name, Age, Diagnosis);
    }
}

internal static class UserUtils
{
    private static Random s_random = new();

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static int GetRandomNumber(int max)
    {
        return s_random.Next(max);
    }
}