using System;
using System.Numerics;
using SFML.System;

namespace Saffron2D.Utils
{
    public static class VecUtils
    {
        public static float Length(Vector2f vector)
        {
            return (float) Math.Sqrt(LengthSq(vector));
        }

        public static float LengthSq(Vector2f vector)
        {
            return (float) (Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }
    }
}