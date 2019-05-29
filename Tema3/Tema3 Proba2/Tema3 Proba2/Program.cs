using System;
using System.Collections.Generic;
using System.IO;

namespace CNTema3
{
    class MatriceRara
    {
        public List<List<Tuple<double, int>>> elemente;

        public MatriceRara()
        {
            elemente = new List<List<Tuple<double, int>>>();
        }

        public void GetDataFrom(double[,] simpleMatrix)
        {
            for (var i = 0; i < 5; i++)
            {
                var newRow = new List<Tuple<double, int>>();

                for (var j = 0; j < 5; j++)
                {
                    var cell = simpleMatrix[i,j];

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

    class Array
    {
        public double[,] arr;
        public double[] vec;
        public int size;
        public Array(IEnumerable<string> matSize, IEnumerable<string> matValue, IEnumerable<string> vecValue)
        {
            int i = 0;
            int j = 0;
            foreach(var si in matSize)
            {
                size = int.Parse(si) ;
            }

            arr = new double[size,size];
            vec = new double[size];

            foreach(var line in matValue)
            {
                var numbers = line.Split(", ");
                foreach (var number in numbers)
                {
                    arr[i, j] = double.Parse(number);
                    j++;
                    if (j == size)
                    {
                        i++;
                        j = 0;
                    }
                }
            }
            i = 0;
            foreach(var line in vecValue)
            {
                vec[i] = double.Parse(line);
                i++;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var fileNames = new[] { "a.txt","a_n.txt","a_vector.txt",
                "b.txt","b_n.txt","b_vector.txt",
                "aorib.txt","aorib_n.txt", "aorib_vector.txt",
                "aplusb.txt","aplusb_n.txt","aplusb_vector.txt" };
            var linesA_size = File.ReadLines(fileNames[1]);
            var linesA_mat = File.ReadLines(fileNames[0]);
            var linesA_vec = File.ReadLines(fileNames[2]);

            Array a = new Array(File.ReadLines(fileNames[1]), File.ReadLines(fileNames[0]), File.ReadLines(fileNames[2]));
            Array b = new Array(File.ReadLines(fileNames[4]), File.ReadLines(fileNames[3]), File.ReadLines(fileNames[5]));
            Array aOriB = new Array(File.ReadLines(fileNames[7]), File.ReadLines(fileNames[6]), File.ReadLines(fileNames[8]));
            Array aPlusB = new Array(File.ReadLines(fileNames[10]), File.ReadLines(fileNames[9]), File.ReadLines(fileNames[11]));

            var ex = new double[5, 5]
            {
                { 102.5, 0.0, 2.5, 0.0, 0.0 },
                { 3.5, 104.88, 1.05, 0.0, 0.33 },
                { 0.0, 0.0, 100.0, 0.0, 0.0 },
                { 0.0, 1.3, 0.0, 101.3, 0.0 },
                { 0.73, 0.0, 0.0, 1.5, 102.23 }
            };

            var matricerara = new MatriceRara();

            matricerara.GetDataFrom(ex);

            //foreach (var x in matricerara.elemente)
            //{
            //    foreach (var y in x)
            //    {
            //        console.write(y + " ");
            //    }

            //    console.writeline();
            //}
            int k = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(ex[i,j] + " + " + ex[i,j] + " = " + ex[i,j] + ex[i,j]);
                    //if (a.arr[i, j] + b.arr[i, j] != aPlusB.arr[i, j])
                    //{
                    //    Console.WriteLine(a.arr[i, j] + " + " + b.arr[i, j] + " comparat cu " + aPlusB.arr[i, j]);
                    //    Console.WriteLine("NotEqual");
                    //    k = 1;
                    //    break;
                    //}
                }
                //if (k == 1)
                //{
                //    break;
                //}
            }
            //int k = 0;
            for(int i = 0; i< a.size; i++)
            {
                for(int j = 0; j< b.size; j++)
                {
                    if (a.arr[i, j] + b.arr[i, j] - aPlusB.arr[i, j] != 10*-10000)
                    {
                        Console.WriteLine(a.arr[i, j] + " + " + b.arr[i, j] + " comparat cu " + aPlusB.arr[i,j]);
                        Console.WriteLine("NotEqual");
                        k = 1;
                        break;
                    }
                }
                if (k == 1)
                {
                    break;
                }
            }

            var cmpMat = new double[a.size, a.size];
            for(int i = 0; i< a.size; i++)
            {
                for(int j = 0; j< b.size; j++)
                {
                    cmpMat[i, j] = 0;
                    for(int p = 0; p < a.size; p++)
                    {
                        cmpMat[i,j] += a.arr[i, k] * b.arr[k, j];
                    }
                }
            }
            k = 0;
            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < aOriB.size; j++)
                {
                    if (cmpMat[i, j] != aOriB.arr[i, j])
                    {
                        k = 1;
                        Console.WriteLine("NotEqual");
                        break;
                    }
                }
                if (k == 1)
                {
                    break;
                }
            }
            if (k == 0)
            {
                Console.WriteLine("Equal");
            }

            double[,] vecAoriArrA = new double[2019,1];
            for(int i = 0; i< a.size; i++)
            {
                for(int j = 0; j < 1; j++)
                {
                    vecAoriArrA[i, j] = 0;
                    for(int p = 0; p < 1; p++)
                    {
                        vecAoriArrA[i, j] += a.arr[i, p] * a.vec[j];
                    }
                }
            }

        }
    }
}