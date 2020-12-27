using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Dusts.Keybrand
{
    class KeybrandHit : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, Main.rand.Next(2) * 24, 22, 24);
            dust.rotation = dust.rotation.ToRotationVector2().RotatedByRandom(360).ToRotation();
            if (Main.rand.NextBool())
                dust.noGravity = true;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.05f;
            dust.scale *= 0.95f;
            if (dust.scale < 0.3f)
                if (Main.rand.NextBool(5) || dust.scale <= 0.1f)
                    dust.active = false;
            if (!dust.noGravity)
            {
                dust.velocity.Y = dust.velocity.Y + 0.075f;
                dust.velocity.X = dust.velocity.X * 0.975f;
            }
            else
                dust.velocity *= 0.9f;
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(255, 255, 255, dust.alpha);
        }
    }
}
