using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;

namespace KeybrandsPlus.Helpers
{
    public static class KeyUtils
    {
        public static int GetItemSlot(Player player, Item item)
        {
            Item i = null;
            for (int num = 0; num < player.inventory.Length; num++)
            {
                if (!player.inventory[num].IsAir)
                    i = player.inventory[num];
                if (item == i)
                    return num;
            }
            return -1;
        }

        public static bool InHotbar(Player player, Item item)
        {
            Item i = null;
            bool condition = false;
            for (int num = 0; num < 10; num++)
            {
                if (!player.inventory[num].IsAir)
                    i = player.inventory[num];
                if (item == i)
                    condition = true;
            }
            return condition;
        }
        
        public static bool RandPercent(float percent)
        {
            if (Main.rand.NextFloat(1f) <= percent)
                return true;
            return false;
        }

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

        public static float GetLerpValue(float from, float to, float t, bool clamped = false)
        {
            if (clamped)
            {
                if (from < to)
                {
                    if (t < from)
                    {
                        return 0f;
                    }
                    if (t > to)
                    {
                        return 1f;
                    }
                }
                else
                {
                    if (t < to)
                    {
                        return 1f;
                    }
                    if (t > from)
                    {
                        return 0f;
                    }
                }
            }
            return (t - from) / (to - from);
        }

        public static double GetLerpValue(double from, double to, double t, bool clamped = false)
        {
            if (clamped)
            {
                if (from < to)
                {
                    if (t < from)
                    {
                        return 0.0;
                    }
                    if (t > to)
                    {
                        return 1.0;
                    }
                }
                else
                {
                    if (t < to)
                    {
                        return 1.0;
                    }
                    if (t > from)
                    {
                        return 0.0;
                    }
                }
            }
            return (t - from) / (to - from);
        }
    }
}
