using System;
using System.Collections.Generic;
using System.IO;

namespace Tema5
{
    class MatriceRara
    {
        public List<List<Tuple<double, int>>> elemente;

        public MatriceRara()
        {
            elemente = new List<List<Tuple<double, int>>>();
        }

        public void GetDataFrom(double[,] simpleMatrix, int size)
        {
            for (var i = 0; i < size; i++)
            {
                var newRow = new List<Tuple<double, int>>();

                for (var j = 0; j < size; j++)
                {
                    var cell = simpleMatrix[i, j];

                    if (cell != 0 && cell != 0.0)
                    {
                        var entry = new Tuple<double, int>(cell, j + 1);
                        newRow.Add(entry);
                    }
                }

                elemente.Add(newRow);
            }
        }
    }

    class Matrice
    {
        public double[,] mat;
        public int size;
        public Matrice(IEnumerable<string> matSize, IEnumerable<string> matValue)
        {
            int i = 0;
            int j = 0;
            foreach (var si in matSize)
            {
                size = int.Parse(si);
            }

            mat = new double[size, size];

            foreach (var line in matValue)
            {
                var numbers = line.Split(", ");
                foreach (var number in numbers)
                {
                    mat[i, j] = double.Parse(number);
                    j++;
                    if (j == size)
                    {
                        i++;
                        j = 0;
                    }
                }
            }
        }
    }


    class Program
    {
        public static double[] vec;
        public static double EPS = Math.Pow(10, -8);
        public static double K_MAX = Math.Pow(10, 6);

