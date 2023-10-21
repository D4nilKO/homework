using System;

namespace homework;

internal static class CurrencyConverter
{
    public static void Main1(string[] args)
    {
        const string CommandRubToUsd = "1";
        const string CommandRubToEur = "2";
        const string CommandEurToRub = "3";
        const string CommandEurToUsd = "4";
        const string CommandUsdToRub = "5";
        const string CommandUsdToEur = "6";
        const string CommandExit = "exit";
            
        string menuText1 = $"{CommandRubToUsd} - обменять рубли на доллары: ";
        string menuText2 = $"{CommandRubToEur} - обменять рубли на евро: ";
        string menuText3 = $"{CommandEurToRub} - обменять евро на рубли: ";
        string menuText4 = $"{CommandEurToUsd} - обменять евро на доллары: ";
        string menuText5 = $"{CommandUsdToRub} - обменять доллары на рубли: ";
        string menuText6 = $"{CommandUsdToEur} - обменять доллары на евро: ";
        string menuTextExit = $"{CommandExit} - выйти из программы";
            
        float margin = 1.02f;

        float rubToUsd = 80;
        float usdToRub = GetReciprocalRate(rubToUsd, margin);

        float rubToEur = 90f;
        float eurToRub = GetReciprocalRate(rubToEur, margin);

        float eurToUsd = 1.09f;
        float usdToEur = GetReciprocalRate(eurToUsd, margin);

        string desiredOperation = "";

        Console.WriteLine("Добро пожаловать в обменник валют!");

        Console.Write("Введите баланс Долларов: ");
        float dollarsInWallet = Convert.ToSingle(Console.ReadLine());

        Console.Write("Введите баланс Рублей: ");
        float rublesInWallet = Convert.ToSingle(Console.ReadLine());

        Console.Write("Введите баланс Евро: ");
        float eurosInWallet = Convert.ToSingle(Console.ReadLine());

        while (desiredOperation != CommandExit)
        {
            Console.WriteLine("\nВыберете необходимую операцию: ");
                
            Console.WriteLine(menuText1);
            Console.WriteLine(menuText2);
            Console.WriteLine(menuText3);
            Console.WriteLine(menuText4);
            Console.WriteLine(menuText5);
            Console.WriteLine(menuText6);
            Console.WriteLine(menuTextExit);
                
            Console.Write("\nВведите команду: ");
            desiredOperation = Console.ReadLine()?.ToLower();

            switch (desiredOperation)
            {
                case CommandRubToUsd:
                    Console.WriteLine("Обмен рублей на доллары.");
                    Exchange(ref rublesInWallet, ref dollarsInWallet, rubToUsd);
                    break;
                    
                case CommandRubToEur:
                    Console.WriteLine("Обмен рублей на евро");
                    Exchange(ref rublesInWallet, ref eurosInWallet, rubToEur);
                    break;
                    
                case CommandEurToRub:
                    Console.WriteLine("Обмен евро на рубли.");
                    Exchange(ref eurosInWallet, ref rublesInWallet, eurToRub);
                    break;
                    
                case CommandEurToUsd:
                    Console.WriteLine("Обмен евро на доллары.");
                    Exchange(ref eurosInWallet, ref dollarsInWallet, eurToUsd);
                    break;
                    
                case CommandUsdToRub:
                    Console.WriteLine("Обмен долларов на рубли.");
                    Exchange(ref dollarsInWallet, ref rublesInWallet, usdToRub);
                    break;
                    
                case CommandUsdToEur:
                    Console.WriteLine("Обмен долларов на евро.");
                    Exchange(ref dollarsInWallet, ref eurosInWallet, usdToEur);
                    break;
                    
                case CommandExit:
                    Console.WriteLine("Выход...");
                    break;
                    
                default:
                    Console.WriteLine("Выбрана неверная опция.");
                    break;
            }
        }

        void Exchange(ref float debitBalance, ref float depositBalance, float exchangeRate)
        {
            Console.Write("Сколько хотите обменять: ");
            float exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

            if (debitBalance >= exchangeCurrencyCount && exchangeCurrencyCount >= 0)
            {
                debitBalance -= exchangeCurrencyCount;
                depositBalance += exchangeCurrencyCount / exchangeRate;
            }
            else
            {
                Console.WriteLine("Вы ввели недопустимое количество валюты");
            }

            PrintBalance();
        }

        void PrintBalance()
        {
            Console.WriteLine("Ваш балланс:");
            Console.WriteLine($"{dollarsInWallet} USD");
            Console.WriteLine($"{rublesInWallet} RUB");
            Console.WriteLine($"{eurosInWallet} EUR\n");
        }

        float GetReciprocalRate(float rate, float _margin)
        {
            float result = 1 / (_margin * rate);
            return result;
        }
    }
}