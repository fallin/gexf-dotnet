using System;

namespace DotNet.Gexf
{
    public static class GexfFloat
    {
        public const float FloatEpsilon = 0.000001f;

        /// <summary>
        /// Indicates whether two floating-point values are approximately equal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Equal(float x, float y)
        {
            return Math.Abs(x - y) < FloatEpsilon;
        }
    }
}