        static void Main(string[] args)
        {
            var fileNames = new[] { "m_rar_sim_2019_500.txt","m_rar_sim_2019_500_size.txt",
                "m_rar_sim_2019_1000.txt","m_rar_sim_2019_1000_size.txt",
                "m_rar_sim_2019_1500.txt", "m_rar_sim_2019_1500_size.txt",
                "m_rar_sim_2019_2019.txt","m_rar_sim_2019_2019_size.txt"};
            var linesA_size = File.ReadLines(fileNames[1]);
            var linesA_mat = File.ReadLines(fileNames[0]);
            var linesA_vec = File.ReadLines(fileNames[2]);

            Matrice m_rar_sim_2019_500 = new Matrice(File.ReadLines(fileNames[1]), File.ReadLines(fileNames[0]));
            Matrice m_rar_sim_2019_1000 = new Matrice(File.ReadLines(fileNames[3]), File.ReadLines(fileNames[2]));
            Matrice m_rar_sim_2019_1500 = new Matrice(File.ReadLines(fileNames[5]), File.ReadLines(fileNames[4]));
            Matrice m_rar_sim_2019_2019 = new Matrice(File.ReadLines(fileNames[7]), File.ReadLines(fileNames[6]));

            int p;
            int size;
            var matricerara = new MatriceRara();
            var matriceRaraA = new MatriceRara();
            //matricerara.GetDataFrom(m_rar_sim_2019_1000.mat, m_rar_sim_2019_1000.size);

            Console.WriteLine("p =");
            p = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("n =");
            size = Convert.ToInt32(Console.ReadLine());
            double[] vecB = new double[p];
            //size = m_rar_sim_2019_1000.size;
            if(p == size && p > 500)
            {
                CreateRandomMatrixA(size);
                var MatAFile = File.ReadLines(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated.txt");
                var MatASizeFile = File.ReadLines(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated_size.txt");
                Matrice MatA = new Matrice(MatASizeFile,MatAFile);
                matricerara.GetDataFrom(m_rar_sim_2019_1000.mat, m_rar_sim_2019_1000.size);
                matriceRaraA.GetDataFrom(MatA.mat, MatA.size);
            }
            else if(p == size)
            {
                var MatAFile = File.ReadLines(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated.txt");
                var MatASizeFile = File.ReadLines(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated_size.txt");
                Matrice MatA = new Matrice(MatASizeFile,MatAFile );
                CreateRandomVectorB(size);
                if (!CheckIfSimetric(MatA.mat, MatA.size)) { Console.WriteLine("Nu este simetric"); return; }
                double norma = norm_2(vec);
                for(int i = 0; i < size; i++)
                {
                    vec[i] = vec[i] / norma;
                }
                double[] w = mat_vec_mul(MatA.mat, vec, size);//matXvec
                double lmb = get_lmb(w, vec,size);
                int count = 0;
                double last_lmb = lmb;
                double[] last_vec = vec;//de verificat sa ia doar valoarea nu si referinta
                double[] rez = new double[size];
                while (true)
                {
                    count++;
                    var aux = 1.0 / norm_2(w);
                    for(int i = 0; i< size; i++)
                    {
                        vec[i] = aux * w[i];

                    }
                    w = mat_vec_mul(MatA.mat, vec, size);
                    lmb = get_lmb(w, vec,size);
                    for(int i = 0; i < size; i++)
                    {
                        rez[i] = -last_lmb * last_vec[i];
                    }
                    for(int i = 0; i < size; i++)
                    {
                        rez[i] += w[i];
                    }

                    if(norm_2(rez) < size * EPS || count > K_MAX)
                    {
                        break;
                    }
                    last_lmb = lmb; // de verificat sa ia doar valoarea nu si referinta
                    last_vec = vec;
                }

                Console.WriteLine(string.Format("lmb={0}\ncount={1}",lmb,count));
                Console.Write("V=[");
                for(int i = 0; i< size; i++)
                {
                    if (i != size)
                    {
                        Console.Write(vec[i] + ",");
                    }
                    else
                    {
                        Console.Write(vec[i] + "];");
                    }
                }

            }
            else if(p > size)
            {
                var MatAFile = File.ReadLines(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated.txt");
                var MatASizeFile = File.ReadLines(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated_size.txt");
                Matrice MatA = new Matrice(MatASizeFile, MatAFile);
                double[] valorisin = ValoriSingulare(MatA.mat, size);
                double detA = CalculateDet(MatA.mat, size);
                double nrConditionare = RaportNrConditionare(valorisin);
                double[,] S = CreateMatrixS(valorisin,p, size);
            }

            Console.ReadLine();
        }

        public static double[,] CreateMatrixS(double[] valorisin, int lin, int col)
        {
            double[,] result = new double[lin, col];
            for(int i = 0; i < lin; i++)
            {
                for(int j = 0; j< col; j++)
                {
                    if (i == j)
                    {
                        result[i, j] = valorisin[i];
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
            }
            return result;
        }

        public static double RaportNrConditionare(double[] valori)
        {
            double min = 999999;
            double max = -999999;
            foreach(var x in valori)
            {
                if(x > max)
                {
                    max = x;
                }
                if (x < min)
                {
                    min = x;
                }
            }
            double result = max / min;
            Console.WriteLine("Numar de conditionare =" + result);
            return result;
        }

        public static double CalculateDet(double[,] mat, int size)
        {
            double result = det(mat, size);
            Console.WriteLine("Determinantul matricii A = " + result);
            return result;
        }

        public static void Submatrice(int n, int lin, int col, double[,] a, double[,] b)
        {
            int i, j, c = 0, l = 1;
            for (i = 1; i <= n; i++)
                for (j = 1; j <= n; j++)
                    if (j != col && i != lin)
                    {
                        c++;
                        if (c == n)
                        {
                            c = 1;
                            l++;
                        }
                        b[l,c] = a[i,j];
                    }
        }

        public static double det(double[,] a,int n)
        {
            int i;
            double[,] b = new double[n,n];
            double s = 0;
            if(n==1) 
            { 
                s=a[1,1];
            }
            if(n==2) 
            {
                s=a[1,1]* a[2,2]-a[1,2]* a[2,1];
            }
            else 
            {
                for(i=1;i<=n;i++)
                {
                    Submatrice(n, 1, i, a, b);
                    s+=a[1,i]* Math.Pow(-1, i+1)* det(b, n-1);
                }
            }
            return s;
    }

        public static double[] ValoriSingulare(double[,] mat, int size)
        {
            double[] result = new double[size];
            for(int i = 0; i< size; i++)
            {
                if(mat[i,i] >= 0)
                {
                    result[i] = mat[i, i];
                }
            }
            Console.Write("Valori singulare = [");
            for(int i = 0; i< size; i++)
            {
                if(i != size - 1)
                {
                    Console.Write(result[i] + ", ");
                }
                else
                {
                    Console.Write(result[i] + "];");
                }
            }
            return result;
        }

        public static double get_lmb(double[] vec1, double[] vec2, int size)
        {
            double total = 0;
            for(int i = 0; i< size; i++)
            {
                total += vec1[i] * vec2[i];
            }
            return total;
        }

        public static double[] mat_vec_mul(double[,] mat, double[] vec, int size)
        {
            double[] result = new double[size];
            double total;

            for(int i = 0; i< size; i++)
            {
                total = 0;
                for(int j = 0; j< size; j++)
                {
                    total = total + (mat[i, j] * vec[j]);
                }
                result[i] = total;
            }
            return result;
        }

        public static double norm_2(double[] v)
        {
            double total = 0;
            foreach(var x in v)
            {
                total += x * x;
            }

            return Math.Pow(total, (1.0 / 2));
        }
        
        public static bool CheckIfSimetric(double[,] mat, int size)
        {
            for(int i = 0; i < size; i++)
            {
                for(int j = i; j < size; j++)
                {
                    if(mat[i,j] != mat[j, i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void CreateRandomVectorB(int size)
        {
            vec = new double[size];
            for (int i = 0; i < size; i++)
            {
                vec[i] = randomNr(size);
            }

            using (System.IO.StreamWriter file =
                new StreamWriter(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated_vector.txt"))
            {
                for (int i = 0; i < size; i++)
                {
                    file.WriteLine(vec[i]);
                }
            }
        }

        public static void CreateRandomMatrixA(int sizeMat)
        {
            double[,] mat = new double[sizeMat, sizeMat];

            for(int i = 0; i < sizeMat; i++)
            {
                for(int j = i; j < sizeMat; j++)
                {
                    mat[i, j] = randomNr(sizeMat);
                    mat[j, i] = mat[i, j];
                }
            }

            double[] vec = new double[3];
            int k = 0;
            using (System.IO.StreamWriter file =
                new StreamWriter(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated.txt"))
            {
                for (int i = 0; i < sizeMat; i++)
                {
                    for (int j = 0; j < sizeMat; j++)
                    {
                        vec[k] = mat[i, j];
                        k++;
                        if(k == 3)
                        {
                            k = 0;
                            file.WriteLine(vec[0] + ", " + vec[1] + ", " + vec[2]);
                        }
                    }
                }
            }
            System.IO.File.WriteAllText(@"D:\Faculty\Calcul Numeric\Tema5 DotNET\Tema5\Tema5\bin\Debug\netcoreapp2.2\generated_size.txt", sizeMat.ToString());
        }

        public static double randomNr(int max)
        {
            Random random = new Random();
            int k = 0;
            double x;
            while (k == 0) {
                x = random.NextDouble() * (max - 0) + 0 ;
                if (x != 0 && x > 0)
                {
                    k = 1;
                    return x;
                }
            }
            return 0;
        }
    }
}
