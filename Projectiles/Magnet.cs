using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    public class Magnet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(0);
            projectile.timeLeft = 150;
            projectile.alpha = 255;
            projectile.damage = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override bool PreAI()
        {
            projectile.velocity = Vector2.Zero;
            return base.PreAI();
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, Color.White.ToVector3() * .25f);
            if (Main.rand.NextBool(3))
            {
                KeyUtils.NewDustConverge(out int Debris, projectile.Center + new Vector2(4), Vector2.Zero, Main.rand.NextFloat(50f, 300f), Main.rand.NextBool() ? DustID.Dirt : DustID.Stone);
                Main.dust[Debris].scale *= Main.rand.NextFloat(1.5f, 2.5f);
                Main.dust[Debris].velocity *= .8f;
            }
            for (int k = 0; k < Main.maxItems; k++)
            {
                Item i = Main.item[k];
                if (i.active)
                {
                    if (Vector2.Distance(i.Center, projectile.Center) < 10f && KeyUtils.HasItemSpace(Main.player[projectile.owner]))
                    {
                        i.Center = Main.player[projectile.owner].Center;
                        i.velocity = Vector2.Zero;
                        i.noGrabDelay = 0;
                    }
                    if (Vector2.Distance(i.Center, projectile.Center) < 300f)
                    {
                        Vector2 vTo = KeyUtils.VectorTo(i.Center, projectile.Center);
                        KeyUtils.AdjustMagnitude(ref vTo, 30f);
                        i.velocity = (10 * i.velocity + vTo) / 11f;
                        KeyUtils.AdjustMagnitude(ref i.velocity, 30f);
                    }
                }
            }
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC i = Main.npc[k];
                if (i.active && !i.boss && !i.dontTakeDamage && !i.friendly && i.lifeMax > 5 && i.knockBackResist > 0)
                {
                    if (Vector2.Distance(i.Center, projectile.Center) < 300f)
                    {
                        Vector2 vTo = KeyUtils.VectorTo(i.Center, projectile.Center);
                        KeyUtils.AdjustMagnitude(ref vTo, 30f * (i.knockBackResist < .5f ? .5f : i.knockBackResist));
                        i.velocity = (10 * i.velocity + vTo) / 11f;
                        KeyUtils.AdjustMagnitude(ref i.velocity, 30f * (i.knockBackResist < .5f ? .5f : i.knockBackResist));
                    }
                }
            }
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player i = Main.player[k];
                if (i.active && i != Main.player[projectile.owner] && i.hostile && (i.team != Main.player[projectile.owner].team || i.team == 0))
                {
                    if (Vector2.Distance(i.Center, projectile.Center) < 300f)
                    {
                        Vector2 vTo = KeyUtils.VectorTo(i.Center, projectile.Center);
                        KeyUtils.AdjustMagnitude(ref vTo, 30f);
                        i.velocity = (10 * i.velocity + vTo) / 11f;
                        KeyUtils.AdjustMagnitude(ref i.velocity, 30f);
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
    }
}
