using Terraria.ID;
using KeybrandsPlus.Helpers;
using Terraria;

namespace KeybrandsPlus.Projectiles
{
    class WaterSplash : KeybrandProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Splash");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 75;
            projectile.height = 75;
            projectile.aiStyle = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 10;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Water = true;
        }
        public override void AI()
        {
            for (int k = 0; k < Main.rand.Next(5, 10); k++)
            {
                int Flame = Dust.NewDust(projectile.Center, 0, 0, 29);
                Main.dust[Flame].scale *= 2f;
                Main.dust[Flame].velocity *= 5f;
                Main.dust[Flame].alpha = 100;
                Main.dust[Flame].noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.NextBool())
                crit = false;
            target.AddBuff(BuffID.Wet, 300);
        }
    }
}
