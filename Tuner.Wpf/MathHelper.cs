using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Wpf
{
    public static class MathHelper
    {
        public static double ConvertToRads(double grad)
        {
            return grad * Math.PI / 180;
        }
    }
}
