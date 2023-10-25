using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.OverdueProductsDetection.UserUtils;

namespace homework.LINQ.OverdueProductsDetection;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Storage().Work();
    }
}

class Storage
{
    private List<CannedMeat> _cannedMeats = new();
    private List<string> _names = new();

    public Storage()
    {
        _names.Add("Курица");
        _names.Add("Свинина");
        _names.Add("Говдяина");
        _names.Add("Индейка");
        _names.Add("Курица + Свинина");
        _names.Add("Курица + Говдяина");
        _names.Add("Курица + Индейка");
        _names.Add("Свинина + Говдяина");
        _names.Add("Свинина + Индейка");
        _names.Add("Говдяина + Индейка");

        CreateCannedMeats(15);
    }

    public void Work()
    {
        Console.WriteLine("Список продуктов:");
        ShowProducts(_cannedMeats);

        Console.WriteLine("\nСписок просроченных продуктов:");
        ShowProducts(GetOverdueProducts());
    }

    private void ShowProducts(List<CannedMeat> products)
    {
        Console.WriteLine();

        foreach (CannedMeat product in products)
        {
            product.ShowInfo();
        }
    }

    private List<CannedMeat> GetOverdueProducts()
    {
        int currentYear = DateTime.Now.Year;

        return _cannedMeats.Where(meat => meat.ProductionYear + meat.StorageLife < currentYear).ToList();
    }

    private void CreateCannedMeats(int count)
    {
        int minProductionYear = 2010;
        int maxProductionYear = 2023;
        int minStorageLife = 1;
        int maxStorageLife = 10;

        for (int i = 0; i < count; i++)
        {
            string name = _names[GetRandomNumber(_names.Count)];
            int productionYear = GetRandomNumber(minProductionYear, maxProductionYear);
            int storageLife = GetRandomNumber(minStorageLife, maxStorageLife);
            int number = i + 1;

            CannedMeat cannedMeat = new(name, number, productionYear, storageLife);
            _cannedMeats.Add(cannedMeat);
        }
    }
}

class CannedMeat
{
    public CannedMeat(string name, int number, int productionYear, int storageLife)
    {
        Name = name;
        Number = number;
        ProductionYear = productionYear;
        StorageLife = storageLife;
    }

    public string Name { get; private set; }
    public int Number { get; private set; }
    public int ProductionYear { get; private set; }
    public int StorageLife { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine("Номер: {0} | {1} | Год производства: {2} | Срок хранения: {3}", Number, Name, ProductionYear,
            StorageLife);
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