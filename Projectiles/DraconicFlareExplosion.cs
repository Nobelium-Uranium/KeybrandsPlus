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
            projectile.width = 225;
            projectile.height = 225;
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
                projectile.damage = (int)(projectile.damage * 1.5f);
                Main.PlaySound(SoundID.Item62, projectile.position);
                projectile.Size = new Vector2(300);
                projectile.position -= new Vector2(37.5f);
                for (int i = 0; i < 30; i++)
                {
                    int debris = Projectile.NewProjectile(projectile.Center, new Vector2(0, 6.5f).RotatedBy(MathHelper.ToRadians(12) * i).RotatedByRandom(MathHelper.ToRadians(2.5f)) * Main.rand.NextFloat(.95f, 1.05f), ModContent.ProjectileType<DraconicFireball>(), projectile.damage / 2, projectile.knockBack, projectile.owner);
                    Main.projectile[debris].scale *= 1.25f;
                    Main.projectile[debris].Size *= 1.25f;
                }
                for (int k = 0; k < 50; k++)
                {
                    KeyUtils.NewDustCircular(out int Flame, projectile.Center, Vector2.Zero, ModContent.DustType<Dusts.DraconicFlame>(), 20, perfect: true);
                    Main.dust[Flame].scale *= 3.75f;
                }
            }
            else if (projectile.timeLeft > 10)
            {
                for (int k = 0; k < Main.rand.Next(5, 10); k++)
                {
                    KeyUtils.NewDustCircular(out int Flame, projectile.Center, Vector2.Zero, ModContent.DustType<Dusts.DraconicFlame>(), 15);
                }
                float RandX = Main.rand.NextFloat(1.5f, 3.5f);
                if (Main.rand.NextBool())
                    RandX *= -1;
                RandX *= Main.rand.NextFloat(.5f, 1.5f);
                float RandY = Main.rand.NextFloat(1.5f, 3.5f);
                if (Main.rand.NextBool())
                    RandY *= -1;
                RandY *= Main.rand.NextFloat(.5f, 1.5f);
                Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(30);
                if (Main.rand.NextBool(3))
                    Projectile.NewProjectile(projectile.Center, RandVelocity, ModContent.ProjectileType<DraconicFireball>(), projectile.damage / 3, projectile.knockBack / 2, projectile.owner);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Collision.CanHit(projectile.Center, 0, 0, target.Center, 0, 0) && target.active && !target.dontTakeDamage && !target.friendly && target.lifeMax > 5;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), 600);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss && target.knockBackResist > 0)
                target.velocity = Vector2.Normalize(projectile.Center - target.position) * 5 * target.knockBackResist;
            if (projectile.timeLeft <= 10)
                target.immune[projectile.owner] = 0;
            else
                target.immune[projectile.owner] /= 3;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.velocity = Vector2.Normalize(projectile.Center - target.position) * 5;
            if (projectile.timeLeft <= 10)
                target.immuneTime = 0;
            else
                target.immuneTime /=  3;
        }
    }
}
