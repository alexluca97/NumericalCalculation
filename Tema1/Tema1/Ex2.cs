using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class Ex2
    {
        public double x = 1.0;
        public double y;
        public double z;

        public bool StartSum(double u)
        {

            y = u;
            z = u;
            if ((x + y) + z != x + (y + z))
            {
                return false;
            }
            return true;
        }

        public bool StartMultiply(double u)
        {
            y = u;
            z = u;
            if((x*y)*z != x * (y * z))
            {
                return false;
            }
            return true;
        }
    }
}
