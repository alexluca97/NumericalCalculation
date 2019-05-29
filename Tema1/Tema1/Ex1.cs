using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class Ex1
    {
        public double Start()
        {
            double result = 0;
            int m = 1;
            while (1 + 1 / Math.Pow(10, m) != 1)
            {
                m++;
                result = 1 / Math.Pow(10, m);
            }
            return result;
        }
    }
}
