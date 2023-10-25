using System;
using System.Collections.Generic;
using System.Linq;
using static homework.LINQ.TopServerPlayers.UserUtils;

namespace homework.LINQ.TopServerPlayers;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Server().Work();
    }
}

class Server
{
    private List<Player> _players = new();
    private List<string> _names = new();

    public Server()
    {
        _names.Add("Александра");
        _names.Add("Валерия");
        _names.Add("Амина");
        _names.Add("Александра");
        _names.Add("Ева");
        _names.Add("Оливия");
        _names.Add("Анна");
        _names.Add("Мирослава");
        _names.Add("Кирилл");
        _names.Add("Анна");
        _names.Add("Маргарита");
        _names.Add("Анастасия");

        CreatePlayers(10);
    }

    public void Work()
    {
        int countOfTopPlayers = 3;

        Console.WriteLine("Список игроков:");
        ShowPlayers(_players);

        Console.WriteLine($"\nТоп {countOfTopPlayers} игроков по уровню:");
        ShowPlayers(GetTopPlayerByLevel(countOfTopPlayers));

        Console.WriteLine($"\nТоп {countOfTopPlayers} игроков по силе:");
        ShowPlayers(GetTopPlayerByStrength(countOfTopPlayers));
    }

    private List<Player> GetTopPlayerByLevel(int countOfTopPlayers)
    {
        return _players.OrderByDescending(player => player.Level).Take(countOfTopPlayers).ToList();
    }

    private List<Player> GetTopPlayerByStrength(int countOfTopPlayers)
    {
        return _players.OrderByDescending(player => player.Strength).Take(countOfTopPlayers).ToList();
    }

    private void ShowPlayers(List<Player> players)
    {
        Console.WriteLine();

        foreach (Player player in players)
        {
            player.ShowInfo();
        }
    }

    private void CreatePlayers(int count)
    {
        int maxLevel = 100;
        int maxStrength = 100;

        for (int i = 0; i < count; i++)
        {
            string name = _names[GetRandomNumber(_names.Count)];
            int level = GetRandomNumber(maxLevel);
            int strength = GetRandomNumber(maxStrength);

            Player player = new Player(name, level, strength);
            _players.Add(player);
        }
    }
}

class Player
{
    public Player(string name, int level, int strength)
    {
        Name = name;
        Level = level;
        Strength = strength;
    }

    public string Name { get; private set; }
    public int Level { get; private set; }
    public int Strength { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine("{0} | Уровень: {1} | Сила: {2}", Name, Level, Strength);
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