using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_01_Opstart.Models;

namespace MVC_01_Opstart.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Products()
        {
            Product p1 = new Product();
            p1.ID = 1;
            p1.Name = "Nike Sko";
            p1.Description = "This is a nice shoe, if you like that kind of shoe.";
            p1.Price = 39.99;
            p1.CategoryID = 1;

            Product p2 = new Product();
            p2.ID = 2;
            p2.Name = "Scholl Bicycle";
            p2.Description = "This is a nice Bicycle, if you like that kind of Bicycle.";
            p2.Price = 1999.99;
            p2.CategoryID = 2;

            List<Product> allProducts = new List<Product>();
            allProducts.Add(p1);
            allProducts.Add(p2);



            return View(allProducts);
        }

        public ActionResult ShowProduct(int id = 0)
        {
            Product p1 = new Product();
            p1.ID = 1;
            p1.Name = "Nike Sko";
            p1.Description = "This is a nice shoe, if you like that kind of shoe.";
            p1.Price = 39.99;
            p1.CategoryID = 1;

            Product p2 = new Product();
            p2.ID = 2;
            p2.Name = "Scholl Bicycle";
            p2.Description = "This is a nice Bicycle, if you like that kind of Bicycle.";
            p2.Price = 1999.99;
            p2.CategoryID = 2;

            List<Product> allProducts = new List<Product>();
            allProducts.Add(p1);
            allProducts.Add(p2);

            Product productToFind = allProducts.Find(product => product.ID == id);

            return View(productToFind);
        }
    }
}