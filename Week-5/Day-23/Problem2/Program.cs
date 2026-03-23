using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Progaram
    {
        static void Main()
        {
            Product product = new Product();
            var products = product.GetProducts();
            //1.Write a LINQ query to search and display all products with category “FMCG”.
            Console.WriteLine("question1");
            var result= from p in products
                       where p.ProCategory =="FMCG"
                       select p;
 
            foreach (var item in result)
            {
                Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
            }
          //2. Write a LINQ query to search and display all products with category “Grain”.
             Console.WriteLine("question 2");
            var result1= from p in products
                       where p.ProCategory =="Grain"
                       select p;;
            foreach(var item1 in result1)
            {
                Console.WriteLine($"{item1.ProCode}\t{item1.ProName}\t{item1.ProCategory}\t{item1.ProMrp}");
            }
           // 3. Write a LINQ query to sort products in ascending order by product code.
            Console.WriteLine("question3");
           var result2=from p in products
                       orderby p.ProCode
                        select p;
           foreach(var item2 in result2)
            {
                Console.WriteLine($"{item2.ProCode}\t{item2.ProName}\t{item2.ProCategory}\t{item2.ProMrp}");
            }
            //4. Write a LINQ query to sort products in ascending order by product Category.
             Console.WriteLine("question4");
            var result3=from p in products
                       orderby p.ProCategory
                        select p;;
           foreach(var item3 in result3)
            {
                Console.WriteLine($"{item3.ProCode}\t{item3.ProName}\t{item3.ProCategory}\t{item3.ProMrp}");
            }
           // 5. Write a LINQ query to sort products in ascending order by product Mrp.
            Console.WriteLine("question5");
            var result4=from p in products
                       orderby p.ProMrp
                        select p;
           foreach(var item4 in result4)
            {
                Console.WriteLine($"{item4.ProCode}\t{item4.ProName}\t{item4.ProCategory}\t{item4.ProMrp}");
            }
          //  6. Write a LINQ query to sort products in descending order by product Mrp.
           Console.WriteLine("question6");
           var result5=from p in products
                       orderby p.ProMrp descending
                        select p;
           foreach(var item5 in result5)
            {
                Console.WriteLine($"{item5.ProCode}\t{item5.ProName}\t{item5.ProCategory}\t{item5.ProMrp}");
            }
            //7. Write a LINQ query to display products group by product Category.
             Console.WriteLine("question7");
           var result6=from p in products
              group p by p.ProCategory into g
              select g;
           foreach (var group in result6)
            {
             Console.WriteLine($"Category: {group.Key}");
            foreach (var item6 in group)
           {
            Console.WriteLine($" {item6.ProName}");
           }
            }
         //8. Write a LINQ query to display products group by product Mrp.
          Console.WriteLine("question8");
         var result7= from p in products
              group p by p.ProMrp into g
              select g;
           foreach (var group in result7)
            {
             Console.WriteLine($"Category: {group.Key}");
            foreach (var item7 in group)
           {
            Console.WriteLine($" {item7.ProName}");
           }
            }
       // 9. Write a LINQ query to display product detail with highest price in FMCG category.
        Console.WriteLine("question9");
         var result9 = (from p in products
               where p.ProCategory == "FMCG"
               orderby p.ProMrp descending
               select p).FirstOrDefault();       Console.WriteLine($"{result9.ProCode}\t{result9.ProName}\t{result9.ProCategory}\t{result9.ProMrp}");
        //10. Write a LINQ query to display count of total products.
         Console.WriteLine("question10");
            var result10 = (from p in products
                select p).Count();      
              Console.WriteLine(result10);
       // 11. Write a LINQ query to display count of total products with category FMCG.
         Console.WriteLine("question11");
         var result11 = (from p in products
                where p.ProCategory == "FMCG"
                select p).Count();
        Console.WriteLine(result11);
      //  12.Write a LINQ query to display Max price.
       Console.WriteLine("question12");
        var result12 = (from p in products
                select p.ProMrp).Max();
        Console.WriteLine(result12);
      //  13.Write a LINQ query to display Min price. 
      Console.WriteLine("question13");
        var result13 = (from p in products
                select p.ProMrp).Min();
        Console.WriteLine(result13);
        //14. Write a LINQ query to display whether all products are below Mrp Rs.30 or not.
         Console.WriteLine("question14");
        var result14 = (from p in products
                select p).All(p => p.ProMrp < 30);
        Console.WriteLine(result14);
       //15. Write a LINQ query to display whether any products are below Mrp Rs.30 or not.
        Console.WriteLine("question15");
        var result15 = (from p in products
                select p).Any(p => p.ProMrp < 30);
        Console.WriteLine(result15);
        } 
    }
}