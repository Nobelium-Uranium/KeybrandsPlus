using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Helpers
{
    public static class KeyUtils
    {
        /// <summary>
        /// Spawn an item and manually send a net message, use like Item.NewItem.
        /// Should only be used if a hook doesn't manually sync across clients.
        /// </summary>
        public static void NewSyncedItem(Vector2 pos, int type, int stack = 1)
        {
            int item = Item.NewItem(pos, type, stack, false);
            if (Main.netMode == 1 && item >= 0)
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
        }

        /// <summary>
        /// Spawn an item and manually send a net message, use like Item.NewItem.
        /// Should only be used if a hook doesn't manually sync across clients.
        /// </summary>
        public static void NewSyncedItem(Rectangle rect, int type, int stack = 1)
        {
            int item = Item.NewItem(rect, type, stack, false);
            if (Main.netMode == 1 && item >= 0)
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
        }

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
        /// See ExampleMod's Wisp projectile on how to use
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="max"></param>
        public static void AdjustMagnitude(ref Vector2 vector, float max)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > max)
            {
                vector *= max / magnitude;
            }
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

        public static bool HasItemSpace(Player player, Item item = null)
        {
            bool Condition = false;
            for (int num = 0; num < player.inventory.Length; num++)
            {
                if (player.inventory[num].IsAir)
                {
                    Condition = true;
                    break;
                }
                if (item != null)
                {
                    int itemType = item.type;
                    if (player.inventory[num].type == item.type && item.stack < player.inventory[num].maxStack)
                    {
                        Condition = true;
                        break;
                    }
                }
            }
            return Condition;
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
        /// I find this easier to use than Main.rand.nextFloat. Range is between 0f and 1f.
        /// </summary>
        public static bool RandPercent(float percent)
        {
            if (percent <= 0f) //Skip calculation altogether if value is less than or equal to its minimum amount
                return false;
            else if (percent >= 1f) //Skip calculation altogether if value is more than or equal to its maximum amount
                return true;
            else if (Main.rand.NextFloat(1f) <= percent) //Otherwise calculate as normal
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

    public class CheatModeCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "kplus_cheatmode";

        public override string Description => "Do you know the rules?";

        public override string Usage => "This command is obfuscated.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (int.Parse(args[0]) == Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().StoredUUIDX + Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().StoredUUIDY * Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().StoredUUIDZ)
            {
                caller.Reply("You clever little sneak!");
                if (!Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().CheatMode)
                    Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().CheatMode = true;
                else
                    Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().CheatMode = false;
            }
            else
                caller.Reply("Insufficient arguments.", Color.Red);
        }
    }

    public class SetUUIDCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "kplus_setuuid";

        public override string Description => "Debug command. Changes your UUID to something else. Requires Cheat Mode to be enabled.";

        public override string Usage => "/kplus_setuuid x y z (All arguments must range from 0 to 255)";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().CheatMode)
            {
                if (int.Parse(args[0]) < 255 && int.Parse(args[0]) > 0 && int.Parse(args[1]) < 255 && int.Parse(args[1]) > 0 && int.Parse(args[2]) < 255 && int.Parse(args[2]) > 0)
                {
                    caller.Reply("Changed UUID successfully.");
                    Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().StoredUUIDX = int.Parse(args[0]);
                    Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().StoredUUIDY = int.Parse(args[1]);
                    Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().StoredUUIDZ = int.Parse(args[2]);
                    Main.LocalPlayer.GetModPlayer<Globals.KeyPlayer>().UUID = new Vector3(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
                }
                else
                    caller.Reply("One or more integers are invalid.", Color.Red);
            }
            else
                caller.Reply("Insufficient permissions.", Color.Red);
        }
    }
}
