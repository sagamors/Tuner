using System;

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
