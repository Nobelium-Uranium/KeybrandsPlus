using KeybrandsPlus.Globals;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static KeybrandsPlus.Helpers.KeyUtils;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Projectiles
{
    class ChimeraTooth : KeybrandProj
    {
        private bool Init;
        private int Type;
        private int Spread;
        private int Deviance;
        private float StartAngle;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chimera Knife");
        }
        public override void SetDefaults()
        {
            Type = -1;
            Spread = -1;
            projectile.Size = new Vector2(16);
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.ignoreWater = true;
            projectile.GetGlobalProjectile<KeyProjectile>().Nil = true;
        }
        public override void AI()
        {
            if (!Init)
            {
                Init = true;
                if (Type == -1)
                    Type = (int)projectile.ai[0];
                if (Spread == -1)
                {
                    if (projectile.ai[1] != 0)
                        Spread = (int)projectile.ai[1];
                    else
                        Spread = 15;
                }
                Deviance = Main.rand.Next(0, Spread + 1);
                StartAngle = projectile.velocity.ToRotation() + MathHelper.ToRadians(Deviance * (Main.rand.NextBool() ? -1 : 1));
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            projectile.direction = projectile.velocity.X > 0f ? 1 : -1;
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.localAI[0] < 30 + Deviance)
            {
                if (projectile.velocity.Length() > .1f)
                    projectile.velocity *= .9f;
                projectile.velocity = projectile.velocity.ToRotation().AngleTowards(StartAngle, MathHelper.ToRadians(2.5f)).ToRotationVector2() * projectile.velocity.Length();
            }
            if (projectile.localAI[0] >= 30 + Deviance)
            {
                if (projectile.timeLeft > 600)
                    projectile.timeLeft = 600;
                if (projectile.velocity.Length() < 25f)
                    projectile.velocity *= 1.1f;
                if (projectile.velocity.Length() > 25f)
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * 25f;
            }
            projectile.localAI[0]++;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * ((255f - projectile.alpha) / 255f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

            return false;
        }
    }
}
