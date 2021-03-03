using KeybrandsPlus.Globals;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class DraconicFlareExplosion : KeybrandProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Eruption");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 300;
            projectile.height = 300;
            projectile.aiStyle = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 70;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Dark = true;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 10)
            {
                projectile.GetGlobalProjectile<KeyProjectile>().Dark = false;
                projectile.GetGlobalProjectile<KeyProjectile>().Nil = false;
                Main.PlaySound(SoundID.Item62, projectile.position);
                projectile.Size = new Vector2(400);
                projectile.position -= new Vector2(50);
                for (int i = 0; i < 30; i++)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(0, 6.5f).RotatedBy(MathHelper.ToRadians(12) * i).RotatedByRandom(MathHelper.ToRadians(2.5f)) * Main.rand.NextFloat(.95f, 1.05f), ModContent.ProjectileType<DraconicFireball>(), projectile.damage / 2, projectile.knockBack, projectile.owner);
                }
                for (int k = 0; k < 50; k++)
                {
                    KeyUtils.NewDustCircular(out int Flame, projectile.Center, Vector2.Zero, ModContent.DustType<Dusts.DraconicFlame>(), 20, perfect: true);
                    Main.dust[Flame].scale *= 3.75f;
                }
            }
            else if (projectile.timeLeft > 10)
            {
                KeyUtils.NewDustCircular(out int Flame, projectile.Center, Vector2.Zero, ModContent.DustType<Dusts.DraconicFlame>(), Main.rand.NextFloat(2.5f, 5f));
                Main.dust[Flame].scale *= .75f;
                if (Main.rand.NextBool())
                    Main.dust[Flame].velocity *= 1.5f;
                for (int k = 0; k < Main.rand.Next(5, 10); k++)
                {
                    if (!Main.rand.NextBool(3))
                        KeyUtils.NewDustConverge(out Flame, projectile.Center, Vector2.Zero, 150, ModContent.DustType<Dusts.DraconicFlame>(), scale: 2f);
                    else
                        Dust.NewDust(projectile.Center + Main.rand.NextVector2CircularEdge(150, 150), 0, 0, ModContent.DustType<Dusts.DraconicFlame>());
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Collision.CanHit(projectile.Center, 0, 0, target.Center, 0, 0) && Vector2.Distance(target.Center, projectile.Center) < projectile.width / 2 && target.active && !target.dontTakeDamage && !target.friendly && target.lifeMax > 5;
        }
        public override bool CanHitPvp(Player target)
        {
            return Collision.CanHit(projectile.Center, 0, 0, target.Center, 0, 0) && Vector2.Distance(target.Center, projectile.Center) < projectile.width / 2 && target.active;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), 600);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss)
                target.velocity = Vector2.Normalize(KeyUtils.VectorTo(target.Center, projectile.Center)) * 10 * (target.knockBackResist < 0 ? 0 : target.knockBackResist);
            if (projectile.timeLeft <= 10)
                target.immune[projectile.owner] = 0;
            else
            {
                damage /= 2;
                target.immune[projectile.owner] /= 4;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.velocity = Vector2.Normalize(KeyUtils.VectorTo(target.Center, projectile.Center)) * 5;
            if (projectile.timeLeft <= 10)
                target.immuneTime /= 5;
            else
            {
                damage /= 2;
                target.immuneTime /= 3;
            }
        }
    }
}
