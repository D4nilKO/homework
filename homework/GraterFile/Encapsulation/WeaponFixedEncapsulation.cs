﻿using System;

namespace homework.GraterFile.Encapsulation.Fixed;

class Weapon
{
    private readonly int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
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
        bool isHealthValid = health > 0;
        bool isMaxHealthValid = maxHealth > 0;

        if (isHealthValid == false)
            throw new ArgumentException("Health can't be negative");
        
        if (isMaxHealthValid == false)
            throw new ArgumentException("MaxHealth can't be negative");

        MaxHealth = maxHealth;
        Health = health;
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