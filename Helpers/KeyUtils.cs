using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Helpers
{
    public static class KeyUtils
    {
        public static Vector2 ClosestPointInRect(Rectangle r, Vector2 point)
        {
            Vector2 vector = point;
            if (vector.X < (float)r.Left)
            {
                vector.X = (float)r.Left;
            }
            if (vector.X > (float)r.Right)
            {
                vector.X = (float)r.Right;
            }
            if (vector.Y < (float)r.Top)
            {
                vector.Y = (float)r.Top;
            }
            if (vector.Y > (float)r.Bottom)
            {
                vector.Y = (float)r.Bottom;
            }
            return vector;
        }
    }
}
