using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Consumables.MP
{
    class Ether : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Restores 25 MP\n" +
                "Reduces current MP Charge time by 25%\n" +
                "10 second cooldown");
        }
        public override void SetDefaults()
        {
            item.width = item.height = 20;
            item.rare = ItemRarityID.White;
            item.maxStack = 30;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseSound = SoundID.Item3;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<KeyPlayer>().EtherSickness;
        }
        public override bool UseItem(Player player)
        {
            CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            if (player.GetModPlayer<KeyPlayer>().rechargeMP)
                player.GetModPlayer<KeyPlayer>().rechargeMPTimer = (int)(player.GetModPlayer<KeyPlayer>().rechargeMPTimer * .75f);
            else
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
            if (player.GetModPlayer<KeyPlayer>().currentMP > player.GetModPlayer<KeyPlayer>().maxMP)
                player.GetModPlayer<KeyPlayer>().currentMP = player.GetModPlayer<KeyPlayer>().maxMP;
            player.AddBuff(ModContent.BuffType<Buffs.EtherSickness>(), 600);
            return true;
        }
    }
    class HiEther : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hi-Ether");
            Tooltip.SetDefault("Restores 75 MP\n" +
                "Reduces current MP Charge time by 50%\n" +
                "10 second cooldown");
        }
        public override void SetDefaults()
        {
            item.width = item.height = 20;
            item.rare = ItemRarityID.Blue;
            item.maxStack = 30;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseSound = SoundID.Item3;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<KeyPlayer>().EtherSickness;
        }
        public override bool UseItem(Player player)
        {
            CombatText.NewText(player.getRect(), Color.DodgerBlue, 75);
            if (player.GetModPlayer<KeyPlayer>().rechargeMP)
                player.GetModPlayer<KeyPlayer>().rechargeMPTimer = (int)(player.GetModPlayer<KeyPlayer>().rechargeMPTimer * .5f);
            else
                player.GetModPlayer<KeyPlayer>().currentMP += 75;
            if (player.GetModPlayer<KeyPlayer>().currentMP > player.GetModPlayer<KeyPlayer>().maxMP)
                player.GetModPlayer<KeyPlayer>().currentMP = player.GetModPlayer<KeyPlayer>().maxMP;
            player.AddBuff(ModContent.BuffType<Buffs.EtherSickness>(), 600);
            return true;
        }
    }
    class MegaEther : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega-Ether");
            Tooltip.SetDefault("Restores 150 MP\n" +
                "Reduces current MP Charge time by 75%\n" +
                "10 second cooldown");
        }
        public override void SetDefaults()
        {
            item.width = item.height = 20;
            item.rare = ItemRarityID.Orange;
            item.maxStack = 30;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseSound = SoundID.Item3;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<KeyPlayer>().EtherSickness;
        }
        public override bool UseItem(Player player)
        {
            CombatText.NewText(player.getRect(), Color.DodgerBlue, 150);
            if (player.GetModPlayer<KeyPlayer>().rechargeMP)
                player.GetModPlayer<KeyPlayer>().rechargeMPTimer = (int)(player.GetModPlayer<KeyPlayer>().rechargeMPTimer * .25f);
            else
                player.GetModPlayer<KeyPlayer>().currentMP += 150;
            if (player.GetModPlayer<KeyPlayer>().currentMP > player.GetModPlayer<KeyPlayer>().maxMP)
                player.GetModPlayer<KeyPlayer>().currentMP = player.GetModPlayer<KeyPlayer>().maxMP;
            player.AddBuff(ModContent.BuffType<Buffs.EtherSickness>(), 600);
            return true;
        }
    }
    class TurboEther : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Turbo-Ether");
            Tooltip.SetDefault("Fully recharges the MP gauge\n" +
                "Can only be used during MP Charge\n" +
                "30 second cooldown");
        }
        public override void SetDefaults()
        {
            item.width = item.height = 20;
            item.rare = ItemRarityID.Lime;
            item.maxStack = 10;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.UseSound = SoundID.Item84;
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().rechargeMP && !player.GetModPlayer<KeyPlayer>().TurboExhaustion && !player.GetModPlayer<KeyPlayer>().EtherSickness;
        }
        public override bool UseItem(Player player)
        {
            CombatText.NewText(player.getRect(), Color.DodgerBlue, player.GetModPlayer<KeyPlayer>().maxMP);
            player.GetModPlayer<KeyPlayer>().rechargeMP = false;
            player.GetModPlayer<KeyPlayer>().rechargeMPToastTimer = 60;
            player.GetModPlayer<KeyPlayer>().currentMP = player.GetModPlayer<KeyPlayer>().maxMP;
            player.GetModPlayer<KeyPlayer>().currentDelta = 0;
            player.AddBuff(ModContent.BuffType<Buffs.TurboExhaustion>(), 1800);
            player.AddBuff(ModContent.BuffType<Buffs.EtherSickness>(), 300);
            return true;
        }
    }
}
