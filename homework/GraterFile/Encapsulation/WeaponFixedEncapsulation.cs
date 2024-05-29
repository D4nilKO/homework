using System;
using System.Diagnostics;

namespace homework.GraterFile.Encapsulation.Fixed;

class Weapon
{
    public Weapon(int damage, int bullets)
    {
        if (damage >= 0)
            _damage = damage;
        else
            throw new ArgumentException("Damage can't be negative");

        if (_bullets >= 0)
            _bullets = bullets;
        else
            throw new ArgumentException("Bullets can't be negative");
    }

    private int _damage;
    private int _bullets;

    public void Fire(Player player)
    {
        if (player == null)
        {
            Debug.WriteLine("Player can't be null");
            return;
        }

        if (_bullets <= 0)
        {
            Debug.WriteLine("No more bullets, need to reload!");
            return;
        }

        if (player.TryApplyDamage(_damage))
        {
            _bullets -= 1;
        }
    }
}

class Player
{
    private int _health;

    public Player(int health, int maxHealth)
    {
        if (health > 0)
            Health = health;
        else
            throw new ArgumentException("Health can't be negative");

        if (maxHealth > 0)
            MaxHealth = maxHealth;
        else
            throw new ArgumentException("MaxHealth can't be negative");
    }

    private int MaxHealth { get; }

    public int Health
    {
        get => _health;
        private set => _health = UserUtils.Clamp(value, 0, MaxHealth);
    }

    private void Die()
    {
        // do something
    }

    public bool TryApplyDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.WriteLine("Damage can't be negative");
            return false;
        }

        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }

        return true;
    }
}

class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon)
    {
        if (weapon != null)
        {
            _weapon = weapon;
        }
        else
        {
            throw new ArgumentException("Weapon can't be null");
        }
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}

internal static class UserUtils
{
    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
            value = min;

        if (value > max)
            value = max;

        return value;
    }
}