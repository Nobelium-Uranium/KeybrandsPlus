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
        bool Primed;
        int Circle;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Flare");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 255;
            projectile.aiStyle = 0;
            projectile.tileCollide = true;
            projectile.timeLeft = 900;
            projectile.extraUpdates += 1;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Dark = true;
            Circle = 45;
        }
        public override void AI()
        {
            Player Owner = Main.player[projectile.owner];
            if (projectile.timeLeft > 750)
            {
                int Flame = Dust.NewDust(projectile.Center + projectile.velocity, 0, 0, ModContent.DustType<Dusts.DraconicFlame>());
                Main.dust[Flame].scale *= .75f;
                if (Main.rand.NextBool())
                    Main.dust[Flame].velocity /= 2;
            }
            if (projectile.timeLeft == 750)
                for (int i = 0; i < 20; i++)
                {
                    int Flame = Dust.NewDust(projectile.Center + projectile.velocity, 0, 0, ModContent.DustType<Dusts.DraconicFlame>());
                    Main.dust[Flame].velocity *= 2.5f;
                }
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 vectorTo = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(vectorTo.X * vectorTo.X + vectorTo.Y * vectorTo.Y);
                    if (Collision.CanHit(projectile.Center, 0, 0, Main.npc[k].Center, 0, 0) && distanceTo < 80f)
                        projectile.Kill();
                }
            }
            if (Owner.hostile)
                for (int i = 0; i < 200; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && !p.immune && p.immuneTime <= 0 && p != Owner && p.hostile && (p.team != Owner.team || p.team == 0))
                    {
                        Vector2 vectorTo = p.Center - projectile.Center;
                        float distanceTo = (float)Math.Sqrt(vectorTo.X * vectorTo.X + vectorTo.Y * vectorTo.Y);
                        if (Collision.CanHit(projectile.Center, 0, 0, p.Center, 0, 0) && distanceTo < 80f)
                        {
                            projectile.Kill();
                            projectile.netUpdate = true;
                        }
                    }
                }
            if (projectile.timeLeft <= 840)
            {
                projectile.velocity = Vector2.Zero;
                if (projectile.timeLeft == 750)
                {
                    Main.PlaySound(SoundID.MaxMana, projectile.Center);
                    Primed = true;
                }
                if (Circle++ >= 90)
                    Circle = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (Collision.CanHit(projectile.Center, 0, 0, projectile.Center + Vector2.UnitY.RotatedBy((MathHelper.PiOver2 * i) + Circle * Math.PI / 180) * 80f, 0, 0))
                    {
                        int Indicator = Dust.NewDust(projectile.Center + Vector2.UnitY.RotatedBy((MathHelper.PiOver2 * i) + Circle * Math.PI / 180) * 80, 0, 0, ModContent.DustType<Dusts.DraconicFlame>());
                        Main.dust[Indicator].velocity = Vector2.Zero;
                        if (projectile.timeLeft <= 750)
                            Main.dust[Indicator].scale *= .25f;
                        else
                            Main.dust[Indicator].scale *= .5f;
                    }
                }
            }
            else
                projectile.velocity *= .96f;
            if (projectile.timeLeft == 1)
                projectile.damage = (int)(projectile.damage * 1.25f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                projectile.velocity.X = oldVelocity.X * -.25f;
            }
            if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                projectile.velocity.Y = oldVelocity.Y * -.25f;
            }
            return false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item74, projectile.position);
            if (Primed)
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DraconicFlareExplosion>(), projectile.damage, projectile.knockBack / 2, projectile.owner);
            else
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DraconicFlareExplosion>(), projectile.damage / 3, projectile.knockBack / 2, projectile.owner);
        }
    }
}
