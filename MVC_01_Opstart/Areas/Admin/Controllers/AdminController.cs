using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_01_Opstart.Factories;
using MVC_01_Opstart.Models;

namespace MVC_01_Opstart.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        ProductFactory productFac;
        CategoryFactory categoryFac;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            categoryFac = new CategoryFactory();
            ViewBag.AllCategories = categoryFac.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddProductSubmit(Product p)
        {
            productFac = new ProductFactory();
            productFac.Add(p);

            TempData["MSG"] = "A new product, " + p.Name + ", has been added.";

            return RedirectToAction("Index");
        }
    }
}