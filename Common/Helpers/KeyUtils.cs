using KeybrandsPlus.Content.Items.Currency;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Helpers
{
    public sealed class KeyUtils
    {
        public static bool HasSpaceForMunny(Player player, int amount, out bool goingIntoVault, out int remainder, bool checkVault = true)
        {
            goingIntoVault = false;
            remainder = 0;
            if (player.preventAllItemPickups)
                return false;
            for (int i = 0; i < 50; i++)
            {
                Item slot = player.inventory[i];
                if (slot.IsAir)
                {
                    return true;
                }
                else if (slot.type == ModContent.ItemType<Munny>() && slot.stack < slot.maxStack)
                {
                    if (slot.stack + amount > slot.maxStack)
                        remainder = slot.stack + amount - slot.maxStack;
                    return true;
                }
                if (checkVault && CofveveSpaceForMunny(player))
                {
                    goingIntoVault = true;
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
        /// <summary>
        /// Makes an entity float on liquids
        /// Despite the name of the method, this will also work with honey and lava if enabled
        /// </summary>
        /// <param name="entity">The entity that will float</param>
        /// <param name="floating">Outputs whether or not the projectile is floating on a liquid</param>
        /// <param name="buoyancy">Bigger numbers means the projectile is slowed less from falling into liquids</param>
        /// <param name="acc">How quickly the projectile will float to the surface</param>
        /// <param name="maxAcc">The max velocity a floating projectile travels upward</param>
        /// <param name="water">Does the projectile float in water?</param>
        /// <param name="honey">Does the projectile float in honey?</param>
        /// <param name="lava">Does the projectile float in lava?</param>
        public static void FloatOnWater(Entity entity, out bool floating, float buoyancy = .5f, float acc = .1f, float maxAcc = 8f, bool water = true, bool honey = true, bool lava = true)
        {
            if ((water && entity.wet && !entity.honeyWet && !entity.lavaWet) || (honey && entity.honeyWet) || (lava && entity.lavaWet))
            {
                floating = true;
                if (entity.velocity.Y > 0f)
                    entity.velocity.Y *= buoyancy;
                int posX = (int)(entity.Center.X / 16f);
                int posY = (int)(entity.Center.Y / 16f);
                float waterLine = GetWaterLine(entity, posX, posY);
                if (entity.Center.Y > waterLine)
                {
                    entity.velocity.Y -= acc;
                    if (entity.velocity.Y < -maxAcc)
                        entity.velocity.Y = -maxAcc;
                    if (entity.Center.Y + entity.velocity.Y < waterLine)
                        entity.velocity.Y = waterLine - entity.Center.Y;
                }
            }
            else
                floating = false;
        }
        public static float GetWaterLine(Entity entity, int x, int y)
        {
            float result = entity.position.Y + (float)entity.height;
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
