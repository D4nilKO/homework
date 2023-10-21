using System;
using System.Collections.Generic;
using System.Linq;
using static homework.OOP.Auqarium.UserUtils;

namespace homework.OOP.Auqarium;

internal static class Program
{
    public static void Main(string[] args)
    {
        new Zoo().Work();
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

class Zoo
{
    private Aquarium _aquarium;

    private List<Fish> _allFishes = new()
    {
        new("Петушок", 5),
        new("Бурый скалозуб", 10),
        new("Сом", 8),
        new("Золотая рыбка", 3),
        new("Формоза", 2),
        new("Гуппи", 6),
        new("Кардинал", 7),
        new("Расбора мера", 11),
    };

    public Zoo()
    {
        _aquarium = new(GetRandomFishes(_allFishes));
    }

    public void Work()
    {
    }

    private List<Fish> GetRandomFishes(List<Fish> fishes)
    {
        List<Fish> resultFishes = new();

        int maxFishesCount = 6;
        int fishesCount = GetRandomNumber(maxFishesCount);

        for (int i = 0; i < fishesCount; i++)
        {
            Fish fish = fishes[GetRandomNumber(fishes.Count)];
            resultFishes.Add(fish);
        }

        return resultFishes;
    }
}

class Aquarium
{
    private List<Fish> _fishes;
    private int _capacity = 10;

    public Aquarium(List<Fish> fishes)
    {
        _fishes = fishes.ToList();
        Year = 0;
    }

    public int Year { get; private set; }

    public void AddFish(Fish fish)
    {
        if (fish != null && _fishes.Count < _capacity)
        {
            _fishes.Add(fish);
        }
    }

    public void RemoveFish(Fish fish)
    {
        _fishes.Remove(fish);
    }

    public void WaitYear()
    {
        Year++;

        foreach (Fish fish in _fishes)
        {
            fish.AddYear();

            if (fish.Dead == false)
                continue;

            RemoveFish(fish);
        }
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Сейчас {Year} год.");

        foreach (var fish in _fishes)
        {
            fish.ShowInfo();
        }
    }
}

class Fish
{
    public Fish(string name, int maxAge)
    {
        Name = name;
        MaxAge = maxAge;
        Age = 0;
    }

    public string Name { get; private set; }
    public int MaxAge { get; private set; }
    public int Age { get; private set; }
    public bool Dead => Age >= MaxAge;

    public void AddYear()
    {
        if (Dead)
        {
            Console.WriteLine($"{Name} уже умерла");
        }
        else
        {
            Age++;
        }
    }

    public void ShowInfo()
    {
        Console.WriteLine(Dead
            ? $"{Name} - умерла"
            : $"{Name} | {Age}/{MaxAge}");
    }
}