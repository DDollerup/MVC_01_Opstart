using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_01_Opstart.Factories;
using MVC_01_Opstart.Models;
using System.Security.Cryptography;
using System.Text;

namespace MVC_01_Opstart.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        ProductFactory productFac;
        CategoryFactory categoryFac;
        AccountFactory accountFac;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserLoggedIn"] == null && !Request.RawUrl.ToLower().Contains("login"))
            {
                Response.Redirect("/Admin/Admin/Login");
            }

            base.OnActionExecuting(filterContext);
        }

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
        public ActionResult AddProductSubmit(Product p, HttpPostedFileBase file)
        {
            p.Image = "placeholder.jpg";

            // Is there a file?
            if (file != null && file.ContentLength > 0)
            {
                string fileName = file.FileName;
                p.Image = fileName; // HVIS SØRENS ELEVER, SLET DENNE

                string path = Request.PhysicalApplicationPath + @"/Content/";
                // TIL SØRENS ELEVER p.Image = path + fileName;
                file.SaveAs(path + fileName);
            }

            productFac = new ProductFactory();
            productFac.Add(p);

            TempData["MSG"] = "A new product, " + p.Name + ", has been added.";

            return RedirectToAction("Index");
        }

        public ActionResult UpdateProductsList()
        {
            productFac = new ProductFactory();
            List<Product> allProducts = productFac.GetAll();
            return View(allProducts);
        }

        public ActionResult UpdateProduct(int id = 0)
        {
            categoryFac = new CategoryFactory();
            productFac = new ProductFactory();
            ViewBag.AllCategories = categoryFac.GetAll();

            Product productToEdit = productFac.Get(id);

            return View(productToEdit);
        }

        [HttpPost]
        public ActionResult UpdateProductSubmit(Product p, HttpPostedFileBase file)
        {
            // Is there a file?
            if (file != null && file.ContentLength > 0)
            {
                string fileName = file.FileName;
                p.Image = fileName; // HVIS SØRENS ELEVER, SLET DENNE

                string path = Request.PhysicalApplicationPath + @"/Content/";
                // TIL SØRENS ELEVER p.Image = path + fileName;
                file.SaveAs(path + fileName);
            }

            productFac = new ProductFactory();
            productFac.Update(p);

            TempData["MSG"] = "A new product, " + p.Name + ", has been updated.";

            return RedirectToAction("Index");
        }

        public ActionResult DeleteProductSubmit(int id)
        {
            productFac = new ProductFactory();
            Product p = productFac.Get(id);
            productFac.Delete(id);

            TempData["MSG"] = "A new product, " + p.Name + ", has been deleted.";

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginSubmit(string username, string password)
        {
            accountFac = new AccountFactory();

            string encryptedPassword = "";

            SHA512 key = new SHA512Managed();
            key.ComputeHash(Encoding.ASCII.GetBytes(password));
            encryptedPassword = BitConverter.ToString(key.Hash).Replace("-", "");

            Account accountToLogin = accountFac.GetAll()
                .Find(x =>
                x.Username.ToLower() == username.ToLower()
                &&
                x.Password == encryptedPassword);

            if (accountToLogin != null && accountToLogin.ID > 0)
            {
                Session["UserLoggedIn"] = accountToLogin;
            }
            else
            {
                TempData["MSG"] = "Username or password was incorrect.";
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session["UserLoggedIn"] = null;
            return Redirect("/Home/Index");
        }
    }
}