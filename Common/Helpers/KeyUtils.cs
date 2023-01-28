using KeybrandsPlus.Content.Items.Currency;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Helpers
{
    public sealed class KeyUtils
    {
        public static bool HasSpaceForMunny(Player player)
        {
            if (player.preventAllItemPickups)
                return false;
            for (int i = 0; i < 50; i++)
            {
                Item slot = player.inventory[i];
                if (slot.IsAir || (slot.type == ModContent.ItemType<Munny>() && slot.stack < slot.maxStack) || CofveveSpaceForMunny(player))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CofveveSpaceForMunny(Player player)
        {
            if (!player.IsVoidVaultEnabled)
                return false;
            Item[] vault = player.bank4.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.IsAir || (item.type == ModContent.ItemType<Munny>() && item.stack < item.maxStack))
                    return true;
            }
            return false;
        }
        public static void FloatOnWater(Projectile projectile, out bool floating, float buoyancy = .5f, float acc = .1f, bool water = true, bool honey = true, bool lava = true)
        {
            if ((water && projectile.wet && !projectile.honeyWet && !projectile.lavaWet) || (honey && projectile.honeyWet) || (lava && projectile.lavaWet))
            {
                floating = true;
                projectile.aiStyle = 0;
                if (projectile.velocity.Y > 0f)
                    projectile.velocity.Y *= buoyancy;
                int posX = (int)(projectile.Center.X / 16f);
                int posY = (int)(projectile.Center.Y / 16f);
                float waterLine = GetWaterLine(projectile, posX, posY);
                if (projectile.Center.Y > waterLine)
                {
                    projectile.velocity.Y -= acc;
                    if (projectile.velocity.Y < -8f)
                        projectile.velocity.Y = -8f;
                    if (projectile.Center.Y + projectile.velocity.Y < waterLine)
                        projectile.velocity.Y = waterLine - projectile.Center.Y;
                }
            }
            else
                floating = false;
        }
        public static float GetWaterLine(Projectile projectile, int x, int y)
        {
            float result = projectile.position.Y + (float)projectile.height;
            if (Main.tile[x, y - 1] != null && Main.tile[x, y - 1].LiquidAmount > 0)
            {
                result = (float)(y * 16f);
                result -= (float)(Main.tile[x, y - 1].LiquidAmount / 16f);
            }
            else if (Main.tile[x, y] != null && Main.tile[x, y].LiquidType == LiquidID.Water)
            {
                result = (float)((y + 1) * 16f);
                result -= (float)(Main.tile[x, y].LiquidAmount / 16f);
            }
            else if (Main.tile[x, y + 1] != null && Main.tile[x, y + 1].LiquidType == LiquidID.Water)
            {
                result = (float)((y + 2) * 16f);
                result -= (float)(Main.tile[x, y + 1].LiquidAmount / 16f);
            }
            return result;
        }
    }
}
