using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAuthhentication.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal SalesPrice { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ProdTotal { get; set; }
    }
}