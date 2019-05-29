using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class Ex3
    {
        Stopwatch sw = new Stopwatch();
        List<Stopwatch> P1 = new List<Stopwatch>();
        List<Stopwatch> P2 = new List<Stopwatch>();
        List<Stopwatch> P3 = new List<Stopwatch>();
        List<Stopwatch> P4 = new List<Stopwatch>();
        List<Stopwatch> P5 = new List<Stopwatch>();
        List<Stopwatch> P6 = new List<Stopwatch>();

        double c1 = 0.16666666666666666666666666666667;
        double c2 = 0.00833333333333333333333333333333;
        double c3 = 1.984126984126984126984126984127e-4;
        double c4 = 2.7557319223985890652557319223986e-6;
        double c5 = 2.5052108385441718775052108385442e-8;
        double c6 = 1.6059043836821614599392377170155e-10;

        public double getP1(double x)
        {
            //return x - c1 * Math.Pow(x, 3) + c2 * Math.Pow(x, 5);
            double y = x * x;
            // x - c1 * x * x * x + c2 * x * x * x * x * x;
            return x * (1 + y * (-c1 + c2 * y));
        }

        public double getP2(double x)
        {
            //return x - c1 * Math.Pow(x, 3) + c2 * Math.Pow(x, 5) - c3 * Math.Pow(x,7);
            double y = x * x;
            //x - c1 * x*x*x + c2 * x*x*x*x*x - c3 * x*x*x*x*x*x*x
            return x * (1 + y * (-c1 + y * (c2 - c3 * y)));
        }

        public double getP3(double x)
        {
            //return x - c1 * Math.Pow(x, 3) + c2 * Math.Pow(x, 5) - c3 * Math.Pow(x, 7) +
            //    + c4 * Math.Pow(x, 9);
            double y = x * x;
            return x * (1 + y * (-c1 + y * (c2 + y * (-c3 + c4 * y))));
        }

        public double getP4(double x)
        {
            //return x - 0.166 * Math.Pow(x, 3) + 0.00833 * Math.Pow(x, 5) - c3 * Math.Pow(x, 7) +
            //    +c4 * Math.Pow(x, 9);
            double y = x * x;
            return x * (1 + y * (-0.166 + y * (0.00833 + y * (-c3 + c4 * y))));
        }

        public double getP5(double x)
        {
            //return x - c1 * Math.Pow(x, 3) + c2 * Math.Pow(x, 5) - c3 * Math.Pow(x, 7) +
            //    +c4 * Math.Pow(x, 9) - c5 * Math.Pow(x, 11);
            double y = x * x;
            return x * (1 + y * (-c1 + y * (c2 + y * (-c3 + y * (c4 - c5 * y)))));
        }

        public double getP6(double x)
        {
            //return x - c1 * Math.Pow(x, 3) + c2 * Math.Pow(x, 5) - c3 * Math.Pow(x, 7) +
            //    +c4 * Math.Pow(x, 9) - c5 * Math.Pow(x, 11) + c6 * Math.Pow(x, 13);
            double y = x * x;
            return x * (1 + y * (-c1 + y * (c2 + y * (-c3 + y * (c4 + y * (-c5 + c6 * y))))));
        }

        private string[] CheckPolynoms(double P1, double P2, double P3, double P4, double P5, double P6, double x)
        {
            double[] array = { P1, P2, P3, P4, P5, P6 };
            var orderedResults = array.OrderBy(z => Math.Abs(z - Math.PI)).Take(3).ToList();
            string[] results = new string[3];
            for (int i = 0; i < 3; i++)
            {
                if (orderedResults[i] == P1)
                    results[i] = "P1";
                else if (orderedResults[i] == P2)
                    results[i] = "P2";
                else if (orderedResults[i] == P3)
                    results[i] = "P3";
                else if (orderedResults[i] == P4)
                    results[i] = "P4";
                else if (orderedResults[i] == P5)
                    results[i] = "P5";
                else
                    results[i] = "P6";
            }

            return results;
        }
        public Dictionary<double, Tuple<string,string,string>> Start()
        {
            Dictionary<double, Tuple<string,string,string>> bestPolynoms = new Dictionary<double, Tuple<string,string,string>>();
            Random rnd = new Random();
            int i = 0;
            double x, resultP1, resultP2, resultP3, resultP4, resultP5, resultP6;
            double min = -(Math.PI / 2);
            double max = Math.PI / 2;
            string[] results;
            while (i < 100)
            {
                x = rnd.NextDouble() * (max - min) + min;
                sw.Start();
                resultP1 = getP1(x);
                sw.Stop();
                P1.Add(sw);
                sw.Restart();
                resultP2 = getP2(x);
                sw.Stop();
                P2.Add(sw);
                sw.Reset();
                resultP3 = getP3(x);
                sw.Stop();
                P3.Add(sw);
                sw.Restart();
                resultP4 = getP4(x);
                sw.Stop();
                P4.Add(sw);
                sw.Restart();
                resultP5 = getP5(x);
                sw.Stop();
                P5.Add(sw);
                sw.Restart();
                resultP6 = getP6(x);
                sw.Stop();
                P6.Add(sw);
                results = CheckPolynoms(resultP1, resultP2, resultP3, resultP4, resultP5, resultP6, x);
                if (!bestPolynoms.ContainsKey(x))
                {
                    bestPolynoms.Add(x,new Tuple<string, string, string>(results[0],results[1],results[2]));
                }
                i++;
            }
            return bestPolynoms;
        }

        public void bestTimes()
        {
            
            for(int i = 0; i < 100; i++)
            {
                
            }
        }
    }
}
