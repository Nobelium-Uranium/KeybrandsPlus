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
			dust.alpha = 50;
            dust.position -= new Vector2(3);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.97f;                    
			dust.velocity *= 0.90f;
            float light = 0.35f * dust.scale; //144, 15, 141
            Lighting.AddLight(dust.position, 0.5647f * dust.scale, 0.0588f * dust.scale, 0.5529f * dust.scale);
            if (dust.noLight ? dust.scale < 0.1f : dust.scale < 0.5f)
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