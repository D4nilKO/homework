using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.SearchCriminal.UserUtils;

namespace homework.LINQ.SearchCriminal;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new DetectiveHelper().Work();
    }
}

internal static class UserUtils
{
    private static Random s_random = new();

    public static bool GetRandomBool()
    {
        return s_random.Next() > (Int32.MaxValue / 2);
    }

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static int GetRandomNumber(int max)
    {
        return s_random.Next(max);
    }

    public static int GetNumberFromRange(int min, int max)
    {
        if (min > max)
        {
            Console.WriteLine($"min > max | {min} > {max}");
        }

        bool isLookingResult = true;
        int result = 0;

        Console.WriteLine($"Введите число от {min} до {max} включительно.");

        while (isLookingResult)
        {
            if (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.WriteLine("Некорректный ввод!");
                continue;
            }

            if (result < min || result > max)
            {
                Console.WriteLine("Введенное число не входит в диапазон!");
                continue;
            }

            isLookingResult = false;
        }

        return result;
    }
}

class DetectiveHelper
{
    private Database _database;

    private int _minHeight = 170;
    private int _maxHeight = 180;

    private int _minWeight = 50;
    private int _maxWeight = 60;

    public DetectiveHelper()
    {
        _database = new Database(_minHeight, _maxHeight, _minWeight, _maxWeight);
    }

    public void Work()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();

            Console.WriteLine("Добро пожаловать, Детектив.");

            Console.WriteLine("Введите рост: ");
            int height = GetNumberFromRange(_minHeight, _maxHeight);

            Console.WriteLine("Введите вес: ");
            int weight = GetNumberFromRange(_minWeight, _maxWeight);

            _database.ShowNationalities();
            Console.WriteLine("Введите номер национальности:");
            int nationalityNumber = GetNumberFromRange(1, _database.GetNationalities().Count);
            string nationality = _database.GetNationalities()[nationalityNumber - 1];

            Console.WriteLine();
            _database.FindCriminals(height, weight, nationality);

            Console.WriteLine("Нажмите любую клавишу для продолжения... ");
            Console.ReadKey();
        }
    }
}

class Database
{
    private List<Criminal> _criminals = new();
    private List<string> _names = new();
    private List<string> _nationalities = new();

    public Database(int minHeight, int maxHeight, int minWeight, int maxWeight)
    {
        _names.Add("Киселева Александра Давидовна");
        _names.Add("Антонова Амина Кирилловна");
        _names.Add("Давыдова Александра Степановна");
        _names.Add("Суворова Ева Александровна");
        _names.Add("Иванова Оливия Ивановна");
        _names.Add("Волошина Анна Всеволодовна");
        _names.Add("Морозова Мирослава Ивановна");
        _names.Add("Дементьева Анна Кирилловна");
        _names.Add("Молчанова Ясмина Марковна");

        _nationalities.Add("Nauruan");
        _nationalities.Add("Egyptian");
        _nationalities.Add("Malagasy");
        _nationalities.Add("Senegalese");
        _nationalities.Add("Monegasque");
        _nationalities.Add("Beninese");
        _nationalities.Add("Rwandan");
        _nationalities.Add("Ukrainian");
        _nationalities.Add("Peruvian");

        CreateCriminals(10000, minHeight, maxHeight, minWeight, maxWeight);
    }

    public void FindCriminals(int height, int weight, string nationality)
    {
        List<Criminal> filteredCriminals = _criminals.Where(criminal =>
            criminal.Custody == false &&
            criminal.Height == height &&
            criminal.Weight == weight &&
            criminal.Nationality == nationality).ToList();

        if (filteredCriminals.Count == 0)
        {
            Console.WriteLine("По данным параметрам никого не удалось найти.");
            return;
        }

        Console.WriteLine("Вот найденные преступники:\n");

        foreach (Criminal criminal in filteredCriminals)
        {
            criminal.ShowInfo();
            Console.WriteLine();
        }
    }

    public List<string> GetNationalities()
    {
        return _nationalities.ToList();
    }

    public void ShowNationalities()
    {
        for (int i = 0; i < _nationalities.Count; i++)
        {
            string nationality = _nationalities[i];
            Console.WriteLine($"{i + 1} - {nationality}");
        }

        Console.WriteLine();
    }

    private void CreateCriminals(int count, int minHeight, int maxHeight, int minWeight, int maxWeight)
    {
        for (int i = 0; i < count; i++)
        {
            string name = _names[GetRandomNumber(_names.Count)];

            bool custody = GetRandomBool();

            int height = GetRandomNumber(minHeight, maxHeight);

            int weight = GetRandomNumber(minWeight, maxWeight);

            string nationality = _nationalities[GetRandomNumber(_nationalities.Count)];

            Criminal criminal = new(name, custody, height, weight, nationality);
            _criminals.Add(criminal);
        }
    }
}

class Criminal
{
    public Criminal(string name, bool custody, int height, int weight, string nationality)
    {
        Name = name;
        Custody = custody;
        Height = height;
        Weight = weight;
        Nationality = nationality;
    }

    public string Name { get; private set; }
    public bool Custody { get; private set; }
    public int Height { get; private set; }
    public int Weight { get; private set; }
    public string Nationality { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine("{0} | заключен под стражу: {1} | Рост: {2} | Вес: {3} | Национальность: {4}", Name, Custody,
            Height, Weight, Nationality);
    }
}