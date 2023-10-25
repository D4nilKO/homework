using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.TroopsUnification.UserUtils;

namespace homework.LINQ.TroopsUnification;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new MilitaryBase().Work();
    }
}

class MilitaryBase
{
    private List<Troop> _troops = new();

    public MilitaryBase()
    {
        _troops.Add(new Troop(1));
        _troops.Add(new Troop(2));
    }

    public void Work()
    {
        Console.WriteLine("Вот изначальныий список войск: \n");
        _troops[0].ShowInfo();
        _troops[1].ShowInfo();
        SoldierTransfer(_troops[0], _troops[1], 'Б');

        Console.WriteLine("Вот текущий список войск: \n");
        _troops[0].ShowInfo();
        _troops[1].ShowInfo();
    }

    private void SoldierTransfer(Troop firstTroop, Troop secondTroop, char startLetter)
    {
        List<Soldier> firstSoldiers = firstTroop.GetSoldiers();

        List<Soldier> soldiersForTransfer =
            firstSoldiers.Where(soldier => soldier.LastName.StartsWith(startLetter.ToString())).ToList();

        firstTroop.RemoveSoldiers(soldiersForTransfer);
        secondTroop.AddSoldiers(soldiersForTransfer);
    }
}

class Troop
{
    private List<Soldier> _soldiers = new();
    private List<string> _firstNames = new();
    private List<string> _lastNames = new();

    public Troop(int number)
    {
        _firstNames.Add("Кирилл");
        _firstNames.Add("Михаил");
        _firstNames.Add("Ярослав");
        _firstNames.Add("Михаил");
        _firstNames.Add("Денис");
        _firstNames.Add("Михаил");
        _firstNames.Add("Максим");
        _firstNames.Add("Александр");
        _firstNames.Add("Дамир");
        _firstNames.Add("Максим");

        _lastNames.Add("Потапов");
        _lastNames.Add("Андреев");
        _lastNames.Add("Евдокимов");
        _lastNames.Add("Алешин");
        _lastNames.Add("Смирнов");
        _lastNames.Add("Степанов");
        _lastNames.Add("Зайцев");
        _lastNames.Add("Бабаев");
        _lastNames.Add("Бабакин");
        _lastNames.Add("Бабаков");
        _lastNames.Add("Бабанин");
        _lastNames.Add("Бабанов");
        _lastNames.Add("Бабарыкин");
        _lastNames.Add("Бабарыко");

        Number = number;

        CreateSoldiers(30);
    }

    public int Number { get; private set; }

    public List<Soldier> GetSoldiers()
    {
        return _soldiers.ToList();
    }

    public void AddSoldiers(List<Soldier> soldiers)
    {
        foreach (Soldier soldier in soldiers)
            _soldiers.Add(soldier);
    }

    public void RemoveSoldiers(List<Soldier> soldiers)
    {
        foreach (Soldier soldier in soldiers)
            _soldiers.Remove(soldier);
    }

    public void ShowInfo()
    {
        Console.WriteLine("{0} Отряд: ", Number);

        foreach (Soldier soldier in _soldiers)
        {
            soldier.ShowInfo();
        }
        
        Console.WriteLine();
    }

    private void CreateSoldiers(int soldiersCount)
    {
        for (int i = 0; i < soldiersCount; i++)
        {
            string firstName = _firstNames[GetRandomNumber(_firstNames.Count)];
            string lastName = _lastNames[GetRandomNumber(_lastNames.Count)];
            string number = $"{Number} - {i + 1}";

            Soldier soldier = new(firstName, lastName, number);
            _soldiers.Add(soldier);
        }
    }
}

class Soldier
{
    public Soldier(string firstName, string lastName, string number)
    {
        FirstName = firstName;
        LastName = lastName;
        Number = number;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Number { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine("Номер: {0} | Фамилия: {1} | Имя: {2}", Number, LastName, FirstName);
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