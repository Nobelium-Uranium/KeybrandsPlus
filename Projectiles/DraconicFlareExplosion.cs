using KeybrandsPlus.Globals;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class DraconicFlareExplosion : KeybrandProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Eruption");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 300;
            projectile.height = 300;
            projectile.aiStyle = 0;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 10)
            {
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC i = Main.npc[k];
                    if (i.active && !i.boss && !i.dontTakeDamage && !i.friendly && Collision.CanHit(i.Center, 0, 0, projectile.Center, 0, 0))
                    {
                        if (Vector2.Distance(i.Center, projectile.Center) < 200f)
                        {
                            i.immune[projectile.owner] = 0;
                            i.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), 1800);
                        }
                    }
                }
                Main.PlaySound(SoundID.Item62, projectile.position);
                projectile.Size = new Vector2(400);
                projectile.position -= new Vector2(50);
                float RandRotate = Main.rand.NextFloat(0, 9);
                for (int i = 0; i < 40; i++)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(0, 10f).RotatedBy(MathHelper.ToRadians(9) * i).RotatedBy(MathHelper.ToRadians(RandRotate)), ModContent.ProjectileType<DraconicFireball>(), projectile.damage / 2, projectile.knockBack, projectile.owner);
                }
                for (int k = 0; k < 50; k++)
                {
                    KeyUtils.NewDustCircular(out int Flame, projectile.Center, Vector2.Zero, ModContent.DustType<Dusts.DraconicFlame>(), 20, perfect: true);
                    Main.dust[Flame].scale *= 3.75f;
                }
            }
            else if (projectile.timeLeft > 10)
            {
                KeyUtils.NewDustCircular(out int Flame, projectile.Center, Vector2.Zero, ModContent.DustType<Dusts.DraconicFlame>(), Main.rand.NextFloat(2.5f, 5f));
                Main.dust[Flame].scale *= .75f;
                if (Main.rand.NextBool())
                    Main.dust[Flame].velocity *= 1.5f;
                for (int k = 0; k < Main.rand.Next(5, 10); k++)
                {
                    if (!Main.rand.NextBool(3))
                        KeyUtils.NewDustConverge(out Flame, projectile.Center, Vector2.Zero, 150, ModContent.DustType<Dusts.DraconicFlame>(), scale: 2f);
                    else
                        KeyUtils.NewDustConverge(out int Debris, projectile.Center, Vector2.Zero, Main.rand.NextFloat(150f, 300f), ModContent.DustType<Dusts.DraconicFlame>(), scale: Main.rand.NextFloat(1.5f, 2.5f));
                }
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC i = Main.npc[k];
                    if (i.active && !i.boss && !i.dontTakeDamage && !i.friendly && Collision.CanHit(i.Center, 0, 0, projectile.Center, 0, 0))
                    {
                        if (Vector2.Distance(i.Center, projectile.Center) < 300f && i.knockBackResist > 0)
                        {
                            Vector2 vTo = KeyUtils.VectorTo(i.Center, projectile.Center);
                            KeyUtils.AdjustMagnitude(ref vTo, 30f * (i.knockBackResist < .5f ? .5f : i.knockBackResist));
                            i.velocity = (10 * i.velocity + vTo) / 11f;
                            KeyUtils.AdjustMagnitude(ref i.velocity, 30f * (i.knockBackResist < .5f ? .5f : i.knockBackResist));
                        }
                        if (Vector2.Distance(i.Center, projectile.Center) < 150f)
                        {
                            i.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), 10);
                        }
                    }
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return Collision.CanHit(projectile.Center, 0, 0, target.Center, 0, 0) && Vector2.Distance(target.Center, projectile.Center) < projectile.width / 2 && target.active;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonRot>(), 600);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss)
                target.velocity = Vector2.Normalize(KeyUtils.VectorTo(target.Center, projectile.Center)) * 10 * (target.knockBackResist < 0 ? 0 : target.knockBackResist);
            target.immune[projectile.owner] = 0;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.velocity = Vector2.Normalize(KeyUtils.VectorTo(target.Center, projectile.Center)) * 5;
            if (projectile.timeLeft <= 10)
                target.immuneTime /= 5;
            else
            {
                damage /= 2;
                target.immuneTime /= 3;
            }
        }
    }
}
