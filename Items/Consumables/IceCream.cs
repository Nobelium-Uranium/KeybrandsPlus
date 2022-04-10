using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Consumables
{
    class IceCream : ModItem
    {
        private bool PickupTimer = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sea-Salt Ice Cream");
            Tooltip.SetDefault("Minor improvements to all stats");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(16);
            item.rare = ItemRarityID.Cyan;
            item.maxStack = 99;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseSound = SoundID.Item2;
            item.buffType = BuffID.WellFed;
            item.buffTime = 162000;
            item.value = Item.sellPrice(gold: 5);
        }
        public override bool UseItem(Player player)
        {
            if (Main.rand.Next(50) == 0)
                player.QuickSpawnItem(ModContent.ItemType<Other.WinnerStick>());
            return true;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (!PickupTimer)
                PickupTimer = true;
        }
        public override bool CanPickup(Player player)
        {
            return PickupTimer;
        }
    }
}
