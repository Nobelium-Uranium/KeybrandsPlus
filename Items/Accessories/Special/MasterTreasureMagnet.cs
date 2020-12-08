using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Accessories.Special
{
    class MasterTreasureMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Vastly increases Munny pickup range");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(28);
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            return !player.GetModPlayer<KeyPlayer>().TreasureMagnet && !player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet = true;
        }
    }
}
