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
    class StrikeRaid : KeybrandProj
    {
        private int SpinTimer;
        private int ReturnTimer;
        private bool SetInitDamage;
        private int InitialDamage;
        private bool CanReturnNormally;
        private bool Returning;
        private int DamageDealt;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strike Raid");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            aiType = 20;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates += 1;
            ReturnTimer = 90;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 1)
            {
                int type = DustID.Fire;
                switch (Main.rand.Next(3))
                {
                    case 1:
                        type = 75;
                        break;
                    case 2:
                        type = 135;
                        break;
                    default:
                        type = DustID.Fire;
                        break;
                }
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, type, 0, 0, 100);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 3f;
            }
            if (!SetInitDamage)
            {
                SetInitDamage = true;
                InitialDamage = projectile.damage;
            }
            if (projectile.ai[0] == 0)
                Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.3f);
            projectile.rotation += 0.3f * projectile.direction;
            if (projectile.velocity.LengthSquared() < 2)
            {
                if (SpinTimer >= 30)
                    CanReturnNormally = true;
                else
                    SpinTimer++;
            }
            if (CanReturnNormally || Main.player[projectile.owner].dead)
            {
                Returning = false;
                Vector2 Return = Main.player[projectile.owner].Center - projectile.Center;
                float distanceTo = (float)Math.Sqrt(Return.X * Return.X + Return.Y * Return.Y);
                if (ReturnTimer <= 0)
                {
                    projectile.tileCollide = false;
                    AdjustMagnitude(ref Return, 50f);
                    projectile.velocity = (10 * projectile.velocity + Return) / 11f;
                    AdjustMagnitude(ref projectile.velocity, 50f);
                }
                else if (Collision.CanHitLine(projectile.position, 1, 1, Main.player[projectile.owner].position, 1, 1))
                {
                    AdjustMagnitude(ref Return, 20f);
                    projectile.velocity = (10 * projectile.velocity + Return) / 11f;
                    AdjustMagnitude(ref projectile.velocity, 20f);
                    Returning = true;
                }
                ReturnTimer -= 1;
                if (distanceTo < 50f)
                    projectile.Kill();
            }
            else
                projectile.velocity *= 0.9f;
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
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
            return !Main.player[projectile.owner].dead && !target.friendly;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[0] == 1)
            {
                if (!target.buffImmune[BuffID.CursedInferno])
                    target.AddBuff(BuffID.CursedInferno, 300);
                else if (!target.buffImmune[BuffID.Frostburn])
                    target.AddBuff(BuffID.Frostburn, 300);
                else if (!target.buffImmune[BuffID.OnFire])
                    target.AddBuff(BuffID.OnFire, 300);
                else
                    damage = (int)(damage * 1.5f);
            }
            if (Returning)
                damage = (int)(damage * 1.5f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            DamageDealt += damage;
            if (projectile.ai[0] == 1 && DamageDealt < 2500)
                SpinTimer = 0;
            else if (DamageDealt < 750)
                SpinTimer = 0;
            if (!Returning && projectile.velocity.LengthSquared() > 2.5f)
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 2.5f;
            projectile.damage -= (int)(InitialDamage * 0.05f);
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
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Dig, projectile.position);
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
    }
}
