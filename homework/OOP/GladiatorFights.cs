using System;
using System.Collections.Generic;

namespace homework.OOP.GladiatorFights
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new Arena().Work();
        }
    }

    class Arena
    {
        private const int CommandWizard = 1;
        private const int CommandBerserk = 2;
        private const int CommandPaladin = 3;
        private const int CommandDruid = 4;
        private const int CommandWarrior = 5;

        private Fighter _fighter1;
        private Fighter _fighter2;

        private Dictionary<int, string> _actionsByCommand = new()
        {
            { CommandWizard, "Выбрать Wizard" },
            { CommandBerserk, "Выбрать Berserk" },
            { CommandPaladin, "Выбрать Paladin" },
            { CommandDruid, "Выбрать Druid" },
            { CommandWarrior, "Выбрать Warrior" },
        };

        private List<Fighter> _fighters = new()
        {
            new Wizard(),
            new Berserk(),
            new Paladin(),
            new Druid(),
            new Warrior()
        };

        public void Work()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\nМеню:");

            foreach (KeyValuePair<int, string> option in _actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write("\nВыберете первого бойца, ");
            _fighter1 = GetFighter();

            Console.Write("\nВыберете второго бойца, ");
            _fighter2 = GetFighter();

            Fight(_fighter1, _fighter2);
        }

        private Fighter GetFighter()
        {
            bool success = false;

            int numberOfFighter = -1;

            while (success == false)
            {
                numberOfFighter = ReadInt() - 1;

                if ((numberOfFighter >= 0) && (numberOfFighter < _fighters.Count))
                {
                    success = true;
                }
                else
                {
                    Console.WriteLine("Такого бойца нет.");
                }
            }

            Fighter fighter = _fighters[numberOfFighter].Clone();

            Console.WriteLine($"Вы выбрали персонажа {fighter.Name}");

            return fighter;
        }

        private int ReadInt()
        {
            Console.WriteLine("Введите число: ");

            bool success = false;
            int number = 0;

            while (success == false)
            {
                string message = Console.ReadLine();

                success = int.TryParse(message, out number);
            }

            return number;
        }

        private void Fight(Fighter fighter1, Fighter fighter2)
        {
            int turnNumber = 1;

            if (fighter1.GetType() == fighter2.GetType())
            {
                fighter1.Name = $"{fighter1.Name} 1";
                fighter2.Name = $"{fighter2.Name} 2";
            }

            while (fighter1.Health.IsAlive && fighter2.Health.IsAlive)
            {
                Console.WriteLine();
                Console.WriteLine($"Сейчас ход №{turnNumber}.");

                fighter1.Attack(fighter2);
                fighter2.ShowInfo();

                fighter2.Attack(fighter1);
                fighter1.ShowInfo();

                turnNumber++;

                Console.WriteLine("Нажмите любую клавишу для продолжения... ");
                Console.ReadKey();
            }

            Fighter winner = fighter2.Health.IsAlive
                ? fighter2
                : fighter1;

            Console.WriteLine($"Персонаж {winner.Name} выиграл!");
        }
    }

    abstract class Fighter
    {
        public Health Health = new();

        protected Fighter()
        {
            Name = GetType().Name;
        }

        public string Name { get; set; }
        protected float Damage { get; set; }
        
        public abstract Fighter Clone();

        public void ShowInfo()
        {
            Console.WriteLine($"\nПерсонаж {Name} имеет {Health.CurrentHealth} из {Health.MaxHealth} здоровья");
            Console.WriteLine($"Атака {Name} = {Damage}");
        }

        public virtual void Attack(Fighter target)
        {
            if (Health.IsAlive == false)
                return;

            target.Health.ApplyDamage(Damage);
            UseAbility(target);
        }

        protected abstract void UseAbility(Fighter target);
    }

    class Wizard : Fighter
    {
        private float _mana;
        private float _fireballDamage;
        private float _fireballManaCost;

        public Wizard()
        {
            Damage = 1;
            Health.SetMaxHealth(40);

            _mana = 100;
            _fireballDamage = 20;
            _fireballManaCost = 15;
        }

        public override void Attack(Fighter target)
        {
            if (EnoughMana(_fireballManaCost))
            {
                UseAbility(target);
                return;
            }

            target.Health.ApplyDamage(Damage);
        }

        public override Fighter Clone()
        {
            return new Wizard();
        }

        protected override void UseAbility(Fighter target)
        {
            target.Health.ApplyDamage(_fireballDamage);
        }

        private bool EnoughMana(float manaCost)
        {
            if (_mana >= manaCost)
            {
                _mana -= manaCost;
                return true;
            }

            return false;
        }
    }

    class Berserk : Fighter
    {
        private bool _damageIncreased;
        private float _extraDamage;
        private int _dividerBerserkMode;
        private float _berserkModeHealthLevel;

        public Berserk()
        {
            Damage = 10;
            Health.SetMaxHealth(80);

            _damageIncreased = false;
            _extraDamage = 15;
            _dividerBerserkMode = 2;
            _berserkModeHealthLevel = Health.MaxHealth / _dividerBerserkMode;
        }

        protected override void UseAbility(Fighter target)
        {
            if ((_damageIncreased == false) && (Health.CurrentHealth <= _berserkModeHealthLevel))
            {
                Damage += _extraDamage;
                _damageIncreased = true;
            }
        }

        public override Fighter Clone()
        {
            return new Berserk();
        }
    }

    class Paladin : Fighter
    {
        private float _healValue;
        private int _abilityCharges;

        public Paladin()
        {
            Damage = 8;
            Health.SetMaxHealth(70);

            _healValue = 10;
            _abilityCharges = 3;
        }

        public override Fighter Clone()
        {
            return new Paladin();
        }

        protected override void UseAbility(Fighter target)
        {
            if ((Health.CurrentHealth <= Health.MaxHealth - _healValue) && (_abilityCharges > 0))
            {
                Health.Heal(_healValue);
                _abilityCharges--;
            }
        }
    }

    class Druid : Fighter
    {
        private int AttackCountForUseAbility { get; }
        private int _attackNumber;

        public Druid()
        {
            Damage = 9;
            Health.SetMaxHealth(80);

            AttackCountForUseAbility = 3;
            _attackNumber = 0;
        }

        public override void Attack(Fighter target)
        {
            base.Attack(target);
            _attackNumber++;
        }

        public override Fighter Clone()
        {
            return new Druid();
        }

        protected override void UseAbility(Fighter target)
        {
            if (_attackNumber % AttackCountForUseAbility == 0)
            {
                base.Attack(target);
            }
        }
    }

    class Warrior : Fighter
    {
        private int _counterHitsOnMe;
        private int _riposteCount;

        public Warrior()
        {
            Damage = 10;
            Health.SetMaxHealth(100);

            Health.DamageMultiplier = 0.8f;
            Health.OnDamageApplied += IncreaseCounterHitsOnMe;
        }

        ~Warrior()
        {
            Health.OnDamageApplied -= IncreaseCounterHitsOnMe;
        }

        public override Fighter Clone()
        {
            return new Warrior();
        }

        protected override void UseAbility(Fighter target)
        {
            if (_counterHitsOnMe >= 4)
            {
                _counterHitsOnMe -= 4;
                _riposteCount++;
            }

            TryRiposte(target);
        }

        private void IncreaseCounterHitsOnMe()
        {
            _counterHitsOnMe++;
        }

        private void TryRiposte(Fighter target)
        {
            if (_riposteCount > 0)
            {
                Attack(target);
                _riposteCount--;
            }
        }
    }

    class Health
    {
        private float _currentHealth;
        private float _damageMultiplier = 1;

        public Health()
        {
            UpdateHealthToMax();
        }

        public event Action OnDamageApplied;

        public float MaxHealth { get; private set; }

        public float DamageMultiplier
        {
            get => _damageMultiplier;
            set => _damageMultiplier = Math.Max(0, value);
        }
        
        public bool IsAlive => CurrentHealth > 0;

        public float CurrentHealth
        {
            get => _currentHealth;
            private set => _currentHealth = Clamp(value, 0f, MaxHealth);
        }

        public void Heal(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            CurrentHealth += value;
        }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
                return;

            float totalDamage = ProcessDamage(damage);

            if (totalDamage < 0)
                throw new NullReferenceException(nameof(totalDamage));

            CurrentHealth -= totalDamage;

            OnDamageApplied?.Invoke();
        }

        public void SetMaxHealth(float maxHealth)
        {
            if (maxHealth <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxHealth));

            MaxHealth = maxHealth;

            UpdateHealthToMax();
        }

        private float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        private void UpdateHealthToMax()
        {
            CurrentHealth = MaxHealth;
        }

        private float ProcessDamage(float damage) => damage * DamageMultiplier;
    }
}