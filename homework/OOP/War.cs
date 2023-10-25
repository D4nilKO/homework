using System;
using System.Collections.Generic;
using static homework.UserUtils;

namespace homework.OOP.War;

internal static class Program
{
    public static void Main1(string[] args)
    {
        new Battlefield(4).Work();
    }
}

abstract class Soldier
{
    private int _percentConverter = 100;
    private float _damageResistance;

    protected Soldier(string name, float health, float armor, float damage, float hitAccuracyPercent,
        float blockChancePercent)
    {
        Name = name;
        Health = health;
        Damage = damage;
        Armor = armor;
        HitAccuracyPercent = hitAccuracyPercent;
        BlockChancePercent = blockChancePercent;
    }

    public float Health { get; protected set; }
    public string Name { get; protected set; }
    public float Damage { get; protected set; }
    public float Armor { get; protected set; }
    public float HitAccuracyPercent { get; protected set; }
    public float BlockChancePercent { get; protected set; }

    public abstract Soldier Clone();

    public virtual void Attack(List<Soldier> enemies)
    {
        int minEvasionChance = 1;
        int maxEvasionChance = 100;

        float evasionChance = GetRandomNumber(minEvasionChance, maxEvasionChance + 1);

        Soldier enemy = enemies[GetRandomNumber(enemies.Count)];

        float chanceAttack = ((HitAccuracyPercent - enemy.BlockChancePercent) / HitAccuracyPercent) *
                             _percentConverter;

        Console.Write($"{Name} - ");

        if (chanceAttack > evasionChance)
        {
            enemy.TakeDamage(Damage);
            Console.WriteLine("Попал!");
            return;
        }

        Console.WriteLine("Мимо!");
    }

    private void TakeDamage(float damage)
    {
        _damageResistance = Armor / _percentConverter;
        float incomingDamageResistanceMultiplier = 1 - _damageResistance;
        Health -= damage * incomingDamageResistanceMultiplier;
    }
}

class Sniper : Soldier
{
    public Sniper() : base("Снайпер", 900, 5, 750, 99, 10)
    {
    }

    public override Soldier Clone()
    {
        return new Sniper();
    }
}

class GrenadeLauncher : Soldier
{
    public GrenadeLauncher() : base("Гранатометчик", 1500, 15, 200, 75, 20)
    {
    }

    public override Soldier Clone()
    {
        return new GrenadeLauncher();
    }

    public override void Attack(List<Soldier> enemy)
    {
        int numberGoals = 5;

        for (int i = 0; i < numberGoals; i++)
        {
            base.Attack(enemy);
        }
    }
}

class Squad
{
    private List<Soldier> _soldiers = new();

    public Squad(string name, int soldiersCount)
    {
        Name = name;
        CreateFighters(soldiersCount);
    }

    public string Name { get; private set; }
    public int CountOfSoldiers => _soldiers.Count;

    public void AttackSquad(Squad targetSquad)
    {
        foreach (Soldier soldier in _soldiers)
        {
            soldier.Attack(targetSquad._soldiers);
        }
    }

    public void ShowInfo()
    {
        if (_soldiers.Count == 0)
        {
            return;
        }

        Console.WriteLine("\n**** " + new string('-', 30) + " ****\n");

        foreach (Soldier soldier in _soldiers)
        {
            Console.WriteLine(
                $"Взвод: {Name}, Боец - {soldier.Name}, здоровье: {soldier.Health}, наносимый урон: {soldier.Damage}, " +
                $"броня: {soldier.Armor}, шанс попадания: {soldier.HitAccuracyPercent}%, шанс заблокировать урон: {soldier.BlockChancePercent}%.");
        }

        Console.WriteLine("\n**** " + new string('-', 30) + " ****\n");
    }

    public void RemoveDead()
    {
        for (int i = _soldiers.Count - 1; i >= 0; i--)
        {
            Soldier soldier = _soldiers[i];
            
            if (soldier.Health <= 0)
            {
                Console.WriteLine($"{soldier.Name} Погиб с честью!");
                _soldiers.Remove(soldier);
            }
        }
    }

    private void CreateFighters(int soldiersCount)
    {
        List<Soldier> soldiers = new List<Soldier>
        {
            new Sniper(),
            new GrenadeLauncher(),
        };

        for (int i = 0; i < soldiersCount; i++)
        {
            _soldiers.Add(soldiers[GetRandomNumber(soldiers.Count)].Clone());
        }
    }
}

class Battlefield
{
    private Squad _firstSquad;
    private Squad _secondSquad;

    public Battlefield(int squadCount)
    {
        _firstSquad = new Squad("Первый", squadCount);
        _secondSquad = new Squad("Второй", squadCount);
    }

    public void Work()
    {
        Battle();
        ShowWinner();
    }

    private void Battle()
    {
        _firstSquad.ShowInfo();
        _secondSquad.ShowInfo();

        while (_firstSquad.CountOfSoldiers > 0 && _secondSquad.CountOfSoldiers > 0)
        {
            _secondSquad.AttackSquad(_firstSquad);
            _firstSquad.AttackSquad(_secondSquad);

            _firstSquad.RemoveDead();
            _secondSquad.RemoveDead();

            _firstSquad.ShowInfo();
            _secondSquad.ShowInfo();
        }
    }

    private void ShowWinner()
    {
        switch (_firstSquad.CountOfSoldiers)
        {
            case 0 when _secondSquad.CountOfSoldiers == 0:
                Console.WriteLine("Все погибли... ");
                return;
            case 0:
                Console.WriteLine($"Второй взвод победил!");
                return;
            default:
                Console.WriteLine($"Первый взвод победил!");
                break;
        }
    }
}