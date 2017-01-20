﻿using FoodCalculator.Models;
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
        private CategoryViewModel vm;

        public HomeController(FoodCalculatorEntities1 dbContext, CategoryViewModel vm)
        {
            context = dbContext;
            this.vm = vm;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ShowProducts()
        {
            return View(context.Products.ToList());
        }

        public ActionResult AddProduct()
        {
            this.vm.CategoriesList = new SelectList(context.Categories, "CategoryID", "CategoryName", 1);

            return View(this.vm);
        }

        [HttpPost]
        public ActionResult AddProduct(CategoryViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Products.Add(vm.NewProduct);

                    context.SaveChanges();

                    TempData["Message"] = string.Format("Product {0} added", vm.NewProduct.ProductName);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occured while adding new product. Please try again.");
            }

            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            try
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occured while adding new category. Please try again.");
            }

            return View();
        }

        public static List<SelectListItem> GetCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            categories.Add(new SelectListItem { Text = "--- Select category ---", Value = "0" });

            foreach (var category in context.Categories)
            {
                categories.Add(new SelectListItem() { Text = category.CategoryName, Value = category.CategoryID.ToString() });
            }

            return categories;
        }
    }
}