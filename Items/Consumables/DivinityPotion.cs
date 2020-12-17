using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Consumables
{
    class DivinityPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% increased magic keybrand damage\n" +
                "40% reduced physical keybrand damage and defense");
        }
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 18;
            item.rare = ItemRarityID.LightPurple;
            item.maxStack = 30;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseSound = SoundID.Item3;
            item.buffType = ModContent.BuffType<Buffs.Divinity>();
            item.buffTime = 18000;
        }
        public override bool UseItem(Player player)
        {
            player.ClearBuff(ModContent.BuffType<Buffs.Stimulated>());
            return true;
        }
    }
}
