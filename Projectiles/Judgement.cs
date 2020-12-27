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
    class Judgement : KeybrandProj
    {
        private NPC LastHit;
        private bool LockOn;
        private int SearchDuration;
        private int HomingDelay;
        private int GlobalTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Judgement");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            aiType = 20;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates += 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            SearchDuration = 45;
            HomingDelay = 0;
            GlobalTimer = 900;
        }
        public override void AI()
        {
            projectile.rotation += 0.3f * projectile.direction;
            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity, 20f);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            if (GlobalTimer > 0 && !Main.player[projectile.owner].dead)
            {
                Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.3f);
                if (HomingDelay <= 0)
                {
                    if (!LockOn)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LockOn").WithVolume(0.75f), projectile.position);
                        LockOn = true;
                    }
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 newMove = Main.MouseWorld - projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        move = newMove;
                        if (distanceTo > 100f)
                        {
                            AdjustMagnitude(ref move, 50f);
                            projectile.velocity = (10 * projectile.velocity + move) / 11f;
                            projectile.velocity += new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f));
                            AdjustMagnitude(ref projectile.velocity, 50f);
                        }
                        else
                            AdjustMagnitude(ref projectile.velocity, 25f);
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    LockOn = false;
                    HomingDelay -= 1;
                }
            }
            else
            {
                projectile.tileCollide = false;
                Vector2 Return = Main.player[projectile.owner].Center - projectile.Center;
                float distanceTo = (float)Math.Sqrt(Return.X * Return.X + Return.Y * Return.Y);
                AdjustMagnitude(ref Return, 100f);
                projectile.velocity = (10 * projectile.velocity + Return) / 11f;
                AdjustMagnitude(ref projectile.velocity, 100f);
                if (distanceTo < 50f)
                    projectile.Kill();
            }
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            GlobalTimer -= 1;
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
            Main.spriteBatch.Draw(texture,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                sourceRectangle, Color.White, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        private void AdjustMagnitude(ref Vector2 vector, float max)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > max)
            {
                vector *= max / magnitude;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return GlobalTimer > 0 && !Main.player[projectile.owner].dead && !target.friendly;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target == LastHit)
                damage = (int)(damage * 0.8f);
            /*if (HomingDelay <= 0 && !target.friendly && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
                HomingDelay = 15;*/
            LastHit = target;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 point = projectile.Center;
            Vector2 positionInWorld = ClosestPointInRect(target.Hitbox, point);
            for (int i = 0; i < Main.rand.Next(2, 5); i++)
            {
                int dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.KeybrandHit>(), Scale: Main.rand.NextFloat(.75f, 1f));
                Main.dust[dust].velocity *= Main.rand.NextFloat(1.25f, 1.75f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            //Main.PlaySound(SoundID.Dig, projectile.position);
            if (projectile.velocity.X != oldVelocity.X)
                projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y)
                projectile.velocity.Y = -oldVelocity.Y;
            /*
            if (HomingDelay <= 0)
                HomingDelay = 30;
            */
            return false;
        }
    }
}
