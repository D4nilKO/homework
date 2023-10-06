using System;
using System.Collections.Generic;

namespace homework.OOP.Store
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Seller seller = new Seller(0);
            Player player = new Player(40, "John");

            Console.WriteLine("Доступные товары в магазине:");
            seller.ViewAllItems();

            Console.WriteLine("Введите ID товара: ");
            string inputIdentifier = Console.ReadLine();

            TransferItem(inputIdentifier, player, seller);

            Console.WriteLine();

            Console.WriteLine("Ваши вещи: ");
            player.ViewAllItems();
            player.ViewMoney();

            Console.WriteLine();

            Console.WriteLine("Вещи магазина: ");
            seller.ViewAllItems();
            seller.ViewMoney();
        }

        private static void TransferItem(string identifier, Player player, Seller seller)
        {
            if (seller.TryGetItem(identifier, out Item item))
            {
                if (player.CanPay(item))
                {
                    player.BuyItem(item);
                    seller.SellItem(item);
                }
            }
        }
    }

    abstract class Entity
    {
        protected Entity(uint money)
        {
            Money = money;
        }

        public uint Money { get; protected set; }

        protected List<Item> Items = new();

        public void ViewAllItems()
        {
            foreach (var product in Items)
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
            Items.Add(new Item("Item1", 10));
            Items.Add(new Item("Item2", 15));
            Items.Add(new Item("Item3", 20));
            Items.Add(new Item("Item4", 45));
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

            foreach (var element in Items)
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

        public bool CanPay(Item item)
        {
            if (Money > item.Price)
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
}