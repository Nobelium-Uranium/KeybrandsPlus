using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Weapons
{
    class FlameLiberator : Helpers.Keybrand
    {
        public override Texture2D GlowmaskTexture => mod.GetTexture("Textures/Glowmasks/FlameLiberator");

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+5 Light Alignment\n" +
                "-5 Dark Alignment\n" +
                "Alt Attack: Eternal Flames\n" +
                "Throws up to two flaming chakrams straight ahead\n" +
                "The chakrams inflict a stacking fire debuff\n" +
                "No Abilities\n" +
                "'Got it memorized?'");
        }
        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 33;
            item.useTime = 33;
            item.knockBack = 6.2f;
            item.width = 40;
            item.height = 40;
            item.damage = 46;
            item.scale = 1.1f;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Orange;
            item.melee = true;
            item.shootSpeed = 15f;
            item.GetGlobalItem<KeyItem>().Fire = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.damage = 46;
                item.knockBack = 6.2f;
                item.melee = true;
                item.ranged = false;
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useTurn = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shoot = 0;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.damage = 31;
                item.knockBack = 2.4f;
                item.melee = false;
                item.ranged = true;
                item.useTurn = false;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shoot = ModContent.ProjectileType<Projectiles.EternalFlames>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.UseSound = SoundID.Item116;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.EternalFlames>()] <= 1;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment += 5;
            player.GetModPlayer<KeyPlayer>().DarkAlignment -= 5;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.NextBool(10))
                target.AddBuff(ModContent.BuffType<Buffs.EternalBlaze>(), 180);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(1))
            {
                int dustIndex = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
                Main.dust[dustIndex].noGravity = true;
                int dustIndex2 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 127, Scale: 2f);
                Main.dust[dustIndex2].position -= new Vector2(4);
                Main.dust[dustIndex2].noGravity = true;
            }
        }
    }
}
