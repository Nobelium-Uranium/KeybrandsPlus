using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;

namespace KeybrandsPlus.Helpers
{
    public static class KeyUtils
    {
        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        /// <summary>
        /// Simple maths really
        /// </summary>
        /// <returns></returns>
        public static Vector2 VectorTo(Vector2 target, Vector2 destination)
        {
            return destination - target;
        }

        public static Dust NewDustConverge(out int dustIndex, Vector2 center, Vector2 size, float distance, int type, int alpha = 0, Color color = default, float scale = 1f, bool fixedScale = true)
        {
            int index = Dust.NewDust(center, (int)size.X, (int)size.Y, type, 0, 0, alpha, color, scale);
            Dust dust = Main.dust[index];
            dust.noGravity = true;
            dust.fadeIn = scale * 1.25f;
            if (fixedScale)
                dust.scale = scale;
            Vector2 positionOffset = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
            positionOffset.Normalize();
            positionOffset *= Main.rand.NextFloat(2, 4);
            positionOffset.Normalize();
            positionOffset *= distance;
            dust.position = center - positionOffset;
            Vector2 newVelocity = center - dust.position;
            dust.velocity = newVelocity * .1f;
            dustIndex = index;
            return dust;
        }

        /// <summary>
        /// Creates the specified dust in a circular fashion.
        /// </summary>
        /// <param name="dustIndex">An output of the created dust's index, to be used with Main.dust[dustIndex]</param>
        /// <param name="perfect">Determines if the velocity is set or semi-random</param>
        /// <returns></returns>
        public static Dust NewDustCircular(out int dustIndex, Vector2 position, Vector2 size, int type, float velocity, int alpha = 0, Color color = default, float scale = 1f, bool perfect = false)
        {
            int index = Dust.NewDust(position, (int)size.X, (int)size.Y, type, 0, 0, alpha, color, scale);
            if (perfect)
                Main.dust[index].velocity = Vector2.Normalize(Main.dust[index].velocity).RotatedByRandom(MathHelper.ToRadians(360)) * velocity;
            else
                Main.dust[index].velocity += new Vector2(0, velocity).RotatedByRandom(MathHelper.ToRadians(360));
            dustIndex = index;
            return Main.dust[index];
        }


        /// <summary>
        /// Gets the specified item's slot ID.
        /// </summary>
        /// <param name="player">The player whose inventory will be iterated through</param>
        /// <param name="item">An item in the specified player's inventory</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns whether or not an item is in the specified player's hotbar. Similar to GetItemSlot but only returns a boolean value that is only true if in the hotbar.
        /// </summary>
        /// <param name="player">The player whose inventory will be iterated through</param>
        /// <param name="item">An item in the specified player's inventory</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// I find this easier to use than Main.rand.nextFloat
        /// </summary>
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
