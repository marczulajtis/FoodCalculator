using FoodCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        public ActionResult AddMealType()
        {
            return View(this.GetMealTypeViewModel());
        }

        private MealTypeViewModel GetMealTypeViewModel()
        {
            MealTypeViewModel mtvm = new MealTypeViewModel();
            mtvm.MealTypes = context.MealTypes.ToList();

            return mtvm;
        }

        [HttpPost]
        public ActionResult AddMealType(MealType mealType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.MealTypes.Add(mealType);

                    context.SaveChanges();

                    TempData["Message"] = string.Format("Meal type {0} added.", mealType.MealTypeName);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occured while adding meal type. Please try again.");
            }

            return View(this.GetMealTypeViewModel());
        }

        public ActionResult ShowCategories()
        {
            return View(context.Categories.ToList());
        }

        public ActionResult AddMeal()
        {
            return View(this.GetMealViewModel());
        }

        public ActionResult AddProductToList()
        {
            if (Session["ProductsList"] == null)
            {
                this.PopulateSessionProductList();
            }

            return View("AddMeal", this.GetMealViewModel());
        }

        [HttpPost]
        public ActionResult AddProductToList(MealViewModel mvm)
        {
            if (Session["ProductsList"] == null)
            {
                this.PopulateSessionProductList();
            }

            ((List<MealProductMatch>)Session["ProductsList"]).Add(this.CreateMealProductMatch(mvm.SelectedProductID, mvm.ProductWeight, mvm.ProductWeightAfterBoiling));

            return View("AddMeal", this.GetMealViewModel());
        }

        public object PopulateSessionProductList()
        {
            List<MealProductMatch> mpmList = new List<MealProductMatch>();

            Session["ProductsList"] = mpmList;

            return Session["ProductsList"];
        }

        public MealProductMatch CreateMealProductMatch(int selectedProductID, int productWeight, int productWeightAfterBoiling)
        {
            MealProductMatch mpm = new MealProductMatch();

            if (context != null)
            {
                if (context.Products != null)
                {
                    mpm.Product = context.Products.Where(x => x.ProductID == selectedProductID).SingleOrDefault();
                }
            }

            mpm.Weight = productWeight;
            mpm.WeightAfterBoiling = productWeightAfterBoiling;

            return mpm;
        }

        public MealViewModel GetMealViewModel()
        {
            MealViewModel mvm = new MealViewModel();
            mvm.Products = context.Products.ToList();
            mvm.MealTypes = context.MealTypes.ToList();

            return mvm;
        }
    }
}