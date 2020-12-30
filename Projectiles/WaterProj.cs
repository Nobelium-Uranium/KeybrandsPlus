using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class WaterProj : KeybrandProj
    {
        int DropletTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water");
        }
        public override void SetDefaults()
        {
            DropletTimer = Main.rand.Next(5, 26);
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 12;
            projectile.height = 12;
            projectile.alpha = 255;
            projectile.aiStyle = 1;
            projectile.tileCollide = true;
            projectile.extraUpdates += 1;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Water = true;
        }
        public override void AI()
        {
            if (DropletTimer++ >= 30)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<WaterDrop>(), projectile.damage, 0, projectile.owner);
                DropletTimer = Main.rand.Next(0, 26);
            }

            for (int k = 0; k < Main.rand.Next(3, 5); k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 29, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 2f;
                Main.dust[dust].alpha = 100;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Wet, 600);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item86, projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<WaterSplash>(), (int)(projectile.damage * 0.5), projectile.knockBack, projectile.owner);
            for (int k = 0; k < Main.rand.Next(2, 4); k++)
            {
                float RandX = Main.rand.NextFloat(1, 3);
                if (Main.rand.NextBool())
                    RandX *= -1;
                float RandY = Main.rand.NextFloat(1, 3);
                if (projectile.velocity.Y >= 0)
                    RandY *= -1;
                Vector2 RandVelocity = new Vector2(RandX, RandY);
                int droplet = Projectile.NewProjectile(projectile.Center, RandVelocity, ModContent.ProjectileType<WaterDrop>(), (int)(projectile.damage * 0.35), projectile.knockBack / 2, projectile.owner);
                Main.projectile[droplet].timeLeft /= 2;
            }
        }
    }
}
