using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Unused
{
    class PGI : ModItem
    {
        private bool condition;
        private int slot;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfectly Generic Item");
            Tooltip.SetDefault("Debugging tool\n'You shouldn't have this'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = item.useAnimation = 20;
        }
        public override void UpdateInventory(Player player)
        {
            /*condition = KeyUtils.InHotbar(player, item);
            slot = KeyUtils.GetItemSlot(player, item);
            Main.NewText("This item is" + (!condition ? " not" : "") + " in the hotbar" + (slot != -1 ? " in position " + slot : "."));*/
        }
        public override bool CanRightClick() { return true; }
        public override void RightClick(Player player)
        {
            if (player.GetModPlayer<KeyPlayer>().maxMP < 300)
            {
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                Main.NewText("Max MP increased to " + player.GetModPlayer<KeyPlayer>().maxMP);
            }
            else
            {
                player.GetModPlayer<KeyPlayer>().maxMP = 100;
                Main.NewText("Max MP reduced to 100");
            }
        }
    }
}
