﻿// Боты ходят по карте и когда видят игрока стреляют в него.
// Систему обнаружения игрока оставим за кадром и ограничимся публичным
// методом OnSeePlayer который условно кто-то будет вызывать.
// Разберитесь с инкапсуляцией в этом коде -

namespace homework.GraterFile.Encapsulation;

class Weapon
{
    public int Damage;
    public int Bullets;

    public void Fire(Player player)
    {
        player.Health -= Damage;
        Bullets -= 1;
    }
}

class Player
{
    public int Health;
}

class Bot
{
    public Weapon Weapon;

    public void OnSeePlayer(Player player)
    {
        Weapon.Fire(player);
    }
}