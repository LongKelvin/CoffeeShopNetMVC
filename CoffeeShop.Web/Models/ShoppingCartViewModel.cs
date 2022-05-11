using System;

namespace CoffeeShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductID { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
}