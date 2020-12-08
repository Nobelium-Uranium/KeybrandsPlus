using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Projectiles
{
    class Blood : ModProjectile
    {
        bool WasWet;
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(5);
            projectile.aiStyle = 1;
            projectile.tileCollide = true;
            projectile.extraUpdates += 1;
            projectile.timeLeft = Main.rand.Next(60, 301);
        }
        public override bool PreAI()
        {
            if (projectile.wet && WasWet)
            {
                projectile.position.Y = projectile.oldPosition.Y + 1;
                projectile.velocity.Y = 0;
                projectile.timeLeft--;
                WasWet = false;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            if (projectile.wet)
            {
                if (projectile.velocity.Y > -1)
                    projectile.velocity.Y -= 0.25f;
                WasWet = true;
            }
            int blood = Dust.NewDust(projectile.Center, 0, 0, DustID.Blood);
            Main.dust[blood].velocity += projectile.velocity;
            Main.dust[blood].velocity /= 2;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X * 0.75f;
            }
            projectile.velocity.X *= 0.9f;
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y * 0.25f;
            }
            return false;
        }
    }
}
