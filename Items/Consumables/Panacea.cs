using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Items.Consumables
{
    class Panacea : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Panacea");
            Tooltip.SetDefault("Must be used directly\nCures most debuffs\n30 second cooldown");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 30;
            item.rare = ItemRarityID.Pink;
            item.maxStack = 10;
            item.consumable = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 2;
            item.UseSound = SoundID.Item3;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<KeyPlayer>().PanaceaSickness;
        }
        public override bool UseItem(Player player)
        {
            player.ClearBuff(BuffID.Bleeding);
            player.ClearBuff(BuffID.Poisoned);
            player.ClearBuff(BuffID.OnFire);
            player.ClearBuff(BuffID.Venom);
            player.ClearBuff(BuffID.Darkness);
            player.ClearBuff(BuffID.Blackout);
            player.ClearBuff(BuffID.Cursed);
            player.ClearBuff(BuffID.Frostburn);
            player.ClearBuff(BuffID.Confused);
            player.ClearBuff(BuffID.Slow);
            player.ClearBuff(BuffID.Weak);
            player.ClearBuff(BuffID.Silenced);
            player.ClearBuff(BuffID.BrokenArmor);
            player.ClearBuff(BuffID.CursedInferno);
            player.ClearBuff(BuffID.Chilled);
            player.ClearBuff(BuffID.Ichor);
            player.ClearBuff(BuffID.ShadowFlame);
            player.ClearBuff(BuffID.Electrified);
            player.ClearBuff(BuffID.Rabies);
            player.ClearBuff(BuffID.VortexDebuff);
            player.ClearBuff(BuffID.WitheredArmor);
            player.ClearBuff(BuffID.WitheredWeapon);
            player.ClearBuff(BuffID.OgreSpit);
            player.ClearBuff(BuffID.Frozen);
            player.ClearBuff(BuffID.Stoned);
            player.ClearBuff(BuffID.Webbed);
            player.AddBuff(ModContent.BuffType<Buffs.PanaceaCD>(), 1800);
            return true;
        }
    }
}
