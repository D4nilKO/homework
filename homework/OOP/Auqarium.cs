using System;
using System.Collections.Generic;
using System.Linq;
using static homework.UserUtils;

namespace homework.OOP.Aquarium;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Zoo().Work();
    }
}

class Zoo
{
    private Aquarium _aquarium;

    private List<Fish> _allFishes = new()
    {
        new Fish("Петушок", 5),
        new Fish("Бурый скалозуб", 10),
        new Fish("Сом", 8),
        new Fish("Золотая рыбка", 3),
        new Fish("Формоза", 2),
        new Fish("Гуппи", 6),
        new Fish("Кардинал", 7),
        new Fish("Расбора мера", 11),
    };

    public Zoo()
    {
        _aquarium = new Aquarium(GetRandomFishes(_allFishes));
    }

    public void Work()
    {
        const int CommandAddFish = 1;
        const int CommandRemoveFish = 2;
        const int CommandWait = 3;
        const int CommandExit = 4;

        Dictionary<int, string> actionsByCommand = new()
        {
            { CommandAddFish, "Добавить рыбу в аквариум" },
            { CommandRemoveFish, "Убрать рыбу из аквариума" },
            { CommandWait, "Подождать 1 итерацию..." },
            { CommandExit, "Выйти из программы" }
        };

        bool isContinue = true;

        while (isContinue)
        {
            Console.WriteLine($"Сейчас {_aquarium.Year} год.");
            _aquarium.ShowFishes(_aquarium.GetFishes());

            Console.WriteLine("Меню:");

            foreach (KeyValuePair<int, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write("\nВыберете необходимую операцию: ");
            int desiredOperation = GetNumberFromRange(1, 4);
            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandAddFish:
                    AddFishToAquarium();
                    break;

                case CommandRemoveFish:
                    RemoveFishFromAquarium();
                    break;

                case CommandWait:
                    _aquarium.WaitYear();
                    break;

                case CommandExit:
                    isContinue = false;
                    Console.WriteLine("Выход...");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения... ");
            Console.ReadKey();

            Console.Clear();
        }
    }

    private void AddFishToAquarium()
    {
        _aquarium.AddFish(ChooseFish(_allFishes).Clone());
    }

    private void RemoveFishFromAquarium()
    {
        _aquarium.RemoveFish(ChooseFish(_aquarium.GetFishes()));
        
    }

    private Fish ChooseFish(List<Fish> fishes)
    {
        _aquarium.ShowFishes(fishes);
        Fish fish = fishes[GetNumberFromRange(1, fishes.Count) - 1];
        
        Console.Write("Вы выбрали: ");
        fish.ShowInfo();
        
        return fish;
    }

    private List<Fish> GetRandomFishes(List<Fish> fishes)
    {
        List<Fish> resultFishes = new();

        int minFishesCount = 3;
        int maxFishesCount = 8;
        int fishesCount = GetRandomNumber(minFishesCount, maxFishesCount);

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
    private List<Fish> _fishes = new();
    private int _capacity = 10;

    public Aquarium(List<Fish> fishes)
    {
        foreach (Fish fish in fishes)
        {
            AddFish(fish.Clone());
        }

        Year = 0;
    }

    public int Year { get; private set; }

    public List<Fish> GetFishes()
    {
        return _fishes.ToList();
    }

    public void AddFish(Fish fish)
    {
        if (fish != null && _fishes.Count < _capacity)
        {
            _fishes.Add(fish);
        }
        else
        {
            Console.WriteLine("Аквариум уже полон.");
        }
    }

    public void RemoveFish(Fish fish)
    {
        _fishes.Remove(fish);
    }

    public void WaitYear()
    {
        Year++;

        for (int i = _fishes.Count - 1; i >= 0; i--)
        {
            Fish fish = _fishes[i];

            if (fish.Dead)
            {
                RemoveFish(fish);
                continue;
            }

            fish.AddYear();
        }
    }

    public void ShowFishes(List<Fish> fishes)
    {
        if (_fishes.Count != 0)
        {
            for (int i = 0; i < fishes.Count; i++)
            {
                Fish fish = fishes[i];
                Console.Write($"{i + 1} - ");
                fish.ShowInfo();
            }
        }
        else
        {
            Console.WriteLine("Аквариум пуст...");
        }

        Console.WriteLine();
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

    public Fish Clone()
    {
        return new Fish(Name, MaxAge);
    }

    public void AddYear()
    {
        Age++;
    }

    public void ShowInfo()
    {
        Console.WriteLine(Dead
            ? $"{Name} - умерла"
            : $"{Name} | {Age}/{MaxAge}");
    }
}