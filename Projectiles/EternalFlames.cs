using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class EternalFlames : KeybrandProj
    {
        private bool FirstTick;
        private int InitDir;
        private int Hit;
        private int GlobalTimer;
        private bool Returning;
        private float Penalty;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(26);
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            GlobalTimer = 10;
            projectile.timeLeft = 180;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Fire = true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Returning)
                damage /= 2;
            damage = (int)(damage - Penalty);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.EternalBlaze>(), Main.rand.Next(180, 301));
            Hit++;
            Penalty += .15f;
            if (Penalty > .45f)
                Penalty = .45f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Dig, projectile.Center);
            projectile.velocity = -projectile.oldVelocity;
            GlobalTimer = 0;
            return false;
        }

        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (!FirstTick)
            {
                InitDir = projectile.direction;
                FirstTick = true;
            }
            projectile.rotation += .75f * InitDir;
            int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 235);
            Main.dust[dustIndex].noGravity = true;
            int dustIndex2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, Scale: 2f);
            Main.dust[dustIndex2].noGravity = true;
            if (!owner.active || owner.dead)
                projectile.Kill();
            GlobalTimer--;
            Vector2 move = Vector2.Zero;
            float distance = 1000f;
            bool target = false;
            if (GlobalTimer <= 0 || Hit >= 3)
            {
                Returning = true;
                projectile.tileCollide = false;
                if (Main.myPlayer == projectile.owner)
                {
                    if (owner.active && !owner.dead)
                    {
                        Vector2 newMove = owner.Center - projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                        if (target)
                        {
                            AdjustMagnitude(ref move, 30f);
                            projectile.velocity = (10 * projectile.velocity + move) / 11f;
                            AdjustMagnitude(ref projectile.velocity, 30f);
                        }
                        if (distanceTo <= 30f)
                            projectile.Kill();
                    }
                }
            }
        }

        private void AdjustMagnitude(ref Vector2 vector, float max)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > max)
            {
                vector *= max / magnitude;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
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
            
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + origin / 2 + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}
