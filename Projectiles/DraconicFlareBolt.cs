using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class DraconicFlareBolt : KeybrandProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Flare");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 12;
            projectile.height = 12;
            projectile.alpha = 255;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
            projectile.timeLeft = 60;
            projectile.extraUpdates += 1;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Dark = true;
        }
        public override void AI()
        {
            for (int k = 0; k < Main.rand.Next(3, 7); k++)
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Dusts.DraconicFlame>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 vectorTo = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(vectorTo.X * vectorTo.X + vectorTo.Y * vectorTo.Y);
                    if (distanceTo < 65f)
                        projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item74, projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DraconicFlareExplosion>(), (int)(projectile.damage * 0.6), projectile.knockBack, projectile.owner);
            for (int k = 0; k < Main.rand.Next(5, 8); k++)
            {
                float RandX = Main.rand.NextFloat(3, 5);
                if (Main.rand.NextBool())
                    RandX *= -1;
                float RandY = Main.rand.NextFloat(3, 5);
                if (Main.rand.NextBool())
                    RandY *= -1;
                Vector2 RandVelocity = new Vector2(RandX, RandY);
                Projectile.NewProjectile(projectile.Center, RandVelocity, ModContent.ProjectileType<DraconicFireball>(), (int)(projectile.damage * 0.3), projectile.knockBack / 2, projectile.owner);
            }
        }
    }
}
