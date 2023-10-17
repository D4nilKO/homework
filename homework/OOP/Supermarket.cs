using System;
using System.Collections.Generic;

namespace homework.OOP.Supermarket
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new Supermarket().Work();
        }
    }

    class Supermarket
    {
        private Random _random = new();

        private List<Client> _clients = new();

        private List<Product> _allProducts = new()
        {
            new Product("Молоко", 10),
            new Product("Яйца", 50),
            new Product("Булочка с ничем", 9),
            new Product("Булочка с чем-то", 49),
            new Product("Творог", 70),
            new Product("Сок", 100),
            new Product("Филе курицы", 150),
            new Product("Хлеб", 15),
            new Product("Рис", 35)
        };

        private List<string> _names = new()
        {
            "Джон",
            "Борис Бритва",
            "Иван",
            "Виктор",
            "Данил",
            "Абдул",
            "Катерина",
            "Александр",
            "Евгений",
            "Дмитрий",
            "Даниил",
        };

        public Supermarket()
        {
            CreateClients();
        }

        private bool Empty => _clients.Count == 0;

        public void Work()
        {
            while (Empty == false)
            {
                Client client = GetFirstClient();

                client.ShowInfo();

                client.PayOff(_random);

                _clients.Remove(client);
            }
        }

        private Client GetFirstClient()
        {
            return _clients[0];
        }

        private void CreateClients()
        {
            int minRandomClientsCount = 5;
            int maxRandomClientsCount = 10;
            // int clientsCount = _random.Next(minRandomClientsCount, maxRandomClientsCount);
            int clientsCount = 2;

            for (int i = 0; i < clientsCount; i++)
            {
                string clientName = _names[_random.Next(0, _names.Count)];
                Client client = new(clientName, _random, _allProducts);

                _clients.Add(client);
            }
        }
    }

    class Client
    {
        private ShoppingCart _shoppingCart = new();

        public Client(string name, Random random, List<Product> allProducts)
        {
            Name = name;
            GainMoney(random);
            FillShoppingCart(allProducts);
        }

        public string Name { get; private set; }

        public int Money { get; private set; }

        public void PayOff(Random random)
        {
            while (Money < GetPurchaseAmount())
            {
                if (_shoppingCart.Empty)
                {
                    Console.WriteLine($"Корзина пуста, а я ничего не купил ");
                    return;
                }

                _shoppingCart.RemoveRandomProduct(random);
            }

            int productsCountToPurchase = _shoppingCart.Products.Count;

            for (int i = 0; i < productsCountToPurchase; i++)
            {
                Product product = _shoppingCart.Products[0];
                Buy(product);
            }

            Console.WriteLine("Я расплатился за товары в корзине!");
        }

        public void ShowInfo()
        {
            Console.Write("\n-----------------------------");
            Console.Write($"\nКлиент: {Name} - ");

            if (_shoppingCart.Empty)
            {
                Console.WriteLine("корзина пуста");
                return;
            }

            Console.WriteLine(" хранит в корзине предметы:\n");

            foreach (Product product in _shoppingCart.Products)
            {
                product.ShowInfo();
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine($"Сумма покупки: {GetPurchaseAmount()}");
            Console.WriteLine($"Денег в кошельке {Money}");

            Console.WriteLine();
        }

        private void Buy(Product product)
        {
            Money -= product.Price;
            _shoppingCart.RemoveProduct(product);

            Console.Write("Я купил ");
            product.ShowInfo();
            Console.WriteLine();
        }

        private int GetPurchaseAmount()
        {
            int sum = 0;

            foreach (Product product in _shoppingCart.Products)
            {
                sum += product.Price;
            }

            return sum;
        }

        private void GainMoney(Random random)
        {
            int minRandomMoneyCount = 200;
            int maxRandomMoneyCount = 500;

            Money = random.Next(minRandomMoneyCount, maxRandomMoneyCount);
        }

        private void FillShoppingCart(List<Product> products)
        {
            Random random = new();

            int minRandomProduct = 0;
            int maxRandomProduct = products.Count;

            int minProductsCount = 3;
            int maxProductsCount = 8;

            int productsCount = random.Next(minProductsCount, maxProductsCount+1);

            for (int i = 0; i < productsCount; i++)
            {
                int numberOfProduct = random.Next(minRandomProduct, maxRandomProduct);
                Product product = products[numberOfProduct];

                _shoppingCart.AddProduct(product);
            }
        }
    }

    class ShoppingCart
    {
        public List<Product> Products { get; private set; } = new();

        public bool Empty => Products.Count == 0;

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        public void RemoveRandomProduct(Random random)
        {
            Product product = Products[random.Next(0, Products.Count)];
            RemoveProduct(product);

            product.ShowInfo();
            Console.WriteLine(" удален из корзины.");
        }
    }

    class Product
    {
        private string _name;

        public Product(string name, int price)
        {
            _name = name;
            Price = price;
        }

        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{_name} - {Price} рублей.");
        }
    }
}