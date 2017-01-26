using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime thisQuarterStart = QuarterStart(new DateTime(2017, 3, 31));
            DateTime lastQuarterEnd = thisQuarterStart.AddDays(-1);

            //System.Console.WriteLine(string.Format("Input date : {0}", ));
            System.Console.WriteLine(string.Format("First day of this quarter: {0}", thisQuarterStart.ToShortDateString()));
            System.Console.WriteLine(string.Format("Last ay of previous quarter: {0}", lastQuarterEnd.ToShortDateString()));

            System.Console.ReadKey();
        }
        
        /// <summary>
        /// Returns first day of reference date quarter
        /// </summary>
        /// <param name="referenceDate"></param>
        /// <returns></returns>
        public static DateTime QuarterStart(DateTime referenceDate)
        {
            int startingMonth = (referenceDate.Month - 1) / 3;
            
            startingMonth *= 3;
            
            startingMonth++;
            
            return new DateTime(referenceDate.Year, startingMonth, 1);
        }
    }
}
