using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAuthhentication.Models.Sales
{
    public class SalesItem
    {
        public long SalesId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ProdTotal { get; set; }
        public List<Product> Products { get; set; }
    }
}