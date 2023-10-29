using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.WeaponsReport.UserUtils;

namespace homework.LINQ.WeaponsReport;

internal static class Program
{
    public static void Main(string[] args)
    {
        new MilitaryBase().Work();
    }
}

class MilitaryBase
{
    private List<Soldier> _soldiers = new();
    
    private List<string> _names = new();
    private List<string> _weapons = new();
    private List<string> _ranks = new();

    public MilitaryBase()
    {
        _names.Add("Давид Жданов");
        _names.Add("Матвей Бирюков");
        _names.Add("Дмитрий Родионов");
        _names.Add("Александр Никитин");
        _names.Add("Степан Николаев");
        _names.Add("Александр Яковлев");
        _names.Add("Матвей Дмитриев");
        _names.Add("Михаил Королев");
        _names.Add("Александр Потапов");
        _names.Add("Владислав Николаев");
        _names.Add("Матвей Петров");
        _names.Add("Степан Анисимов");
        _names.Add("Игорь Макаров");
        _names.Add("Тимофей Калинин");
        _names.Add("Виктор Лебедев");
        _names.Add("Роман Панин");
        _names.Add("Денис Косарев");

        _weapons.Add("пистолет");
        _weapons.Add("пулемет");
        _weapons.Add("ракетница");
        _weapons.Add("АК-47");
        _weapons.Add("Танк");
        _weapons.Add("Вертолет");
        _weapons.Add("Самолет");

        _ranks.Add("рядовой");
        _ranks.Add("лейтенант");
        _ranks.Add("старший лейтенант");
        _ranks.Add("подполковник");
        _ranks.Add("полковник");
        _ranks.Add("генерал");
        _ranks.Add("генерал-лейтенант");
        _ranks.Add("генерал-майор");
        _ranks.Add("генерал-капитан");
        _ranks.Add("генерал-полковник");

        CreateSoldiers(20);
    }

    public void Work()
    {
        ShowNamesAndRanks();
    }

    private void ShowNamesAndRanks()
    {
        Console.WriteLine();
        var soldiersData = _soldiers.Select(soldier => new { soldier.Name, soldier.Rank });
        
        foreach (var soldierData in soldiersData)
        {
            Console.WriteLine($"{soldierData.Name} | {soldierData.Rank}");
        }
    }

    private void CreateSoldiers(int count)
    {
        int maxServicePeriod = 180;

        for (int i = 0; i < count; i++)
        {
            string name = _names[GetRandomNumber(_names.Count)];
            string weapon = _weapons[GetRandomNumber(_weapons.Count)];
            string rank = _ranks[GetRandomNumber(_ranks.Count)];
            int servicePeriod = GetRandomNumber(maxServicePeriod);
            int number = i + 1;

            Soldier soldier = new(name, weapon, rank, servicePeriod, number);
            _soldiers.Add(soldier);
        }
    }
}

class Soldier
{
    public Soldier(string name, string weapon, string rank, int servicePeriod, int number)
    {
        Name = name;
        Weapon = weapon;
        Rank = rank;
        ServicePeriod = servicePeriod;
        Number = number;
    }

    public string Name { get; private set; }
    public string Weapon { get; private set; }
    public string Rank { get; private set; }
    public int ServicePeriod { get; private set; }
    public int Number { get; private set; }
}

internal static class UserUtils
{
    private static Random s_random = new();
    
    public static int GetRandomNumber(int max)
    {
        return s_random.Next(max);
    }
}