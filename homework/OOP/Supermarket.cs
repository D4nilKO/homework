using System;
using System.Collections.Generic;

namespace homework.OOP.Supermarket
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    class Supermarket
    {
        private Random random = new();
        private int minRandomClientsCount = 5;
        private int maxRandomClientsCount = 10;
        public Supermarket()
        {
            
        }
        
        private List<Client> _clients = new();

        private List<Product> _allProducts = new ()
        {
            new Product("Молоко", 10),
            new Product("Яйца", 50),
            new Product("Булочка с ничем", 9),
            new Product("Булочка с кое-чем", 49),
            new Product("Творог", 70 ),
            new Product("Сок",  100),
            new Product("Курица филе", 150),
            new Product("Хлеб", 15 ),
            new Product("Рис", 35 )
        };

        private List<string> names = new()
        {
            "Джон",
            "Борис",
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
    }

    class Client
    {
        private ShoppingCart _shoppingCart = new();
        public int Money { get; private set; }

        public Client(List<Product> products)
        {
            FillShoppingCart(products);
        }

        private void FillShoppingCart(List<Product> products) 
        {
            Random random = new();
            
            int minRandomProduct = 0;
            int maxRandomProduct = products.Count;
            
            int numberOfProduct = random.Next(minRandomProduct, maxRandomProduct);
            Product product = products[numberOfProduct];
            
            _shoppingCart.AddProduct(product);
        }
    }

    class ShoppingCart
    {
        public List<Product> Products { get; private set; } = new();
        public bool Empty => Products.Count == 0;

        public ShoppingCart(params Product[] products)
        {
            foreach (Product product in products)
                AddProduct(product);
        }

        public ShoppingCart()
        {
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public bool TryRemoveProduct(Product product)
        {
            if (Products.Contains(product))
            {
                Products.Remove(product);
                Console.WriteLine($"Товар {product} удален из корзины.");
                return true;
            }

            return false;
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
    }
}