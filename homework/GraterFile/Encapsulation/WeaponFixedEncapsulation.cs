using System;
using System.Diagnostics;

namespace homework.GraterFile.Encapsulation.Fixed;

class Weapon
{
    private readonly int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
        //бросать исключения в блоке else 
        //плохая практика, сначала нужно все проверить и если что не так - бросить исключение
        
        bool isDamageValid = damage > 0;
        bool isBulletsValid = bullets > 0;

        if (isDamageValid == false)
            throw new ArgumentException("Damage can't be negative");

        if (isBulletsValid == false)
            throw new ArgumentException("Bullets can't be negative");

        _damage = damage;
        _bullets = bullets;
    }

    public void Fire(Player player)
    {
        bool isPlayerValid = player != null;
        bool isBulletsValid = _bullets > 0;
        
        if (isPlayerValid == false)
            throw new ArgumentException("Player can't be null");
        
        if (isBulletsValid == false)
            throw new ArgumentException("Bullets can't be negative");

        if (player.TryApplyDamage(_damage))
        {
            _bullets--;
        }
    }
}

class Player
{
    private int _health;
    private bool _isDead;

    public Player(int health, int maxHealth)
    {
        //бросать исключения в блоке else 
        //плохая практика, сначала нужно все проверить и если что не так - бросить исключение
        if (health > 0)
            Health = health;
        else
            throw new ArgumentException("Health can't be negative");

        if (maxHealth > 0)
            MaxHealth = maxHealth;
        else
            throw new ArgumentException("MaxHealth can't be negative");
    }

    public int Health
    {
        get => _health;
        private set => _health = UserUtils.Clamp(value, 0, MaxHealth);
    }

    private int MaxHealth { get; }

    public bool TryApplyDamage(int damage)
    {
        if (damage <= 0)
        {
            throw new ArgumentException("Damage can't be negative");
        }

        _health -= damage;

        if (_health <= 0)
        {
            _isDead = true;
        }

        return true;
    }
}

class Bot
{
    private readonly Weapon _weapon;

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
        if (player == null)
            throw new ArgumentException("Player can't be null");

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