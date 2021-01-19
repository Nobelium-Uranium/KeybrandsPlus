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
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(0, 0);
            projectile.friendly = true;
            projectile.damage = 0;
            projectile.ignoreWater = true;
            CureLevel = (int)Math.Round(projectile.ai[0]);
            if (CureLevel < 0)
                CureLevel = 0;
            else if (CureLevel > 3)
                CureLevel = 3;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            Boost = 1 + owner.GetModPlayer<Globals.KeyPlayer>().KeybrandMagic;
            projectile.velocity = Vector2.Zero;
            float distance;
            switch (CureLevel)
            {
                default:
                    distance = 150 * (.5f+ (Boost / 2));
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
            if (CureTimer == 60 || CureTimer == 120 || CureTimer == 180)
            {
                Main.PlaySound(SoundID.Item4, projectile.Center);
                for (int i = 0; i < 25; i++)
                {
                    int dust = Dust.NewDust(projectile.Center - new Vector2(4, 4), 0, 0, 163);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 5f * (1 + (float)CureLevel / 2) * Boost;
                }
                for (int k = 0; k < 200; k++)
                {
                    Player p = Main.player[k];
                    if (p.active && !p.dead && projectile.Distance(p.Center) < distance && p.team == owner.team)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int dust = Dust.NewDust(p.Center - new Vector2(4, 4), 0, 0, 163);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 2f;
                        }
                        switch (CureLevel)
                        {
                            default:
                                p.statLife += (int)(50 * Boost);
                                if (p.statLife > p.statLifeMax2)
                                    p.statLife = p.statLifeMax2;
                                CombatText.NewText(p.getRect(), CombatText.HealLife, (int)(50 * Boost), dot: true);
                                break;
                            case 1:
                                p.statLife += (int)(100 * Boost);
                                if (p.statLife > p.statLifeMax2)
                                    p.statLife = p.statLifeMax2;
                                CombatText.NewText(p.getRect(), CombatText.HealLife, (int)(100 * Boost), dot: true);
                                break;
                            case 2:
                                p.statLife += (int)(150 * Boost);
                                if (p.statLife > p.statLifeMax2)
                                    p.statLife = p.statLifeMax2;
                                CombatText.NewText(p.getRect(), CombatText.HealLife, (int)(150 * Boost), dot: true);
                                break;
                            case 3:
                                p.statLife += (int)(200 * Boost);
                                if (p.statLife > p.statLifeMax2)
                                    p.statLife = p.statLifeMax2;
                                CombatText.NewText(p.getRect(), CombatText.HealLife, (int)(200 * Boost), dot: true);
                                break;
                        }
                    }
                    NPC n = Main.npc[k];
                    if (n.active && n.friendly && projectile.Distance(n.Center) < distance && n.lifeMax > 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int dust = Dust.NewDust(n.Center - new Vector2(4, 4), 0, 0, 163);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 2f;
                        }
                        switch (CureLevel)
                        {
                            default:
                                n.life += (int)(50 * Boost);
                                if (n.life > n.lifeMax)
                                    n.life = n.lifeMax;
                                CombatText.NewText(n.getRect(), CombatText.HealLife, (int)(50 * Boost), dot: true);
                                break;
                            case 1:
                                n.life += (int)(100 * Boost);
                                if (n.life > n.lifeMax)
                                    n.life = n.lifeMax;
                                CombatText.NewText(n.getRect(), CombatText.HealLife, (int)(100 * Boost), dot: true);
                                break;
                            case 2:
                                n.life += (int)(150 * Boost);
                                if (n.life > n.lifeMax)
                                    n.life = n.lifeMax;
                                CombatText.NewText(n.getRect(), CombatText.HealLife, (int)(150 * Boost), dot: true);
                                break;
                            case 3:
                                n.life += (int)(200 * Boost);
                                if (n.life > n.lifeMax)
                                    n.life = n.lifeMax;
                                CombatText.NewText(n.getRect(), CombatText.HealLife, (int)(200 * Boost), dot: true);
                                break;
                        }
                    }
                }
            }
            if (CureTimer >= 180)
                projectile.Kill();
        }
    }
}
