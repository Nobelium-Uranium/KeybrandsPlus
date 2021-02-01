using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Armor.Developer
{
    [AutoloadEquip(EquipType.Body)]
    public class AvaliShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chem's Avalispec Breastplate");
            Tooltip.SetDefault("'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 13;
            item.rare = 9;
            item.vanity = true;
        }
        public override bool DrawBody()
        {
            return false;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<Globals.KeyPlayer>().AvaliShirt = true;
        }
    }
}
