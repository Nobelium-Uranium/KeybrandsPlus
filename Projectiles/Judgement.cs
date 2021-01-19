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
        private int CheckTimer;
        private Vector2 DistCheck;
        private bool DidntMoveMuch;
        private int GlobalTimer;
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
        }
        public override void AI()
        {
            projectile.rotation += 0.3f * projectile.direction;
            if (projectile.localAI[0] == 0f)
            {
                DistCheck = projectile.position;
                AdjustMagnitude(ref projectile.velocity, 20f);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            if (GlobalTimer++ < 900 && !Main.player[projectile.owner].dead)
            {
                if (++CheckTimer >= 15)
                {
                    if (!DidntMoveMuch)
                        DistCheck = projectile.position;
                    Vector2 lastMove = DistCheck - projectile.Center;
                    float oldDistance = (float)Math.Sqrt(lastMove.X * lastMove.X + lastMove.Y * lastMove.Y);
                    if (oldDistance < 50f)
                        DidntMoveMuch = true;
                    else
                        DidntMoveMuch = false;
                    CheckTimer = 0;
                }
                Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.3f);
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 newMove = Main.MouseWorld - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    Vector2 vectorToPlayer = Main.player[projectile.owner].Center - projectile.Center;
                    float playerDistance = (float)Math.Sqrt(vectorToPlayer.X * vectorToPlayer.X + vectorToPlayer.Y * vectorToPlayer.Y);
                    move = newMove;
                    if (distanceTo > 75f)
                    {
                        AdjustMagnitude(ref move, 50f);
                        projectile.velocity = (10 * projectile.velocity + move) / 11f;
                        projectile.velocity += new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f));
                        AdjustMagnitude(ref projectile.velocity, 50f);
                    }
                    else
                        AdjustMagnitude(ref projectile.velocity, 25f);
                    if (playerDistance > 1000f)
                        GlobalTimer += 14;
                    projectile.netUpdate = true;
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
            return GlobalTimer < 900 && !Main.player[projectile.owner].dead && !target.friendly;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target == LastHit)
                damage = (int)(damage * 0.8f);
            if (DidntMoveMuch)
                damage = (int)(damage * 0.8f);
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
            if (projectile.velocity.X != oldVelocity.X)
                projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y)
                projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
    }
}
