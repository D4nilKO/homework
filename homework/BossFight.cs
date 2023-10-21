using System;

namespace homework;

internal static class BossFight
{
    public static void Main1(string[] args)
    {
        const int CommandSummonShadowSpirit = 1;
        const int CommandHuganzakura = 2;
        const int CommandInterdimensionalRift = 3;
        const int CommandBackStab = 4;
        const int CommandSurrender = 5;

        int summonShadowSpiritDamage = 100;
        int shadowSpiritDamage = 100;
        int interdimensionalRiftHeal = 250;
        int backStabDamage = 75;

        string menuTextMain = "Меню заклинаний:";

        string menuTextSummonShadowSpirit =
            $"{CommandSummonShadowSpirit} - Призыв теневого духа (наносит {summonShadowSpiritDamage} урона игроку)";

        string menuTextHuganzakura =
            $"{CommandHuganzakura} - Хуганзакура (Только после призыва теневого духа, наносит {shadowSpiritDamage} урона)";

        string menuTextInterdimensionalRift =
            $"{CommandInterdimensionalRift} - Межпространственный разлом (Восстановление {interdimensionalRiftHeal} здоровья, урон игнорируется)";

        string menuTextBackStab =
            $"{CommandBackStab} - Удар в спину (Наносит {backStabDamage} урона боссу и восстанавливает игроку cnстолько же здоровья)";

        string menuTextSurrender = $"{CommandSurrender} - Сдаться\n";

        string greetingText1 = "Добро пожаловать в игру!";
        string greetingText2 = "Вы – теневой маг, и ваша цель - уничтожить босса.";
        string greetingText3 = "У вас есть заклинания для нанесения урона боссу.";
        string greetingTextLast = "Удачи!\n";

        string errorText = "Неверный выбор заклинания.";
        string textChoice = "Ваш выбор: ";

        string drawText = "Произошла ничья.";
        string playerLostText = "Вы погибли. Игра окончена :( ";
        string bossWonText = "Босс побежден! Поздравляем!";

        string summonShadowSpiritText = "Вы использовали заклинание Рашамон.";
        string huganzakuraText = "Вы использовали заклинание Хуганзакура.";
        string requireShadowSpiritText = "Требуется призыв теневого духа.";
        string interdimensionalRiftText = "Вы использовали заклинание Межпространственный разлом.";
        string backStabText = "Вы использовали заклинание Удар в спину.";
        string surrenderText = "Вы сдались.";
        string bossAttackText = "Босс атаковал вас и нанес";

        bool isShadowSpiritSummoned = false;
        bool isInInterdimensionalRift = false;

        int startPlayerHealth = 500;
        int playerHealth = startPlayerHealth;

        int startBossHealth = 1000;
        int bossHealth = startBossHealth;

        Console.WriteLine(greetingText1);
        Console.WriteLine(greetingText2);
        Console.WriteLine(greetingText3);
        Console.WriteLine(greetingTextLast);

        while (playerHealth > 0 && bossHealth > 0)
        {
            Console.WriteLine($"Здоровье игрока: {playerHealth}");
            Console.WriteLine($"Здоровье босса: {bossHealth}\n");

            Console.WriteLine(menuTextMain);
            Console.WriteLine(menuTextSummonShadowSpirit);
            Console.WriteLine(menuTextHuganzakura);
            Console.WriteLine(menuTextInterdimensionalRift);
            Console.WriteLine(menuTextBackStab);
            Console.WriteLine(menuTextSurrender);

            Console.Write(textChoice);
            int spellChoice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            switch (spellChoice)
            {
                case CommandSummonShadowSpirit:
                    Console.WriteLine(summonShadowSpiritText);

                    playerHealth -= summonShadowSpiritDamage;
                    isShadowSpiritSummoned = true;
                    break;

                case CommandHuganzakura:
                    if (isShadowSpiritSummoned)
                    {
                        Console.WriteLine(huganzakuraText);

                        bossHealth -= shadowSpiritDamage;
                        isShadowSpiritSummoned = false;
                    }
                    else
                    {
                        Console.WriteLine(requireShadowSpiritText);
                    }

                    break;

                case CommandInterdimensionalRift:
                    Console.WriteLine(interdimensionalRiftText);

                    playerHealth = Math.Min(playerHealth + interdimensionalRiftHeal, startPlayerHealth);
                    isInInterdimensionalRift = true;
                    break;

                case CommandBackStab:
                    Console.WriteLine(backStabText);

                    int damage = 75;

                    bossHealth -= damage;
                    playerHealth += damage;
                    break;

                case CommandSurrender:
                    Console.WriteLine(surrenderText);

                    playerHealth = 0;
                    break;

                default:
                    Console.WriteLine(errorText);
                    break;
            }

            Console.WriteLine();

            if (bossHealth > 0)
            {
                int minBossDamage = 50;
                int maxBossDamage = 150;

                int bossDamage = new Random().Next(minBossDamage, maxBossDamage);

                if (isInInterdimensionalRift == false)
                {
                    playerHealth -= bossDamage;
                }

                Console.WriteLine($"{bossAttackText} {bossDamage} урона.");

                isInInterdimensionalRift = false;
            }
        }

        if (playerHealth <= 0 && bossHealth <= 0)
        {
            Console.WriteLine(drawText);
        }
        else if (playerHealth <= 0)
        {
            Console.WriteLine(playerLostText);
        }
        else
        {
            Console.WriteLine(bossWonText);
        }
    }
}