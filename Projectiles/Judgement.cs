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
        private NPC currTarget;
        private NPC LastHit;
        private NPC LastHitTarget;
        private int hitCd;
        private int GlobalTimer;
        private Vector2 randOffset;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Judgement");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            aiType = 20;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates += 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            randOffset = new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8));
        }
        public override void AI()
        {
            projectile.rotation += 0.3f * projectile.direction;
            if (projectile.localAI[0]++ == 10)
            {
                LastHitTarget = null;
                projectile.localAI[0] = 0;
            }
            if (hitCd > 0)
                hitCd--;
            else if (hitCd < 0)
                hitCd = 0;
            Vector2 move = Vector2.Zero;
            if (GlobalTimer++ < 1200 && !Main.player[projectile.owner].dead)
            {
                Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.3f);
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC n = Main.npc[k];
                    Vector2 vectorToNPC = n.Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(vectorToNPC.X * vectorToNPC.X + vectorToNPC.Y * vectorToNPC.Y);
                    if (n.active && n != LastHitTarget && (LastHit == null ? distanceTo < 200f : distanceTo < 300f) && !n.friendly && n.lifeMax > 5 && !n.dontTakeDamage && currTarget == null)
                    {
                        currTarget = n;
                    }
                    if (currTarget != null)
                    {
                        if (!Collision.CanHitLine(projectile.Center, 0, 0, currTarget.Center, 0, 0))
                            projectile.localAI[1]++;
                        else
                            projectile.localAI[1] = 0;
                        if (!currTarget.active || currTarget == LastHitTarget || currTarget.dontTakeDamage || projectile.localAI[1] >= 10)
                        {
                            projectile.ai[0] = 0;
                            currTarget = null;
                            projectile.tileCollide = true;
                            randOffset = new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8));
                        }
                        else
                        {
                            if (projectile.ai[0]++ >= 5)
                            {
                                projectile.ai[0] = 0;
                                randOffset = new Vector2(Main.rand.NextFloat(-currTarget.width * .75f, currTarget.width * .75f), Main.rand.NextFloat(-currTarget.height * .75f, currTarget.height * .75f));
                            }
                            Vector2 newMove = currTarget.Center + randOffset - projectile.Center;
                            move = newMove;
                            if (hitCd == 0 && Collision.CanHitLine(projectile.Center, 0, 0, currTarget.Center, 0, 0))
                            {
                                projectile.tileCollide = false;
                                AdjustMagnitude(ref move, 20f);
                                projectile.velocity = (10 * projectile.velocity + move) / 11f;
                                projectile.velocity += new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f));
                                AdjustMagnitude(ref projectile.velocity, 20f);
                            }
                            else
                            {
                                projectile.tileCollide = true;
                                AdjustMagnitude(ref projectile.velocity, 15f);
                            }
                        }
                    }
                    else
                    {
                        AdjustMagnitude(ref projectile.velocity, 15f);
                    }
                }
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 vectorToPlayer = Main.player[projectile.owner].Center - projectile.Center;
                    float playerDistance = (float)Math.Sqrt(vectorToPlayer.X * vectorToPlayer.X + vectorToPlayer.Y * vectorToPlayer.Y);
                    if (playerDistance > 1000f)
                    {
                        if (currTarget != null)
                            GlobalTimer += 2;
                        else
                            GlobalTimer += 19;
                    }
                    else if (currTarget == null && hitCd == 0)
                    {
                        GlobalTimer += 9;
                    }
                }
                if (projectile.velocity.LengthSquared() < 10f)
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * 10f;
                projectile.netUpdate = true;
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
                projectile.netUpdate = true;
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
            return GlobalTimer < 1200 && !Main.player[projectile.owner].dead && !target.friendly;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target == LastHit)
                damage = (int)(damage * .75f);
            LastHit = target;
            LastHitTarget = target;
            projectile.localAI[0] = 0;
            hitCd = Main.rand.Next(7, 13);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 point = projectile.Center;
            Vector2 positionInWorld = ClosestPointInRect(target.Hitbox, point);
            for (int i = 0; i < Main.rand.Next(4, 7); i++)
            {
                int dust = Dust.NewDust(positionInWorld, 0, 0, DustID.TerraBlade, Scale: Main.rand.NextFloat(.75f, 1f));
                Main.dust[dust].velocity += projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15)) * Main.rand.NextFloat(.25f, .75f);
                Main.dust[dust].noGravity = true;
                dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.KeybrandHit>(), Scale: Main.rand.NextFloat(.75f, 1f));
                Main.dust[dust].velocity += Vector2.Normalize(projectile.velocity) * Main.rand.NextFloat(1.25f, 1.75f);

            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
                projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y)
                projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
    }
}
