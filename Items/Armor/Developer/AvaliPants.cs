using KeybrandsPlus.Globals;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Armor.Developer
{
    [AutoloadEquip(EquipType.Legs)]
    public class AvaliPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avalispec Keybrand Greaves");
            Tooltip.SetDefault("'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 13;
            item.rare = 9;
            item.vanity = true;
            item.GetGlobalItem<KeyRarity>().DeveloperRarity = true;
            item.GetGlobalItem<KeyRarity>().DeveloperName = "ChemAtDark";
        }
        public override bool DrawLegs()
        {
            return false;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<KeyPlayer>().AvaliPants = true;
        }
    }
}
