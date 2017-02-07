using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodCalculator.Models
{
    public class MealViewModel
    {
        public MealViewModel()
        { }

        public List<Product> Products { get; set; }
        
        public int SelectedProductID { get; set; }
        
        public int ProductWeight { get; set; }

        public int ProductWeightAfterBoiling { get; set; }
        
        public Meal MealToAdd { get; set; }
        
        public int SelectedMealTypeID { get; set; }

        public List<MealType> MealTypes { get; set; }
    }
}