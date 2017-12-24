using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuthhentication.BLL;
using UserAuthhentication.Models;
using UserAuthhentication.Models.Sales;

namespace UserAuthhentication.Controllers.Sales
{
    public class SalesController : BaseController
    {
        // GET: Sales
        public ActionResult Sales()
        {
            IEnumerable<Product> listProducts = ProductManager.GetAllProduct();
            if (listProducts != null)
            {
                //var productsList = new SelectList(listProducts, "ProductId", "ProductName", "");
                Session["Products"] = listProducts;
                ViewBag.Products = listProducts;
            }
            return View();
        }
        public JsonResult GetProductById(long ProductId)
        {
            try
            {
                Product product = new Product();
                List<Product> products = (List<Product>)Session["Products"];
                products = products.Where(m => m.ProductId == ProductId).Take(1).ToList();
                foreach (var item in products)
                {
                    product.ProductName = item.ProductName;
                    product.SalesPrice = item.SalesPrice;
                }
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }
        public JsonResult GetProductsById(SalesItem Sales)
        {
            try
            {
                List<Product> items = new List<Product>();
                items = Sales.Products;
                if (items != null)
                    items = items.Where(m => m.ProductId != Sales.ProductId).Take(1).ToList();
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }
    }
}