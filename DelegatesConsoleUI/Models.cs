using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesConsoleUI
{
    public class ProductModel
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
       
    }

    public class ShoppingCartModel
    {
        public delegate void MentionDiscount(decimal subtotal);

        public List<ProductModel> Items { get; set; }

        public ShoppingCartModel()
        {
            Items = new List<ProductModel>();
        }

        public decimal GenerateTotal(MentionDiscount mentionSubtotal,
            Func<List<ProductModel>, decimal, decimal> calculateDiscountedTotal,
            Action<string> tellUserWeAreDicounting)
        {
            var subtotal = Items.Sum(i => i.Price);

            mentionSubtotal(subtotal);

            tellUserWeAreDicounting("Aplicando desconto");

            return calculateDiscountedTotal(Items, subtotal);
        }
    }
}
