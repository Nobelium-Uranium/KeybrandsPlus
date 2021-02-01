using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Consumables
{
    class Elixir : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elixir");
            Tooltip.SetDefault("Must be used directly, does not trigger Leaf Bracer\nFully restores life, mana, and MP\nGrants brief immunity to most debuffs\nBoosts life and mana regen for a short duration\n3 minute cooldown");
        }
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 18;
            item.rare = -12;
            item.maxStack = 3;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 2;
            item.UseSound = SoundID.Item6;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<KeyPlayer>().ElixirSickness;
        }
        public override bool UseItem(Player player)
        {
            player.HealEffect(player.statLifeMax2 - player.statLife);
            player.statLife = player.statLifeMax2;
            player.ManaEffect(player.statManaMax2 - player.statMana);
            player.statMana = player.statManaMax2;
            if (player.GetModPlayer<KeyPlayer>().rechargeMP)
            {
                CombatText.NewText(player.getRect(), Color.DodgerBlue, player.GetModPlayer<KeyPlayer>().maxMP);
                player.GetModPlayer<KeyPlayer>().rechargeMP = false;
                player.GetModPlayer<KeyPlayer>().rechargeMPToastTimer = 60;
            }
            else
                CombatText.NewText(player.getRect(), Color.DodgerBlue, player.GetModPlayer<KeyPlayer>().currentMP - player.GetModPlayer<KeyPlayer>().maxMP);
            player.GetModPlayer<KeyPlayer>().currentMP = player.GetModPlayer<KeyPlayer>().maxMP;
            player.AddBuff(ModContent.BuffType<Buffs.ElixirSickness>(), 10800);
            if (!player.HasBuff(BuffID.NebulaUpLife2) && !player.HasBuff(BuffID.NebulaUpLife3))
                player.AddBuff(BuffID.NebulaUpLife1, 600);
            if (!player.HasBuff(BuffID.NebulaUpMana2) && !player.HasBuff(BuffID.NebulaUpMana3))
                player.AddBuff(BuffID.NebulaUpMana1, 600);
            player.AddBuff(ModContent.BuffType<Buffs.AilmentGuard>(), 180);
            return true;
        }
    }
}
