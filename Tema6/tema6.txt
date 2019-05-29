using System;

namespace ConsoleApp1
{
    public class Polynom
    {
        public readonly int n;
        public readonly double[] a;
        public readonly double[] x;
        public double R;
        
        public Polynom(int n)
        {
            this.n = n + 1;
            a = new double[this.n];
            x = new double[this.n];
            a[0] = 1.0;
            a[1] = -6;
            a[2] = 11.0;
            a[3] = -6;
        }

        public double P(double x)
        {
            double[] b = new double[n];
            b[0] = a[0];
            for(int i = 1; i < n; i++)
            {
                b[i] = a[i] + b[i - 1] * x;
            }
            return b[n-1];
        }

        public double Pderivat(double x)
        {
            double result = 0;
            int k = n;
            for(int i = 0; i < n; i++)
            {
                result += a[i] * k * Math.Pow(x,k-1);
                k--;
            }
            return result;
        }

        public void CreateXAndA(double x0)
        {
            x[0] = x0;
            for(int k = 0; k < n-1; k++)
            {
                x[k + 1] = x[k] - 1 / a[k];
                //a[k + 1] = Pderivat(x[k])/P(x[k]) - Pderivat(Pderivat((x[k])))/(2*Pderivat(x[k]));
            }
            double maxA = -99999;
            for(int i = 0; i< n; i++)
            {
                if (a[i] > maxA) maxA = a[i];
            }
            R = (a[0] + maxA) / a[0];
            x[n-1] = 0;
            int tk = n;
            for(int i = 0; i<n; i++)
            {
                Console.Write(string.Format("{0} * {1} ^ {2} +",a[i],x[i],tk));
                tk--;
            }
        }

        public void Halley()
        {
            double A = 0;
            var k = 1;
            var kMax = 10;
            double delta = 3;
            Random rnd = new Random();
            var ranX = this.x[rnd.Next(this.n)];
            while (delta >= Math.E && k <= kMax && delta <= Math.Pow(10, 8))
            {
                A = 2 * Math.Pow(Pderivat(ranX),2) - P(x[k]) * Pderivat(Pderivat(ranX));
                if (A < Math.E) break;
                delta = (P(ranX) * Pderivat(ranX)) / A;
                ranX = ranX - delta;
                k++;
            }
            if (delta < Math.E) Console.WriteLine(delta);
            else Console.WriteLine("TryAnother x0");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int n = 3;
            Polynom polynom = new Polynom(n);
            polynom.CreateXAndA(6);
            polynom.Halley();
        }
    }
}
