using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodCalculator.Models
{
    public class MealTypeViewModel
    {
        public List<MealType> MealTypes { get; set; }

        public MealType MealType { get; set; }
    }
}