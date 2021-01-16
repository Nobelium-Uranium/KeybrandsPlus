using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using KeybrandsPlus.Items;
using KeybrandsPlus.Buffs;
using KeybrandsPlus.Items.Currency;
using System;
using Microsoft.Xna.Framework;
using static KeybrandsPlus.Helpers.KeyUtils;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class KeyPlayer : ModPlayer
    {
        public int statOldLife;
        public int PlayerDefense;

        public Item lastVisualizedSelectedItem;
        public Rectangle itemRectangle;

        private bool FixedDirection;
        private int FixedDir;

        public bool LuckySevens;

        public bool Moving;
        private bool NoHitsound;

        #region Abilities
        public bool VitalBlow;
        public bool AliveAndKicking;
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
        public float ChimeraMultiplier = 1;
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

            Moving = false;

            NoHitsound = false;
            
            #region Abilities
            VitalBlow = false;
            AliveAndKicking = false;
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
            if (NoHitsound)
                playSound = false;
            if (LeafBracerTimer > 0)
                return false;
            if (LuckySevens)
                damage = 7777;
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (MPRage)
                if (damage / 3 < 1)
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
                player.AddBuff(BuffType<SecondChanceCooldown>(), Main.expertMode ? 1200 : 600);
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
            if (Main.expertMode)
                PlayerDefense = (int)Math.Ceiling(player.statDefense * 0.75f);
            else
                PlayerDefense = (int)Math.Ceiling(player.statDefense * 0.5f);
            if (GliderInactive)
                player.wingsLogic = 0;
            if (ChimeraBleed)
            {
                SuperBleedTimer++;
                if (player.mount.Type == MountID.Ufo && (player.controlUp || player.controlDown) || player.controlLeft || player.controlRight || (player.controlJump && player.velocity.Y < 0f))
                    Moving = true;
                if ((SuperBleedTimer == 15 || SuperBleedTimer == 30 || SuperBleedTimer == 45 || SuperBleedTimer == 60) && Main.rand.NextBool(10))
                {
                    if (Main.rand.NextBool(5))
                        for (int i = 0; i < Main.rand.Next(9, 21); i++)
                        {
                            float RandX = Main.rand.NextFloat(0, 2f);
                            if (Main.rand.NextBool())
                                RandX *= -1;
                            float RandY = Main.rand.NextFloat();
                            if (Main.rand.NextBool())
                                RandY *= -3;
                            Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(MathHelper.ToRadians(5));
                            Projectile.NewProjectile(player.Center, RandVelocity, ProjectileType<Projectiles.Blood>(), 0, 0);
                        }
                    else
                        for (int i = 0; i < Main.rand.Next(6, 10); i++)
                        {
                            float RandX = Main.rand.NextFloat(0, 2f);
                            if (Main.rand.NextBool())
                                RandX *= -1;
                            float RandY = Main.rand.NextFloat();
                            if (Main.rand.NextBool())
                                RandY *= -3;
                            Vector2 RandVelocity = new Vector2(RandX, RandY).RotatedByRandom(MathHelper.ToRadians(10));
                            Projectile.NewProjectile(player.Center, RandVelocity, ProjectileType<Projectiles.Blood>(), 0, 0);
                        }
                }
                if (SuperBleedTimer >= 60)
                {
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
                    if (LeafBracerTimer <= 0)
                    {
                        int OldImmuneTime = player.immuneTime;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Stab").WithVolume(0.5f).WithPitchVariance(0.15f), player.Center);
                        player.immuneTime = 0;
                        player.immune = false;
                        NoHitsound = true;
                        player.Hurt(PlayerDeathReason.ByCustomReason(DeathText), (int)(1 * ChimeraMultiplier) + (PlayerDefense / 2), 0);
                        NoHitsound = false;
                        player.immuneTime = OldImmuneTime;
                        if (Main.expertMode || player.bleed)
                            ChimeraMultiplier *= 2;
                        else
                            ChimeraMultiplier *= 1.5f;
                        if (ChimeraMultiplier > 5000)
                            ChimeraMultiplier = 5000;
                    }
                }
            }
            else
            {
                ChimeraMultiplier = 1;
                SuperBleedTimer = 30;
            }
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

        public override void PostItemCheck()
        {
            Item item = player.inventory[player.selectedItem];
            Item item2 = (player.itemAnimation > 0) ? player.HeldItem : item;
            if (player.itemAnimation > 0)
            {
                bool dontattack = default;
                itemRectangle = default;
                ItemCheck_GetMeleeHitbox(item2, item2.getRect(), out dontattack, out itemRectangle);
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (VitalBlow && target.life >= (float)target.lifeMax * .9f)
                damage *= 2;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (item.GetGlobalItem<KeyItem>().IsKeybrand)
            {
                Vector2 point = itemRectangle.Center.ToVector2();
                Vector2 positionInWorld = ClosestPointInRect(target.Hitbox, point);
                if (item.type == ItemType<Items.Weapons.Keybrand>() || item.type == ItemType<Items.Weapons.KeybrandD>() || item.type == ItemType<Items.Weapons.TrueKeybrand>() || item.type == ItemType<Items.Weapons.TrueKeybrandD>())
                    for (int i = 0; i < Main.rand.Next(2, 5); i++)
                    {
                        int dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.KeybrandHit>(), Scale: Main.rand.NextFloat(.75f, 1.25f));
                        Main.dust[dust].velocity *= Main.rand.NextFloat(1.25f, 1.5f);
                    }
                else if (item.type == ItemType<Items.Weapons.Developer.Chimera>())
                    for (int i = 0; i < Main.rand.Next(2, 5); i++)
                    {
                        int dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.ChimeraHit>(), Scale: Main.rand.NextFloat(.75f, 1.25f));
                        Main.dust[dust].velocity *= Main.rand.NextFloat(1.25f, 1.5f);
                    }
                else if (item.type == ItemType<Items.Weapons.Other.BleakMidnight>())
                    for (int i = 0; i < Main.rand.Next(2, 5); i++)
                    {
                        int dust = Dust.NewDust(positionInWorld, 0, 0, DustType<Dusts.Keybrand.MidnightHit>(), Scale: Main.rand.NextFloat(.75f, 1.25f));
                        Main.dust[dust].velocity *= Main.rand.NextFloat(1.25f, 1.5f);
                    }
            }
        }

        private void ItemCheck_GetMeleeHitbox(Item sItem,Rectangle heldItemFrame, out bool dontAttack, out Rectangle itemRectangle)
        {
            dontAttack = false;
            itemRectangle = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, 32, 32);
            if (!Main.dedServ)
                itemRectangle = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, heldItemFrame.Width, heldItemFrame.Height);
            if (player.direction == -1)
                itemRectangle.X -= itemRectangle.Width;
            if (player.gravDir == 1f)
                itemRectangle.Y -= itemRectangle.Height;
            if (sItem.useStyle == 1)
            {
                if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.333)
                {
                    if (player.direction == -1)
                    {
                        itemRectangle.X -= (int)((double)itemRectangle.Width * 1.4 - (double)itemRectangle.Width);
                    }
                    itemRectangle.Width = (int)((double)itemRectangle.Width * 1.4);
                    itemRectangle.Y += (int)((double)itemRectangle.Height * 0.5 * (double)player.gravDir);
                    itemRectangle.Height = (int)((double)itemRectangle.Height * 1.1);
                }
                else if (!((double)player.itemAnimation < (double)player.itemAnimationMax * 0.666))
                {
                    if (player.direction == 1)
                    {
                        itemRectangle.X -= (int)((double)itemRectangle.Width * 1.2);
                    }
                    itemRectangle.Width *= 2;
                    itemRectangle.Y -= (int)(((double)itemRectangle.Height * 1.4 - (double)itemRectangle.Height) * (double)player.gravDir);
                    itemRectangle.Height = (int)((double)itemRectangle.Height * 1.4);
                }
            }
            else if (sItem.useStyle == 3)
            {
                if ((double)player.itemAnimation > (double)player.itemAnimationMax * 0.666)
                {
                    dontAttack = true;
                }
                else
                {
                    if (player.direction == -1)
                    {
                        itemRectangle.X -= (int)((double)itemRectangle.Width * 1.4 - (double)itemRectangle.Width);
                    }
                    itemRectangle.Width = (int)((double)itemRectangle.Width * 1.4);
                    itemRectangle.Y += (int)((double)itemRectangle.Height * 0.6);
                    itemRectangle.Height = (int)((double)itemRectangle.Height * 0.6);
                }
            }
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

        public override void UpdateBadLifeRegen()
        {
            if (LeafBracerTimer > 0 && player.lifeRegen < 0)
                player.lifeRegen = 0;
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
