using System.Diagnostics;

namespace homework.GraterFile.Encapsulation.Fixed;

class Weapon
{
    private int _damage;
    private int _bullets;

    public void Fire(Player player)
    {
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

    private void Die()
    {
        // do something
    }
}

class Bot
{
    private Weapon _weapon;

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}