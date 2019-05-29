using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2
{
    class Program
    {
        static void Main(string[] args)
        {
            Ex1 ex1 = new Ex1();
            ex1.DescompunereLU();
            ex1.printMatrix();
            Console.WriteLine();
            ex1.DetA();
            Console.WriteLine("\n\n\n");
            ex1.Subtitution("L");
            Console.WriteLine("\n\n\n");
            ex1.Subtitution("U");
            Console.WriteLine("\n\n");
            ex1.norma();
            //ex1.Invers();
        }
    }
}
