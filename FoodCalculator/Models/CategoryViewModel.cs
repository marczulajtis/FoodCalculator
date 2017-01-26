using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodCalculator.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        { }
        
        public SelectList CategoriesList { get; set; }

        public Product NewProduct { get; set; }
    }
}