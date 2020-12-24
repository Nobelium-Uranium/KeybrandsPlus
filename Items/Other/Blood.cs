using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Items.Other
{
    class Blood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("If you somehow got this in your inventory, it's a bug\nPlease let the mod developer know about this\n...Unless you got it via HERO's Mod, Cheat Sheet or the such, you cheater");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(5);
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.buffType = BuffType<Buffs.ChimeraBleed>();
            item.buffTime = 3600;
        }

        public override void PostUpdate()
        {
            if (item.wet)
                item.active = false;
            int blood = Dust.NewDust(item.Center, 0, 0, DustID.Blood);
            Main.dust[blood].velocity += item.velocity;
            Main.dust[blood].velocity /= 2;
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 3;
        }
        public override bool GrabStyle(Player player)
        {
            Vector2 vectorItemToPlayer = player.Center - item.Center;
            Vector2 movement = vectorItemToPlayer.SafeNormalize(default) * 10f;
            item.velocity = movement;
            return true;
        }
        public override bool ItemSpace(Player player)
        {
            return true;
        }
        public override bool OnPickup(Player player)
        {
            Main.PlaySound(SoundID.Item111.WithVolume(0.5f), player.Center);
            player.statLife += 1;
            CombatText.NewText(player.getRect(), CombatText.HealLife, 1, dot: true);
            return false;
        }
    }
}
