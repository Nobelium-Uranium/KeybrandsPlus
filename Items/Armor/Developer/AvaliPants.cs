using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Armor.Developer
{
    [AutoloadEquip(EquipType.Legs)]
    public class AvaliPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chem's Avalispec Greaves");
            Tooltip.SetDefault("'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 13;
            item.rare = 9;
            item.vanity = true;
        }
        public override bool DrawLegs()
        {
            return false;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<Globals.KeyPlayer>().AvaliPants = true;
        }
    }
}
