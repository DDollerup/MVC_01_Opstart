using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_01_Opstart.Models;
using MVC_01_Opstart.Factories;

namespace MVC_01_Opstart.Controllers
{
    public class HomeController : Controller
    {
        ProductFactory productFac;
        CategoryFactory categoryFac;

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
            productFac = new ProductFactory();
            List<Product> allProducts = productFac.GetAll();
            return View(allProducts);
        }

        public ActionResult ShowProduct(int id = 0)
        {
            productFac = new ProductFactory();
            Product productToFind = productFac.Get(id);
            return View(productToFind);
        }
    }
}