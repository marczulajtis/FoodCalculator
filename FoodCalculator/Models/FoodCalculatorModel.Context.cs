﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodCalculator.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FoodCalculatorEntities1 : DbContext
    {
        public FoodCalculatorEntities1()
            : base("name=FoodCalculatorEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Consumption> Consumptions { get; set; }
        public virtual DbSet<MealProductMatch> MealProductMatches { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<MealType> MealTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
