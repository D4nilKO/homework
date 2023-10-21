using System;
using System.Collections.Generic;

namespace homework.OOP.Store;

internal static class Program
{
    public static void Main1(string[] args)
    {
        Shop shop = new();
        shop.Work();
    }
}

class Shop
{
    public void Work()
    {
        Seller seller = new Seller(0);
        Player player = new Player(40, "Василий");

        const string CommandViewSellerItems = "1";
        const string CommandViewPlayerItems = "2";
        const string CommandViewSellerBalance = "3";
        const string CommandViewPlayerBalance = "4";
        const string CommandBuyItem = "5";
        const string CommandExit = "Exit";

        Dictionary<string, string> actionsByCommand = new()
        {
            { CommandViewSellerItems, "Показать товары продавца" },
            { CommandViewPlayerItems, "Показать предметы игрока" },
            { CommandViewSellerBalance, "Показать баланс продавца" },
            { CommandViewPlayerBalance, "Показать баланс игрока" },
            { CommandBuyItem, "Купить предмет" },
            { CommandExit, "Выйти из программы" }
        };

        bool isContinue = true;

        while (isContinue)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\nМеню:");
            Console.WriteLine("Здесь Команды | Здесь Что делают команды");
            Console.WriteLine();

            foreach (KeyValuePair<string, string> option in actionsByCommand)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }

            Console.Write($"\n{player.Name}, Скорее выберете необходимую операцию, пирожки стынут: ");
            string desiredOperation = Console.ReadLine();
            Console.WriteLine();

            switch (desiredOperation)
            {
                case CommandViewSellerItems:
                    seller.ViewAllItems();
                    break;

                case CommandViewPlayerItems:
                    player.ViewAllItems();
                    break;

                case CommandViewSellerBalance:
                    seller.ViewMoney();
                    break;

                case CommandViewPlayerBalance:
                    player.ViewMoney();
                    break;

                case CommandBuyItem:
                    TransferItem(seller, player);
                    break;

                case CommandExit:
                    isContinue = false;
                    Console.WriteLine("Выход...");
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Повторите ввод.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения... ");
            Console.ReadKey();
        }
    }

    private void TransferItem(Seller seller, Player player)
    {
        seller.ViewAllItems();

        Console.WriteLine("Введите ID прдемета, который вы хотите купить");
        string inputIdentifier = Console.ReadLine();

        if (seller.TryGetItem(inputIdentifier, out Item item))
        {
            if (player.CanPay(item.Price))
            {
                player.BuyItem(item);
                seller.SellItem(item);

                Console.WriteLine($"Вы успешно купили {item.Name} за {item.Price} шекелей");
            }
        }
    }
}

abstract class Entity
{
    protected List<Item> Items = new();

    protected Entity(uint money)
    {
        Money = money;
    }

    protected uint Money { get; set; }

    public void ViewAllItems()
    {
        foreach (Item product in Items)
        {
            product.View();
        }
    }

    public void ViewMoney()
    {
        Console.WriteLine($"Деньги {GetType().Name} = {Money}");
    }
}

class Seller : Entity
{
    public Seller(uint money) : base(money)
    {
        Items.Add(new Item("Пирожок без всего", 10));
        Items.Add(new Item("Пирожок с картошкой", 15));
        Items.Add(new Item("Пирожок с луком и яйцом", 20));
        Items.Add(new Item("Пирожок с мясом и луком", 45));
    }

    public void SellItem(Item item)
    {
        Items.Remove(item);
        Money += item.Price;
    }

    public bool TryGetItem(string identifier, out Item item)
    {
        bool isFound = false;

        item = null;

        foreach (Item element in Items)
        {
            if (element.Identifier.ToString() == identifier)
            {
                item = element;
                isFound = true;
            }
        }

        if (item == null)
        {
            Console.WriteLine("Такого товара нет, попробуйте еще раз.");
        }

        return isFound;
    }
}

class Player : Entity
{
    public Player(uint money, string name) : base(money)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public void BuyItem(Item item)
    {
        Money -= item.Price;
        Items.Add(item);
    }

    public bool CanPay(uint price)
    {
        if (Money >= price)
        {
            return true;
        }

        Console.WriteLine("У игрока недостаточно денег.");
        return false;
    }
}

class Item
{
    public Item(string name, uint price)
    {
        Name = name;
        Price = price;

        Identifier = Guid.NewGuid();
    }

    public string Name { get; private set; }
    public uint Price { get; private set; }
    public Guid Identifier { get; private set; }

    public void View()
    {
        Console.WriteLine($"{Name} - цена: {Price} рублей | Id: {Identifier}");
    }
}