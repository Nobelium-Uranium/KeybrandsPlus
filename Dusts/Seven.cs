using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Dusts
{
    class Seven : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 18, 20);
            dust.position -= new Vector2(9, 10);
            //If our texture had 2 different dust on top of each other (a 30x60 pixel image), we might do this:
            //dust.frame = new Rectangle(0, Main.rand.Next(2) * 30, 30, 30);
        }
        public override bool Update(Dust dust)
        {
            dust.velocity = Vector2.Zero;
            dust.scale -= 0.1f;
            if (dust.scale < 0.25f)
            {
                dust.active = false;
            }
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return Color.White;
        }
    }
}
