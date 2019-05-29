using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Tema2
{
    public class Ex1
    {
        double[,] A;
        double[,] Ainit;
 
        int n;
        double[] b = { 0, 1,2,3 };
        double[] x = new double[4];
        double[] y = new double[4];

        public Ex1()
        {
            Console.WriteLine("Matrix dimension:");
            n = int.Parse(Console.ReadLine());
            int i, j;
            A = new double[n+1, n+1];
            Ainit = new double[n+1, n+1];
            Console.WriteLine("Matrix elements: ");
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    Console.Write("A[{0},{1}] = ", i, j);
                    A[i, j] = double.Parse(Console.ReadLine());     
                }
            }
           for(i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    Console.Write("A[{0},{1}] = ", i, j);
                    Ainit[i, j] = A[i, j];
                }
            }
            Console.WriteLine();       
        }

        public bool Singular()
        {
            double result = 1;
            int i;
            int j;
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    if (i == j)
                    {
                        result = result * A[i, j];
                    }
                }
            }
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public void DescompunereLU()
        {
            int p;
            int i;
                
            for (p = 1; p <= n; p++)
            {
                for (i = 1; i <= p-1; i++)
                {
                    //Calculul elementelor coloanei p din matricea U
                    double sumU = 0;
                    for(int k =1; k<= i-1; k++)
                    {
                        sumU += A[i, k] * A[k, p];
                    }

                    A[i, p] = (A[i, p] - sumU) / A[i, i];
                }

                for(i = 1; i<= p; i++)
                {
                    //Calculul elementelor liniei p din matricea L
                    double SumL = 0;
                    for(int k =1; k<=i-1; k++)
                    {
                        SumL += A[p, k] * A[k, i];
                    }

                    A[p, i] = A[p, i] - SumL;
                }
            }

        }

        public void printMatrix()
        {
            Console.WriteLine("Matrix A");
            for(int i = 1; i <= n; i++)
            {
                for(int j = 1; j <= n; j++)
                {
                    Console.Write(A[i,j]+ " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");

            Console.WriteLine("Matrix Ainit");
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    Console.Write(Ainit[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public double DetA()
        {
            double result = 1;
            for(int i = 1; i<=n; i++)
            {
                for(int j = 1; j<=n; j++)
                {
                    if(i == j)
                    {
                        result = result * A[i, j];
                    }
                }
            }
            Console.WriteLine("Det A =" + result);
            return result;
        }

        public double[] Subtitution(string LU)
        {
            int i;
            int j;
            y[0] = 0;
            if (LU == "L")
            {
                y[1] = b[1] / A[1, 1];
                for (i = 2; i <= n; i++)
                {
                    double sum = 0;
                    for (j = 1; j <= i - 1; j++)
                    {
                        sum += A[i, j] * y[j];
                    }
                    y[i] = (b[i] - sum) / A[i, i];
                }
            }
            else
            {
                x[n] = y[n];
                for (i = n - 1; i >= 1; i--)
                {
                    double sum = 0;
                    for (j = i + 1; j <= n; j++)
                    {
                        sum += A[i, j] * x[j];
                    }

                    x[i] = y[i] - sum;
                }
            }

            foreach(var a in x)
            {
                Console.WriteLine(a);
            }
            return x;
        }

        public void norma()
        {
            double[] y = new double[n+1];
            for(int i =1; i<=n; i++)
            {
                double sum = 0;
                for(int j =1; j<=n; j++)
                {
                    sum += Ainit[i, j] * x[j];
                }
                y[i] = sum;
            }
            double[] z = new double[n+1];
            
            for(int i = 1; i<=n; i++)
            {
                z[i] = y[i] - b[i];
            }

             double Zsum = 0;
            for(int i =1; i <= n; i++)
            {
                Zsum = Zsum + Math.Pow(z[i], 2);
            }
            Console.WriteLine("Norma = " + Math.Sqrt(Zsum));
        }

        public void Invers()
        {
            double[,] aux = new double[n, n];

            for (int i = 1;i<= n; i++)
            {
                for(int j=1;j<=n; j++)
                {
                    aux[i - 1, j - 1] = Ainit[i, j];
                }
            }

            DenseMatrix Reverse = DenseMatrix.OfArray(aux);

            var b = Reverse.Inverse();

            for(int i = 0; i<n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    Console.WriteLine(b[i,j]);
                }
            }

        }
    }
}
