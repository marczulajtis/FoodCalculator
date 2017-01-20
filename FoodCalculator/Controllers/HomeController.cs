using FoodCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCalculator.Controllers
{
    public class HomeController : Controller
    {
        private static FoodCalculatorEntities1 context;

        public HomeController(FoodCalculatorEntities1 dbContext)
        {
            context = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            TempData["Message"] = string.Format("Product {0} added", product.ProductName);

            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                using (context)
                {
                    context.Categories.Add(new Category
                    {
                        CategoryName = category.CategoryName
                    });

                    context.SaveChanges();

                    TempData["Message"] = "Category added";
                }
            }

            return View();
        }

        public static List<SelectListItem> GetCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            
            foreach (var category in context.Categories)
            {
                categories.Add(new SelectListItem() { Text = category.CategoryName, Value = category.CategoryID.ToString() });
            }

            return categories;
        }
    }
}