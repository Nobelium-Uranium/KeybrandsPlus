using KeybrandsPlus.Content.Items.Currency;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Helpers
{
    public sealed class KeyUtils
    {
        public static bool HasSpaceForMunny(Player player)
        {
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
    }
}
