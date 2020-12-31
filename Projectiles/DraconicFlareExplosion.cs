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
            projectile.width = 150;
            projectile.height = 150;
            projectile.aiStyle = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 60;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Dark = true;
        }
        public override void AI()
        {
            for (int k = 0; k < Main.rand.Next(5, 10); k++)
            {
                int Flame = Dust.NewDust(projectile.Center, 0, 0, ModContent.DustType<Dusts.DraconicFlame>());
                Main.dust[Flame].velocity *= 5f;
            }
            float RandX = Main.rand.NextFloat(1.5f, 3.5f);
            if (Main.rand.NextBool())
                RandX *= -1;
            RandX *= Main.rand.NextFloat(.5f, 1);
            float RandY = Main.rand.NextFloat(1.5f, 3.5f);
            if (Main.rand.NextBool())
                RandY *= -1;
            RandY *= Main.rand.NextFloat(.5f, 1);
            Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(30);
            if (Main.rand.NextBool(5))
                Projectile.NewProjectile(projectile.Center, RandVelocity, ModContent.ProjectileType<DraconicFireball>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Collision.CanHit(projectile.Center, 0, 0, target.Center, 0, 0);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), 600);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item62, projectile.position);
            for (int k = 0; k < 50; k++)
            {
                int Flame = Dust.NewDust(projectile.Center, 0, 0, ModContent.DustType<Dusts.DraconicFlame>());
                Main.dust[Flame].velocity = new Vector2(0, 15).RotatedBy(7.2 * k);
                Main.dust[Flame].scale *= 2.5f;
            }
            for (int i = 0; i < 20; i++)
            {
                int debris = Projectile.NewProjectile(projectile.Center, new Vector2(0, 5f).RotatedBy(18 * i).RotatedByRandom(0.5f) * Main.rand.NextFloat(.9f, 1.1f), ModContent.ProjectileType<DraconicFireball>(), projectile.damage, projectile.knockBack, projectile.owner);
                Main.projectile[debris].scale *= 1.25f;
                Main.projectile[debris].Size *= 1.25f;
            }
        }
    }
}
