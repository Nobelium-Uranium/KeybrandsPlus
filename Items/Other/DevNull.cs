using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Other
{
    class DevNull : ModItem
    {
        public override void UpdateInventory(Player player)
        {
            item.SetDefaults(0, false);
            if (player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure")
            {
                player.QuickSpawnItem(ModContent.ItemType<FullbrightDye>());
                player.QuickSpawnItem(ModContent.ItemType<Accessories.Wings.AvaliGlider>());
                player.QuickSpawnItem(ModContent.ItemType<Armor.Developer.AvaliPants>());
                player.QuickSpawnItem(ModContent.ItemType<Armor.Developer.AvaliShirt>());
                player.QuickSpawnItem(ModContent.ItemType<Armor.Developer.AvaliHelmet>());
            }
        }
    }
}
