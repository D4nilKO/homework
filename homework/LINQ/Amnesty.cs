using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.Amnesty.UserUtils;

namespace homework.LINQ.Amnesty;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Amnesty().Work();
    }
}

class Amnesty
{
    private Database _database = new();

    public void Work()
    {
        _database.ShowPrisoners();
        _database.GrantAmnesty();
        _database.ShowPrisoners();
    }
}

class Database
{
    private List<Prisoner> _prisoners = new();
    private List<string> _names = new();
    private List<string> _crimes = new();

    public Database()
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

        _crimes.Add("против личности");
        _crimes.Add("в сфере экономики");
        _crimes.Add("против мира и безопасности человечества");
        _crimes.Add("антиправительственное");

        CreateCriminals(10);
    }

    public void ShowPrisoners()
    {
        Console.WriteLine();
        Console.WriteLine("Вот текущий список заключенных: ");
        
        foreach (Prisoner prisoner in _prisoners)
        {
            prisoner.ShowInfo();
        }
    }

    public void GrantAmnesty()
    {
        Console.WriteLine();
        string chosenCrime = "антиправительственное";
        
        IEnumerable<Prisoner> releasedCriminals = _prisoners.Where(prisoner =>
            string.Equals(prisoner.Crime, chosenCrime, StringComparison.CurrentCultureIgnoreCase));

        _prisoners = _prisoners.Except(releasedCriminals).ToList();
        
        Console.WriteLine($"Произошла амнистия для осужденных по статье \"{chosenCrime}\" ");
    }

    private void CreateCriminals(int count)
    {
        for (int i = 0; i < count; i++)
        {
            string name = _names[GetRandomNumber(_names.Count)];
            string crime = _crimes[GetRandomNumber(_crimes.Count)];

            Prisoner prisoner = new(name, crime);
            _prisoners.Add(prisoner);
        }
    }
}

class Prisoner
{
    public Prisoner(string name, string crime)
    {
        Name = name;
        Crime = crime;
    }

    public string Name { get; private set; }
    public string Crime { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine("{0} | преступление: {1}", Name, Crime);
    }
}

internal static class UserUtils
{
    private static Random s_random = new();

    public static int GetRandomNumber(int max)
    {
        return s_random.Next(max);
    }
}