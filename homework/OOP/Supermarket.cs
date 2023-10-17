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
                client.PayOff();

                _clients.Remove(client);
            }

            Console.WriteLine("\nМагазин обслужил всех клиентов");
        }

        private Client GetFirstClient()
        {
            return _clients[0];
        }

        private void CreateClients()
        {
            int minRandomClientsCount = 3;
            int maxRandomClientsCount = 6;
            int clientsCount = UserUtils.GetRandomNumber(minRandomClientsCount, maxRandomClientsCount);

            for (int i = 0; i < clientsCount; i++)
            {
                string clientName = _names[UserUtils.GetRandomNumber(_names.Count)];
                Client client = new(clientName, _allProducts);

                _clients.Add(client);
            }
        }
    }

    class Client
    {
        private ShoppingCart _shoppingCart = new();
        private string _name;
        private int _money;

        public Client(string name, List<Product> allProducts)
        {
            _name = name;
            GainMoney();
            FillShoppingCart(allProducts);
        }

        public void PayOff()
        {
            while (_money < GetPurchaseAmount())
            {
                if (_shoppingCart.Empty)
                {
                    Console.WriteLine($"Больше нечего выкладывать, мне не хватило денег на еду( ");
                    return;
                }

                _shoppingCart.RemoveRandomProduct();
            }

            int productsCountToPurchase = _shoppingCart.Count;

            for (int i = 0; i < productsCountToPurchase; i++)
            {
                Product product = _shoppingCart.GetProducts()[0];
                Buy(product);
            }

            Console.WriteLine("Я расплатился за товары в корзине!");
        }

        public void ShowInfo()
        {
            Console.Write("\n-----------------------------");
            Console.Write($"\nКлиент: {_name} - ");

            if (_shoppingCart.Empty)
            {
                Console.WriteLine("корзина пуста");
                return;
            }

            Console.WriteLine(" хранит в корзине предметы:\n");

            foreach (Product product in _shoppingCart.GetProducts())
            {
                product.ShowInfo();
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine($"Сумма покупки: {GetPurchaseAmount()} рублей.");
            Console.WriteLine($"Денег в кошельке: {_money} рублей.");

            Console.WriteLine();
        }

        private void Buy(Product product)
        {
            _money -= product.Price;

            if (_shoppingCart.TryRemoveProduct(product) == false)
                return;

            Console.Write("Я купил ");
            product.ShowInfo();
            Console.WriteLine();
        }

        private int GetPurchaseAmount()
        {
            int sum = 0;

            foreach (Product product in _shoppingCart.GetProducts())
            {
                sum += product.Price;
            }

            return sum;
        }

        private void GainMoney()
        {
            int minRandomMoneyCount = 200;
            int maxRandomMoneyCount = 500;

            _money = UserUtils.GetRandomNumber(minRandomMoneyCount, maxRandomMoneyCount);
        }

        private void FillShoppingCart(List<Product> allProducts)
        {
            int minProductsCount = 3;
            int maxProductsCount = 8;

            int count = UserUtils.GetRandomNumber(minProductsCount, maxProductsCount + 1);

            for (int i = 0; i < count; i++)
            {
                int index = UserUtils.GetRandomNumber(allProducts.Count);
                Product product = allProducts[index];

                _shoppingCart.AddProduct(product);
            }

            Console.WriteLine($"Количество продуктов: {_shoppingCart.Count}");
        }
    }

    class ShoppingCart
    {
        private List<Product> _products = new();

        public bool Empty => _products.Count == 0;
        public int Count => _products.Count;

        public List<Product> GetProducts()
        {
            return new List<Product>(_products);
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public bool TryRemoveProduct(Product product)
        {
            if (_products.Remove(product))
                return true;

            throw new NullReferenceException(
                $"Вы хотите удалить такой товар, который найти не удалось: {product.Name}.");
        }

        public void RemoveRandomProduct()
        {
            Product product = _products[UserUtils.GetRandomNumber(_products.Count)];
            TryRemoveProduct(product);

            product.ShowInfo();
            Console.WriteLine(" удален из корзины.");
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} - {Price} рублей.");
        }
    }
}