using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using KeybrandsPlus.Buffs;
using KeybrandsPlus.Items.Currency;
using System;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class KeyPlayer : ModPlayer
    {
        public int statOldLife;

        private bool FixedDirection;
        private int FixedDir;

        public bool LuckySevens;

        #region Abilities
        public bool Defender;
        public bool DefenderPlus;
        public int DefenderThreshold;
        public bool DamageControl;
        public bool DamageControlPlus;
        public bool LeafBracer;
        public bool MPRage;
        public bool DangerBracer;
        public bool MPHaste;
        public bool MPHastera;
        public bool MPHastega;
        public bool MPHasteza;
        public bool CritMPHastega;
        public bool CritMPHasteza;
        public bool DarkAffinity;
        public bool MunnyConverter;
        #endregion

        #region Buffs
        public bool CureCooldown;
        public bool HeartlessAngel;
        public bool ChimeraBleed;
        public bool ElixirSickness;
        public bool ElixirGuard;
        public bool PanaceaSickness;
        public bool SecondChance;
        public bool SCCooldown;
        public bool Stop;

        public bool Stimulated;
        public bool Divinity;

        public float FireBoost;
        public float BlizzardBoost;
        public float ThunderBoost;
        public float AeroBoost;
        public float WaterBoost;
        public float DarkBoost;
        #endregion

        #region Equips
        public bool GliderInactive;
        public bool CrownCharm;
        public bool MasterTreasureMagnet;
        public bool TreasureMagnetPlus;
        public bool TreasureMagnet;
        //Equipment
        public bool BeltEquipped;
        public bool ChainEquipped;
        public bool RingEquipped;
        //Belt
        public int BeltDefense;
        //Chain
        public float ChainResistFire;
        public float ChainResistBlizzard;
        public float ChainResistThunder;
        public float ChainResistAero;
        public float ChainResistWater;
        public float ChainResistDark;
        //Ring
        public float RingAttackPhysical;
        public float RingAttackMagic;
        //Ribbon
        public float RibbonEndurance;
        #endregion

        private int SuperBleedTimer;
        
        public float KeybrandMelee;
        public float KeybrandRanged;
        public float KeybrandMagic;

        public int LightAlignment;
        public int DarkAlignment;
        public int TotalAlignment;

        public int LeafBracerTimer;
        public int ChimeraLifestealCD;

        public override void ResetEffects()
        {
            statOldLife = 0;

            LuckySevens = false;

            #region Abilities
            Defender = false;
            DefenderPlus = false;
            DefenderThreshold = 0;
            DamageControl = false;
            DamageControlPlus = false;
            LeafBracer = false;
            MPRage = false;
            DangerBracer = false;
            MPHaste = false;
            MPHastera = false;
            MPHastega = false;
            MPHasteza = false;
            CritMPHastega = false;
            CritMPHasteza = false;
            DarkAffinity = false;
            MunnyConverter = false;
            #endregion

            #region Buffs
            CureCooldown = false;
            HeartlessAngel = false;
            ChimeraBleed = false;
            ElixirSickness = false;
            ElixirGuard = false;
            PanaceaSickness = false;
            SecondChance = false;
            SCCooldown = false;
            Stop = false;

            Stimulated = false;
            Divinity = false;

            FireBoost = 0;
            BlizzardBoost = 0;
            ThunderBoost = 0;
            AeroBoost = 0;
            WaterBoost = 0;
            DarkBoost = 0;
            #endregion

            #region Equips
            GliderInactive = false;
            CrownCharm = false;
            MasterTreasureMagnet = false;
            TreasureMagnetPlus = false;
            TreasureMagnet = false;
            BeltEquipped = false;
            ChainEquipped = false;
            RingEquipped = false;
            BeltDefense = 0;
            ChainResistFire = 0;
            ChainResistBlizzard = 0;
            ChainResistThunder = 0;
            ChainResistAero = 0;
            ChainResistWater = 0;
            ChainResistDark = 0;
            RingAttackPhysical = 0;
            RingAttackMagic = 0;
            RibbonEndurance = 0;
            #endregion

            KeybrandMelee = 0;
            KeybrandRanged = 0;
            KeybrandMagic = 0;

            LightAlignment = 0;
            DarkAlignment = 0;
            TotalAlignment = 0;
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            player.statDefense += BeltDefense;
            player.statDefense = (int)(player.statDefense * (1 + RibbonEndurance));
            KeybrandMelee += RingAttackPhysical;
            KeybrandRanged += RingAttackPhysical;
            KeybrandMagic += RingAttackMagic;
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (npc.GetGlobalNPC<KeyNPC>().Fire)
                damage -= (int)(damage * ChainResistFire);
            if (npc.GetGlobalNPC<KeyNPC>().Blizzard)
                damage -= (int)(damage * ChainResistBlizzard);
            if (npc.GetGlobalNPC<KeyNPC>().Thunder)
                damage -= (int)(damage * ChainResistThunder);
            if (npc.GetGlobalNPC<KeyNPC>().Aero)
                damage -= (int)(damage * ChainResistAero);
            if (npc.GetGlobalNPC<KeyNPC>().Water)
                damage -= (int)(damage * ChainResistWater);
            if (npc.GetGlobalNPC<KeyNPC>().Dark)
                damage -= (int)(damage * ChainResistDark);
            if (DamageControlPlus && player.statLife <= player.statLifeMax2 / 2)
                damage /= 2;
            else if (DamageControl && player.statLife <= player.statLifeMax2 / 5)
                damage /= 2;
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (proj.GetGlobalProjectile<KeyProjectile>().Fire)
                damage -= (int)(damage * ChainResistFire);
            if (proj.GetGlobalProjectile<KeyProjectile>().Blizzard)
                damage -= (int)(damage * ChainResistBlizzard);
            if (proj.GetGlobalProjectile<KeyProjectile>().Thunder)
                damage -= (int)(damage * ChainResistThunder);
            if (proj.GetGlobalProjectile<KeyProjectile>().Aero)
                damage -= (int)(damage * ChainResistAero);
            if (proj.GetGlobalProjectile<KeyProjectile>().Water)
                damage -= (int)(damage * ChainResistWater);
            if (proj.GetGlobalProjectile<KeyProjectile>().Dark)
                damage -= (int)(damage * ChainResistDark);
            if (DamageControlPlus && player.statLife <= player.statLifeMax2 / 2)
                damage /= 2;
            else if (DamageControl && player.statLife <= player.statLifeMax2 / 5)
                damage /= 2;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (LeafBracerTimer > 0)
                return false;
            if (LuckySevens)
                damage = 7777;
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (MPRage)
                if (damage / 3 <= 0)
                {
                    player.ManaEffect(1);
                    player.statMana += 1;
                }
                else
                {
                    player.ManaEffect((int)(damage / 3));
                    player.statMana += (int)(damage / 3);
                }
            statOldLife = player.statLife;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (SecondChance && !SCCooldown && statOldLife > 1)
            {
                Main.PlaySound(SoundID.Item67, player.position);
                player.statLife = 1;
                LeafBracerTimer = 180;
                player.ClearBuff(BuffID.Bleeding);
                player.ClearBuff(BuffID.Poisoned);
                player.ClearBuff(BuffID.OnFire);
                player.ClearBuff(BuffID.Venom);
                player.ClearBuff(BuffID.Darkness);
                player.ClearBuff(BuffID.Blackout);
                player.ClearBuff(BuffID.Cursed);
                player.ClearBuff(BuffID.Frostburn);
                player.ClearBuff(BuffID.Confused);
                player.ClearBuff(BuffID.Slow);
                player.ClearBuff(BuffID.Weak);
                player.ClearBuff(BuffID.Silenced);
                player.ClearBuff(BuffID.BrokenArmor);
                player.ClearBuff(BuffID.CursedInferno);
                player.ClearBuff(BuffID.Chilled);
                player.ClearBuff(BuffID.Ichor);
                player.ClearBuff(BuffID.ShadowFlame);
                player.ClearBuff(BuffID.Electrified);
                player.ClearBuff(BuffID.Rabies);
                player.ClearBuff(BuffID.VortexDebuff);
                player.ClearBuff(BuffID.WitheredArmor);
                player.ClearBuff(BuffID.WitheredWeapon);
                player.ClearBuff(BuffID.OgreSpit);
                player.ClearBuff(BuffID.Frozen);
                player.ClearBuff(BuffID.Stoned);
                player.ClearBuff(BuffID.Webbed);
                player.AddBuff(BuffType<SecondChanceCooldown>(), 600);
                player.ClearBuff(BuffType<SecondChance>());
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (ChimeraBleed)
                for (int i = 0; i < player.statLifeMax2 / 20; i++)
                {
                    float RandX = Main.rand.NextFloat(0, 3);
                    if (Main.rand.NextBool())
                        RandX *= -1;
                    float RandY = Main.rand.NextFloat(0, 3);
                    if (Main.rand.NextBool())
                        RandY *= -1;
                    Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(MathHelper.ToRadians(5));
                    Projectile.NewProjectile(player.Center, RandVelocity, ProjectileType<Projectiles.Blood>(), 0, 0);
                }
            if (!pvp)
            {
                if (player.trashItem.type == ItemType<Munny>())
                    player.trashItem.SetDefaults(0, false);
                int munny = 0;
                for (int i = 0; i < 58; i++)
                {
                    if (player.inventory[i].type == ItemType<Munny>() && player.inventory[i].stack > 0)
                    {
                        munny += player.inventory[i].stack;
                    }
                }
                int amount = (munny / 4);
                if (!MunnyConverter && amount > 0)
                {
                    int Munny;
                    if (amount > 9999)
                    {
                        CombatText.NewText(player.getRect(), Color.Goldenrod, "Dropped 9999 Munny, " + (amount - 9999) + " was lost", true);
                        Munny = Item.NewItem(player.getRect(), ItemType<Munny>(), 9999);
                    }
                    else
                    {
                        CombatText.NewText(player.getRect(), Color.Goldenrod, "Dropped " + amount + " Munny", true);
                        Munny = Item.NewItem(player.getRect(), ItemType<Munny>(), amount);
                    }
                    Main.item[Munny].GetGlobalItem<KeyItem>().TimeLeft = 3600;
                }
                for (int i = 0; i < 58 && amount > 0; i++)
                {
                    if (player.inventory[i].stack > 0 && player.inventory[i].type == ItemType<Items.Currency.Munny>())
                    {
                        if (player.inventory[i].stack >= amount)
                        {
                            player.inventory[i].stack -= amount;
                            amount = 0;
                        }
                        else
                        {
                            amount -= player.inventory[i].stack;
                            player.inventory[i].stack = 0;
                        }
                        if (player.inventory[i].stack <= 0)
                        {
                            player.inventory[i].SetDefaults(0, false);
                        }
                        if (amount <= 0)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public override void PreUpdate()
        {
            if (GliderInactive)
                player.wingsLogic = 0;
        }

        public override void PostUpdate()
        {
            if (GliderInactive)
                player.wingsLogic = 0;
            if (ChimeraBleed)
            {
                int PlayerDefense;
                if (Main.expertMode)
                    PlayerDefense = (int)Math.Ceiling(player.statDefense * 0.75f);
                else
                    PlayerDefense = (int)Math.Ceiling(player.statDefense * 0.5f);
                SuperBleedTimer++;
                if (SuperBleedTimer >= 30)
                {
                    for (int i = 0; i < Main.rand.Next(1, 11); i++)
                    {
                        float RandX = Main.rand.NextFloat(0, 1.5f);
                        if (Main.rand.NextBool())
                            RandX *= -1;
                        float RandY = Main.rand.NextFloat(0, 1.5f);
                        if (Main.rand.NextBool())
                            RandY *= -1;
                        Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(player.Center, RandVelocity, ProjectileType<Projectiles.Blood>(), 0, 0);
                    }
                    string DeathText;
                    switch (Main.rand.Next(3))
                    {
                        case 1:
                            DeathText = player.name + " was completely drained of blood.";
                            break;
                        case 2:
                            DeathText = player.name + "'s body became a mere husk.";
                            break;
                        default:
                            DeathText = player.name + " couldn't find the IV bag.";
                            break;
                    }
                    SuperBleedTimer = 0;
                    if (!player.immune && player.immuneTime <= 0)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Stab").WithVolume(0.5f).WithPitchVariance(0.1f), player.Center);
                        player.Hurt(PlayerDeathReason.ByCustomReason(DeathText), 15 + PlayerDefense, 0);
                        player.immuneTime = 0;
                    }
                }
            }
            else
                SuperBleedTimer = 15;
            if (ChimeraLifestealCD > 0)
                ChimeraLifestealCD -= 1;
            if (LeafBracerTimer > 0)
            {
                player.immune = true;
                LeafBracerTimer -= 1;
            }
            if (DefenderPlus && player.statLife <= player.statLifeMax2 / 2)
            {
                DefenderThreshold = player.statDefense / 10;
                if (DefenderThreshold < 4)
                    DefenderThreshold = 4;
                player.statDefense += DefenderThreshold;
            }
            else if (Defender && player.statLife <= player.statLifeMax2 / 5)
                player.statDefense += 4;
            if (CritMPHasteza && player.statLife <= player.statLifeMax2 / 5)
            {
                player.manaRegenCount += (int)(player.manaRegen * 0.75f);
            }
            else if (CritMPHastega && player.statLife <= player.statLifeMax2 / 5)
            {
                player.manaRegenCount += (int)(player.manaRegen * 0.5f);
            }
            else if (MPHasteza)
            {
                player.manaRegenCount += (int)(player.manaRegen * 0.75f);
            }
            else if (MPHastega)
            {
                player.manaRegenCount += (int)(player.manaRegen * 0.5f);
            }
            else if (MPHastera)
            {
                player.manaRegenCount += (int)(player.manaRegen * 0.3f);
            }
            else if (MPHaste)
            {
                player.manaRegenCount += (int)(player.manaRegen * 0.1f);
            }
            if (DarkAffinity && player.lifeRegen < 0)
            {
                float AlignmentFactor = DarkAlignment - LightAlignment;
                if (AlignmentFactor < 50)
                    AlignmentFactor = 50;
                AlignmentFactor /= 100;
                player.manaRegenCount += (int)(player.manaRegen * AlignmentFactor);
            }
            if (LightAlignment < 0)
                LightAlignment = 0;
            if (DarkAlignment < 0)
                DarkAlignment = 0;
            TotalAlignment = LightAlignment + DarkAlignment;
        }

        public override void PreUpdateMovement()
        {
            if (Stop)
            {
                if (!FixedDirection)
                {
                    FixedDir = player.direction;
                    FixedDirection = true;
                }
                player.direction = FixedDir;

                player.position = player.oldPosition;
                player.velocity = Vector2.Zero;
            }
            else
                FixedDirection = false;
        }

        public override void PostUpdateBuffs()
        {
            if (SCCooldown)
                player.lifeSteal = 0;
            if (Stop)
            {
                int dust = Dust.NewDust(player.Center, 0, 0, DustID.AncientLight, Scale: 2f);
                Main.dust[dust].velocity *= 5f;
                Main.dust[dust].noGravity = true;
                player.noItems = true;
            }
            if (Stimulated)
            {
                KeybrandMelee += .25f;
                KeybrandRanged += .25f;
                KeybrandMagic -= .4f;
                ChainResistFire -= .4f;
                ChainResistBlizzard -= .4f;
                ChainResistThunder -= .4f;
                ChainResistAero -= .4f;
                ChainResistWater -= .4f;
                ChainResistDark -= .4f;
            }
            if (Divinity)
            {
                KeybrandMagic += .25f;
                KeybrandMelee -= .4f;
                KeybrandRanged -= .4f;
                RibbonEndurance -= .4f;
            }
        }

        public override void UpdateDead()
        {
            if (MunnyConverter)
                for (int i = 0; i < 58; i++)
                    if (player.inventory[i].type == ItemType<Munny>() && player.inventory[i].stack > 0)
                        player.inventory[i].SetDefaults(0, false);
        }
    }
}
