using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FoodCalculator.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/scripts").Include("~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/scripts/bootstrap.js", 
                "~/scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/Custom.css"));

            bundles.Add(new ScriptBundle("~/scripts/custom").Include(
                "~/scripts/custom/MealDetails.js"));
        }
    }
}