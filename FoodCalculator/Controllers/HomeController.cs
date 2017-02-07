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

        [HttpPost]
        public ActionResult AddMeal(MealViewModel mvm)
        {
            if (mvm != null)
            {
                if (this.ValidateAddingMeal(mvm))
                {
                    // add meal
                    Meal mealToAdd = this.PopulateMealObject(mvm);

                    context.Meals.Add(mealToAdd);

                    context.SaveChanges();

                    // add meal product matches
                    foreach (var mpm in this.PopulateMealProductMatchList(mealToAdd.MealID))
                    {
                        context.MealProductMatches.Add(mpm);
                    }

                    context.SaveChanges();

                    TempData["Message"] = string.Format("Meal {0} added.", mealToAdd.MealName);
                }
            }

            return View(this.GetMealViewModel());
        }

        private Meal PopulateMealObject(MealViewModel mvm)
        {
            Meal meal = new Meal();
            meal.MealName = mvm.MealToAdd.MealName;
            meal.MealTypeID = mvm.SelectedMealTypeID;
            
            return meal;
        }

        private List<MealProductMatch> PopulateMealProductMatchList(int mealID)
        {
            List<MealProductMatch> mpmList = new List<MealProductMatch>();

            foreach (var mealProductMatch in ((List<MealProductMatch>)Session["ProductsList"]))
            {
                MealProductMatch mpm = new MealProductMatch()
                {
                    ProductID = mealProductMatch.ProductID,
                    Weight = mealProductMatch.Weight,
                    WeightAfterBoiling = mealProductMatch.WeightAfterBoiling,
                    MealID = mealID
                };

                mpmList.Add(mpm);
            }

            return mpmList;
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

            if (mvm != null)
            {
                if (this.ValidateAddingProducts(mvm))
                {
                    ((List<MealProductMatch>)Session["ProductsList"]).Add(this.CreateMealProductMatch(mvm.SelectedProductID, mvm.ProductWeight, mvm.ProductWeightAfterBoiling));

                }
            }

            return View("AddMeal", this.GetMealViewModel());
        }

        private bool ValidateAddingProducts(MealViewModel mvm)
        {
            if (mvm.SelectedProductID != 0)
            {
                if (mvm.ProductWeight > 0)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "Product weight has to be greater than 0.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please select product.");
            }

            return false;
        }

        private bool ValidateAddingMeal(MealViewModel mvm)
        {
            if (mvm.MealToAdd != null)
            {
                if (!(string.IsNullOrEmpty(mvm.MealToAdd.MealName)))
                {
                    if (mvm.SelectedMealTypeID != 0)
                    {
                        return true;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please select a meal type.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please type a meal name.");
                }
            }

            return false;
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

            mpm.ProductID = selectedProductID;
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