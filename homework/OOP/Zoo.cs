using System;
using System.Collections.Generic;

namespace homework.OOP.Zoo;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Zoo().Work();
    }
}

public class Zoo
{
    private List<Aviary> _aviaries = new();
    private List<Animal> _animals = new();

    public Zoo()
    {
        _animals.Add(new Animal("Тигр", "Ррррр"));
        _animals.Add(new Animal("Лев", "Арр-Ррр"));
        _animals.Add(new Animal("Обезьяна", "Уу-аа-а"));
        _animals.Add(new Animal("Лошадь", "И-го-го"));

        _aviaries.Add(new Aviary("для Тигров", _animals[0].Clone(), 5));
        _aviaries.Add(new Aviary("для Львов", _animals[1].Clone(), 4));
        _aviaries.Add(new Aviary("для Обезьян", _animals[2].Clone(), 6));
        _aviaries.Add(new Aviary("для Лошадей", _animals[3].Clone(), 2));
    }

    public void Work()
    {
        bool isContinue = true;

        while (isContinue)
        {
            Console.Clear();

            ShowAviaries();

            Console.Write("\nВыберете к какому вольеру хотите подойти: ");
            int desiredAviary = UserUtils.GetNumberFromRange(1, _aviaries.Count);
            Console.WriteLine();

            _aviaries[desiredAviary - 1].ShowFullInfo();

            Console.WriteLine("Нажмите любую клавишу для продолжения... ");
            Console.ReadKey();
        }
    }

    private void ShowAviaries()
    {
        if (_aviaries.Count == 0)
        {
            Console.WriteLine("Вольеров нет.");
            return;
        }

        Console.WriteLine("\nВольеры:");

        for (int i = 0; i < _aviaries.Count; i++)
        {
            Aviary aviary = _aviaries[i];
            Console.Write($"{i + 1} - ");
            aviary.ShowInfo();
        }
    }
}

class Aviary
{
    private List<Animal> _animals = new();

    public Aviary(string name, Animal animal, int count)
    {
        Name = name;

        for (int i = 0; i < count; i++)
        {
            _animals.Add(animal.Clone());
        }
    }

    public string Name { get; private set; }
    public int AnimalsCount => _animals.Count;

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Вольер {Name}, в нем {AnimalsCount} животных.");
    }

    public void ShowFullInfo()
    {
        ShowInfo();

        for (int i = 0; i < _animals.Count; i++)
        {
            Animal animal = _animals[i];
            Console.Write($"{i + 1} - ");
            animal.ShowInfo();
        }
    }
}

class Animal
{
    public Animal(string name, string sound, Gender sex)
    {
        Name = name;
        Sound = sound;
        Sex = sex;
    }

    public Animal(string name, string sound)
    {
        Name = name;
        Sound = sound;

        int genderCount = Enum.GetNames(typeof(Gender)).Length;
        Sex = (Gender)UserUtils.GetRandomNumber(genderCount);
    }

    public string Name { get; private set; }
    public string Sound { get; private set; }
    public Gender Sex { get; private set; }

    public Animal Clone()
    {
        return new Animal(Name, Sound);
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Name} - {Sex} - говорит: {Sound}");
    }
}

enum Gender
{
    Male,
    Female,
    Unknown
}