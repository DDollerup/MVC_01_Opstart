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
            categoryFac = new CategoryFactory();

            List<Category> allCategories = categoryFac.GetAll();
            ViewBag.AllCategories = allCategories;

            if (TempData["FilteredProducts"] == null)
            {
                List<Product> allProducts = productFac.GetAll();
                return View(allProducts); 
            }
            else
            {
                return View(TempData["FilteredProducts"]);
            }
        }

        public ActionResult ShowProduct(int id = 0)
        {
            productFac = new ProductFactory();
            Product productToFind = productFac.Get(id);
            return View(productToFind);
        }

        [HttpPost]
        public ActionResult ShowFilteredProducts(int categoryID)
        {
            productFac = new ProductFactory();
            List<Product> filteredProducts = productFac.GetAll()
                .Where(p => p.CategoryID == categoryID).ToList();
            TempData["FilteredProducts"] = filteredProducts;

            return RedirectToAction("Products");
        }
    }
}