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
            seller.ViewAll();

            seller.SellItem(player);
            
            Console.WriteLine();

            Console.WriteLine("Ваши вещи: ");
            player.ViewAll();
            player.ViewMoney();

            Console.WriteLine();

            Console.WriteLine("Вещи магазина: ");
            seller.ViewAll();
            seller.ViewMoney();
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

        public void ViewAll()
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
        }

        public void SellItem(Player player)
        {
            Console.WriteLine("Вы хотите купить предмет, введите его ID: ");
            Item item = FindItemByIdentifier();

            if (player.Buy(item))
            {
                Items.Remove(item);
                TransferMoneyForItem(item);
            }
        }

        private void TransferMoneyForItem(Item item)
        {
            Money += item.Price;
        }

        private Item FindItemByIdentifier()
        {
            bool isContinue = true;

            Item item = null;

            while (isContinue)
            {
                string input = Console.ReadLine();

                if (TryGetItemByIdentifier(input, out Item foundedItem))
                {
                    isContinue = false;
                    item = foundedItem;
                }
                else
                {
                    Console.WriteLine("Такого товара нет, попробуйте еще раз.");
                }
            }

            return item;
        }

        private bool TryGetItemByIdentifier(string identifier, out Item item)
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

        public bool Buy(Item item)
        {
            bool isPurchased = false;

            if (Money >= item.Price)
            {
                SpendMoney(item);
                Items.Add(item);

                isPurchased = true;
            }
            else
            {
                Console.WriteLine("У вас недостаточно денег");
            }

            return isPurchased;
        }

        private void SpendMoney(Item item)
        {
            Money -= item.Price;
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