using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAuthhentication.DAL;
using UserAuthhentication.Models;

namespace UserAuthhentication.BLL
{
    public static class ProductManager
    {
        public static List<Product> GetAllProduct()
        {
            return ProductGateway.GetAllProduct();
        }
    }
}