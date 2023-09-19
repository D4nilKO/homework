using System;
using System.Collections.Generic;

namespace homework.OOP;

internal static class DatabaseProgram
{
    public static void Main1(string[] args)
    {
        const string CommandAddPlayer = "Add";
        const string CommandRemovePlayer = "Remove";
        const string CommandBan = "Ban";
        const string CommandUnban = "Unban";
        const string CommandViewAll = "View All";
        const string CommandExit = "Exit";

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandAddPlayer, "Добавить игрока" },
            { CommandRemovePlayer, "Удалить игрока" },
            { CommandBan, "Забанить игрока" },
            { CommandUnban, "Разбанить игрока" },
            { CommandViewAll, "Показать всех игроков" },
            { CommandExit, "Выйти из программ" }
        };

        Database database = new Database();

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

            Console.Write("Выберете необходимую операцию: ");
            string desiredOperation = Console.ReadLine();

            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandAddPlayer:
                    database.AddPlayer();
                    break;

                case CommandRemovePlayer:
                    database.RemovePlayer();
                    break;

                case CommandBan:
                    database.BanPlayer();
                    break;

                case CommandUnban:
                    database.UnbanPlayer();
                    break;

                case CommandViewAll:
                    database.ViewAllPlayers();
                    break;

                case CommandExit:
                    isContinue = false;
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Повторите ввод.");
                    break;
            }


            if (isContinue)
            {
                Console.Write("\nДля продолжения нажмите любую клавишу... ");
                Console.ReadKey();
            }
        }
    }
}

class Database
{
    private List<Player3> _players = new()
    {
        new Player3("Tom"),
        new Player3("Danil"),
        new Player3("Mark")
    };
    
    private string GetIdentifierFromInput()
    {
        ViewAllPlayers();

        Console.WriteLine("Введите ID пользователя.");
        string identifier = Console.ReadLine();

        return identifier;
    }

    public void AddPlayer()
    {
        Console.Write("Введите имя игрока: ");
        string name = Console.ReadLine();

        _players.Add(new Player3(name));
    }

    public void RemovePlayer()
    {
        if (TryGetPlayer(GetIdentifierFromInput(), out Player3 player))
        {
            _players.Remove(player);
            Console.WriteLine("Удаление успешно завершено!");
        }
        else
        {
            Console.WriteLine("Удаление не удалось. Проверьте правильность ввода ID.");
        }
    }

    public void BanPlayer()
    {
        if (TryGetPlayer(GetIdentifierFromInput(), out Player3 player))
        {
            player.Ban();
        }
    }

    public void UnbanPlayer()
    {
        if (TryGetPlayer(GetIdentifierFromInput(), out Player3 player))
        {
            player.Unban();
        }
    }

    public void ViewAllPlayers()
    {
        foreach (var player in _players)
        {
            Console.WriteLine(
                $"{player.Name} | Уровень: {player.CurrentLevel} | Бан статус: {player.IsBanned} | ID: {player.Identifier}");
        }
    }

    private bool TryGetPlayer(string identifier, out Player3 player)
    {
        player = default;

        foreach (var element in _players)
        {
            if (string.Equals(element.Identifier.ToString(), identifier, StringComparison.Ordinal))
            {
                player = element;
                return true;
            }
        }

        Console.WriteLine("Игрок с таким ID не найден.");

        return false;
    }
}

class Player3
{
    public Player3(string name)
    {
        Name = name;

        Identifier = Guid.NewGuid();

        CurrentLevel = 1;

        IsBanned = false;
    }

    public string Name { get; private set; }
    public Guid Identifier { get; private set; }
    public uint CurrentLevel { get; private set; }
    public bool IsBanned { get; private set; }

    public void Ban()
    {
        if (IsBanned == false)
        {
            IsBanned = true;
            Console.WriteLine("Пользователь успешно забанен");
        }
        else
        {
            Console.WriteLine("Пользователь уже забанен!");
        }
    }

    public void Unban()
    {
        if (IsBanned == true)
        {
            IsBanned = false;
            Console.WriteLine("Пользователь успешно разбанен");
        }
        else
        {
            Console.WriteLine("Пользователь еще не забанен!");
        }
    }
}