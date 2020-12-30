using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace KeybrandsPlus.Projectiles
{
    class WaterDrop : KeybrandProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Droplet");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.alpha = 255;
            projectile.aiStyle = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = Main.rand.Next(90, 120);
            projectile.extraUpdates += 1;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Water = true;
        }
        public override void AI()
        {
            for (int k = 0; k < Main.rand.Next(3, 5); k++)
            {
                int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 29, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[dust].alpha = 100;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            target.AddBuff(BuffID.Wet, 180);
        }
        public override void Kill(int timeLeft)
        {
            int num4;
            for (int num582 = 0; num582 < 20; num582 = num4 + 1)
            {
                int num583 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f);
                Main.dust[num583].noGravity = true;
                Dust dust = Main.dust[num583];
                dust.velocity *= 2f;
                num583 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 33, (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f);
                dust = Main.dust[num583];
                dust.velocity *= 2f;
                num4 = num582;
            }
        }
    }
}
