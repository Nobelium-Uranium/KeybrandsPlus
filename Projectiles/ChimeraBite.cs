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
    class ChimeraBite : KeybrandProj
    {
        private NPC LastHitNPC;
        private int ExtraUpdateCounter;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chimera's Bite");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 250;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            aiType = 20;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.penetrate = -1;
            drawOffsetX = -24;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.ignoreWater = true;
            ExtraUpdateCounter = 50;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.DodgerBlue * ((255f - projectile.alpha) / 255f);
        }
        public override void AI()
        {
            ExtraUpdateCounter ++;
            if (ExtraUpdateCounter >= 10)
            {
                ExtraUpdateCounter = 0;
                projectile.extraUpdates++;
            }
            if (projectile.extraUpdates >= 500)
                projectile.Kill();
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.spriteDirection = projectile.direction;
            ref float reference = ref projectile.localAI[0];
            reference += 1f;
            if (Main.rand.NextBool(20))
            {
                Vector2 spinningpoint = Utils.RandomVector2(Main.rand, -0.5f, 0.5f) * new Vector2(20f, 80f);
                spinningpoint = spinningpoint.RotatedBy(projectile.velocity.ToRotation(), default);
                Dust dust8 = Dust.NewDustDirect(projectile.Center, 0, 0, DustID.AncientLight, -projectile.velocity.X, -projectile.velocity.Y, 0, Color.DodgerBlue, 1f);
                dust8.alpha = 127;
                dust8.fadeIn = 1.5f;
                dust8.scale = 1.3f;
                Dust dust3 = dust8;
                dust3.velocity *= 0.3f;
                dust8.position = projectile.Center + spinningpoint;
                dust8.noGravity = true;
                dust8.noLight = true;
            }
            Lighting.AddLight(projectile.Center, Color.DodgerBlue.ToVector3());
        }
        public override bool? CanHitNPC(NPC target)
        {
            return target != LastHitNPC && !target.friendly && projectile.friendly && !projectile.hostile;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (!target.friendly && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
            {
                LastHitNPC = target;
            }
            if (crit)
            {
                int num276 = Main.rand.Next(15, 25);
                int num4;
                for (int num277 = 0; num277 < num276; num277 = num4 + 1)
                {
                    Main.PlaySound(SoundID.Item88, target.position);
                    int num278 = Dust.NewDust(projectile.Center, 0, 0, DustID.Blood, 0f, 0f, 0, default, 1.3f);
                    Dust dust = Main.dust[num278];
                    dust.velocity *= 2f * (0.3f + 0.7f * Main.rand.NextFloat());
                    Main.dust[num278].fadeIn = 1.3f + Main.rand.NextFloat() * 0.2f;
                    Main.dust[num278].noGravity = true;
                    Main.dust[num278].noLight = true;
                    dust = Main.dust[num278];
                    dust.position += Main.dust[num278].velocity * 4f;
                    num4 = num277;
                }
                target.AddBuff(BuffType<Buffs.ChimeraBleed>(), 600);
                projectile.damage += 4;
            }
            else
            {
                target.AddBuff(BuffType<Buffs.ChimeraBleed>(), 180);
                projectile.damage += 2;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 point = projectile.Center;
            Vector2 positionInWorld = ClosestPointInRect(target.Hitbox, point);
            for (int i = 0; i < Main.rand.Next(2, 5); i++)
            {
                int dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.ChimeraHit>(), Scale: Main.rand.NextFloat(.75f, 1f));
                Main.dust[dust].velocity *= Main.rand.NextFloat(1.25f, 1.75f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(drawOffsetX, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(mod.GetTexture("Textures/ChimeraTrail"), drawPos, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            }
            return true;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (projectile.hostile)
            {
                for (int i = 0; i < target.statLifeMax2 / 20; i++)
                {
                    float RandX = Main.rand.NextFloat(0, 3);
                    if (Main.rand.NextBool())
                        RandX *= -1;
                    float RandY = Main.rand.NextFloat(0, 3);
                    if (Main.rand.NextBool())
                        RandY *= -1;
                    Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(target.Center, RandVelocity, ProjectileType<Blood>(), 0, 0);
                }
                target.AddBuff(BuffType<Buffs.ChimeraBleed>(), 300);
                target.AddBuff(BuffID.Bleeding, 300);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Vector2 point = projectile.Center;
            Vector2 positionInWorld = ClosestPointInRect(target.Hitbox, point);
            for (int i = 0; i < Main.rand.Next(3, 8); i++)
            {
                int dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.ChimeraHit>(), Scale: Main.rand.NextFloat(.75f, 1f));
                Main.dust[dust].velocity *= Main.rand.NextFloat(1.25f, 1.75f);
            }
        }
        public override void Kill(int timeLeft)
        {
            int num276 = Main.rand.Next(15, 25);
            int num4;
            for (int num277 = 0; num277 < num276; num277 = num4 + 1)
            {
                int num278 = Dust.NewDust(projectile.Center, 0, 0, DustID.AncientLight, 0f, 0f, 100, Color.DodgerBlue, 1.3f);
                Dust dust = Main.dust[num278];
                dust.velocity *= 8f * (0.3f + 0.7f * Main.rand.NextFloat());
                Main.dust[num278].fadeIn = 1.3f + Main.rand.NextFloat() * 0.2f;
                Main.dust[num278].noGravity = true;
                Main.dust[num278].noLight = true;
                dust = Main.dust[num278];
                dust.position += Main.dust[num278].velocity * 4f;
                num4 = num277;
            }
        }
    }
}
