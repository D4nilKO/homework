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
        private List<Client> _clients = new();
    }

    class Client
    {
        private ShoppingCart _shoppingCart = new ();
        public int Money { get; private set; }

        public Client()
        {
            
        }

        private void FillShoppingCart(Product[] products)
        {
            
        }
    }

    class ShoppingCart
    {
        private List<Product> _products = new();

        // public ShoppingCart(params Product[] products)
        // {
        //     foreach (Product product in products) _products.Add(product);
        // }
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