using System;

namespace homework.OOP;

internal static class PlayerProgram
{
    public static void Main1(string[] args)
    {
        Player1 player1 = new("Иван", 100, 15);
        Player1 player2 = new("GeNa", 90, 20);

        player1.ShowInfo();
        player2.ShowInfo();
    }
}

internal class Player1
{
    private string _name;
    private int _health;
    private int _damage;

    public Player1(string name, int health, int damage)
    {
        _name = name;
        _health = health;
        _damage = damage;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Я - игрок, меня зовут {_name}, у меня {_health} здоровья и {_damage} урона.");
    }
}