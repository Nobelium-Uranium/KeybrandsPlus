using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class DraconicFireball : KeybrandProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Debris");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 255;
            projectile.aiStyle = 1;
            projectile.timeLeft = 600;
            projectile.extraUpdates += 1;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Dark = true;
        }
        public override void AI()
        {
            projectile.alpha -= 10;
            if (projectile.alpha < 50)
            {
                projectile.alpha = 50;
            }
            if (Main.rand.NextBool())
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Dusts.DraconicFlame>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * ((255f - (float)projectile.alpha) / 255f);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), Main.rand.Next(45, 90));
        }
        public override bool? CanHitNPC(NPC target)
        {
            return projectile.alpha <= 50 && !target.friendly;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item89, projectile.position);
            int num4;
            for (int num582 = 0; num582 < 20; num582 = num4 + 1)
            {
                int num583 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DraconicFlame>(), (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f);
                Main.dust[num583].noGravity = true;
                Dust dust = Main.dust[num583];
                dust.velocity *= 2f;
                num583 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DraconicFlame>(), (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f);
                dust = Main.dust[num583];
                dust.velocity *= 2f;
                num4 = num582;
            }
        }
    }
}
