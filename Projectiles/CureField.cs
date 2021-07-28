using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Projectiles
{
    class CureField : ModProjectile
    {
        private float Boost;
        private int CureLevel;
        private int CureTimer;
        private int HealAmount;
        private int PulseAmount;
        private float DeltaBonus;
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(0, 0);
            projectile.friendly = true;
            projectile.damage = 0;
            projectile.ignoreWater = true;
            DeltaBonus = 1f;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (projectile.ai[1] == 0)
                Boost = 1 + owner.GetModPlayer<Globals.KeyPlayer>().CureBoost;
            else
                Boost = .6f * (1 + owner.GetModPlayer<Globals.KeyPlayer>().CureBoost);
            CureLevel = (int)Math.Round(projectile.ai[0]);
            if (CureLevel < 0)
                CureLevel = 0;
            else if (CureLevel > 3)
                CureLevel = 3;
            projectile.velocity = Vector2.Zero;
            float distance;
            switch (CureLevel)
            {
                default:
                    distance = 150 * (.5f + (Boost / 2));
                    break;
                case 1:
                    distance = 225 * (.5f + (Boost / 2));
                    break;
                case 2:
                    distance = 300 * (.5f + (Boost / 2));
                    break;
                case 3:
                    distance = 375 * (.5f + (Boost / 2));
                    break;
            }
            for (int i = 0; i < 10; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * distance);
                offset.Y += (float)(Math.Cos(angle) * distance);
                Dust dust = Main.dust[Dust.NewDust(
                    projectile.Center + offset - new Vector2(4, 4), 0, 0, 163)];
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
            }
            CureTimer++;
            for (int k = 0; k < 200; k++)
            {
                Player p = Main.player[k];
                if (p.active && !p.dead && projectile.Distance(p.Center) < distance && p.team == owner.team)
                {
                    if (Main.rand.NextBool())
                    {
                        int dust = Dust.NewDust(p.Center - new Vector2(4, 4), 0, 0, 163);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale /= 2;
                    }
                }
                NPC n = Main.npc[k];
                if (n.active && n.friendly && projectile.Distance(n.Center) < distance && n.lifeMax > 5)
                {
                    if (Main.rand.NextBool())
                    {
                        int dust = Dust.NewDust(n.Center - new Vector2(4, 4), 0, 0, 163);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale /= 2;
                    }
                }
            }
            if (CureTimer == 30 || CureTimer == 60 || CureTimer == 90 || CureTimer == 120 || CureTimer == 150)
            {
                Main.PlaySound(SoundID.Item4, projectile.Center);
                for (int i = 0; i < 25; i++)
                {
                    KeyUtils.NewDustCircular(out int dust, projectile.Center - new Vector2(4, 4), Vector2.Zero, 163, 0);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 5f * (1 + (float)CureLevel / 2) * Boost;
                }
                switch (CureLevel)
                {
                    default:
                        HealAmount = (int)(30 * Boost);
                        break;
                    case 1:
                        HealAmount = (int)(60 * Boost);
                        break;
                    case 2:
                        HealAmount = (int)(90 * Boost);
                        break;
                    case 3:
                        HealAmount = (int)(120 * Boost);
                        break;
                }
                for (int k = 0; k < 200; k++)
                {
                    Player p = Main.player[k];
                    if (p.active && !p.dead && projectile.Distance(p.Center) < distance && p.team == owner.team)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            KeyUtils.NewDustConverge(out int dust, p.Center, Vector2.Zero, 75, 163, scale: .75f, fixedScale: false);
                            Main.dust[dust].position += p.velocity;
                        }
                        p.statLife += HealAmount;
                        if (p.statLife > p.statLifeMax2)
                            p.statLife = p.statLifeMax2;
                        CombatText.NewText(p.getRect(), CombatText.HealLife, HealAmount, dot: true);
                        if (p != owner)
                        {
                            owner.GetModPlayer<Globals.KeyPlayer>().currentDelta += (int)(HealAmount * DeltaBonus * (1 - PulseAmount / 10));
                            DeltaBonus *= .75f;
                            if (DeltaBonus < .25f)
                                DeltaBonus = .25f;
                        }
                    }
                    NPC n = Main.npc[k];
                    if (n.active && n.friendly && projectile.Distance(n.Center) < distance && n.lifeMax > 5)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            KeyUtils.NewDustConverge(out int dust, n.Center, Vector2.Zero, 75, 163, scale: .75f, fixedScale: false);
                            Main.dust[dust].position += n.velocity;
                        }
                        n.life += HealAmount;
                        if (n.life > n.lifeMax)
                            n.life = n.lifeMax;
                        CombatText.NewText(n.getRect(), CombatText.HealLife, HealAmount, dot: true);
                    }
                }
                PulseAmount++;
                DeltaBonus = 1;
            }
            if (CureTimer >= 150)
                projectile.Kill();
        }
    }
}
