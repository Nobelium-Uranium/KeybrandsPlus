using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class DraconicFireball : KeybrandProj
    {
        int FallDelay;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Debris");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = 1;
            projectile.timeLeft = 600;
            projectile.extraUpdates += 1;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.GetGlobalProjectile<Globals.KeyProjectile>().Nil = true;
        }
        public override void AI()
        {
            if (FallDelay < 10)
            {
                projectile.ai[0] = 0;
            }
            if (FallDelay == 30)
                projectile.penetrate = 1;
            projectile.alpha -= 10;
            if (Main.rand.NextBool())
            {
                int Flame = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Dusts.DraconicFlame>(), projectile.velocity.X * .25f, projectile.velocity.Y * .25f);
                Main.dust[Flame].scale *= .75f;
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];
                if (!target.HasBuff(ModContent.BuffType<Buffs.DragonRot>()) && !target.dontTakeDamage && !target.friendly && Vector2.Distance(projectile.Center, target.Center) <= 30f)
                    target.AddBuff(ModContent.BuffType<Buffs.DragonAura>(), Main.rand.Next(120, 300));
            }
            FallDelay++;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                projectile.velocity.X = -oldVelocity.X / 2;
                return false;
            }
            if (projectile.velocity.Y < 0)
            {
                projectile.velocity.Y = -oldVelocity.Y;
                return false;
            }
            return base.OnTileCollide(oldVelocity);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, .6f);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), Main.rand.Next(120, 300));
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item89.WithVolume(.5f), projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int Flame = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DraconicFlame>(), (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f);
                Main.dust[Flame].velocity += projectile.velocity / 5;
                Main.dust[Flame].velocity *= 2.5f;
            }
        }
    }
}
