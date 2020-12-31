using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Dusts
{
    public class DraconicFlame : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 6, 6, 6);
            dust.color = default;
            dust.scale = 2f;
            dust.noGravity = true;
            dust.noLight = true;
			dust.alpha = 50;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.97f;                    
			dust.velocity *= 0.90f;
            float light = 0.35f * dust.scale; //144, 15, 141
            Lighting.AddLight(dust.position, 0.144f * dust.scale, 0.015f * dust.scale, 0.141f * dust.scale);
            if (dust.scale < 0.1f)
            {
				if (Main.rand.Next(10) == 0)
                {
					dust.active = false;
				}
            }
            if(!dust.noGravity)
            {
                dust.velocity.Y = dust.velocity.Y + 0.15f;
                dust.velocity.X = dust.velocity.X * 0.99f;
            }
            return false;
        }
		
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color(255, 255, 255, dust.alpha);
		}
    }
}