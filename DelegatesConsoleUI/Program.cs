using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesConsoleUI
{
    class Program
    {
        static ShoppingCartModel cart = new ShoppingCartModel();

        static void Main(string[] args)
        {
            PopulateCart();

            Console.WriteLine($"Total do carrinho é {cart.GenerateTotal(subtotalAlert, calculateLeveledDiscount, alertUser):C2}");
            Console.ReadKey();

        }

        private static void PopulateCart()
        {
            cart.Items.Add(new ProductModel() { ItemName = "Chocolate", Price = 3.5m });
            cart.Items.Add(new ProductModel() { ItemName = "Pringles", Price = 4.75m });
            cart.Items.Add(new ProductModel() { ItemName = "Cerveja", Price = 2.5m });
            cart.Items.Add(new ProductModel() { ItemName = "Cookie", Price = 3.25m });
        }

        private static void subtotalAlert(decimal subtotal)
        {
            Console.WriteLine($"O subtotal é {subtotal:C2}");
        }

        private static void alertUser(string message)
        {
            Console.WriteLine(message);
        }

        private static decimal calculateLeveledDiscount(List<ProductModel> items, decimal subtotal)
        {

            if (subtotal > 100)
                return subtotal * 0.8m;
            else if (subtotal > 50)
                return subtotal * 0.85m;
            else if (subtotal > 10)
                return subtotal * 0.9m;
            else
                return subtotal;
        }
    }
}
