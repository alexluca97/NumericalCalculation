using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tema1
{

    class Program
    {
        static void Main(string[] args)
        {
            Ex1 ex1 = new Ex1();
            Ex2 ex2 = new Ex2();
            Ex3 ex3 = new Ex3();

            Console.WriteLine(string.Format("Exercitiul 1.\n" +
                "=============================================\n" +
                "Result = {0} \n" +
                "=============================================\n\n",ex1.Start()));

            double u = ex1.Start();
            Console.WriteLine(string.Format("Exercitiul 2.\n" +
                "=============================================\n" +
                "Result = {0}",ex2.StartSum(u)));
            if(ex2.StartSum(u) is false)
            {
                Console.WriteLine("\"(x+y)+z != x+(y+z)\" Nu este asociativa.");
            }
            else
            {
                Console.WriteLine("\"(x + y) + z != x + (y + z)\"Este asociativa.\n\n");
            }
            Console.WriteLine(string.Format("Result = {0}", ex2.StartMultiply(u)));
            if (ex2.StartMultiply(u) is false)
            {
                Console.WriteLine("\"(x * y) * z != x * (y * z)\" Nu este asociativa.");
            }
            else
            {
                Console.WriteLine("\"(x * y) * z != x * (y * z)\"Este asociativa.\n\n");
            }
            Console.WriteLine("=============================================\n\n");
            //EX3 will generate 100 results instead of 10.000
            //if you want to change it, go to Ex3.cs start() method and change at while
            Console.WriteLine(string.Format("Exercitiul 3.\n" +
                "=============================================\n" +
                "Result:\n"));
            Dictionary<double, Tuple<string,string,string>> results = ex3.Start();
            foreach(KeyValuePair<double,Tuple<string,string,string>> result in results)
            {
                Console.WriteLine("Number = {0}, BestPolynoms = {1}", result.Key, result.Value);
            }
            Console.WriteLine("=============================================\n\n");

        }
    }
}
