using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using KeybrandsPlus.Buffs;
using KeybrandsPlus.Items.Currency;
using System;
using Microsoft.Xna.Framework;
using static KeybrandsPlus.Helpers.KeyUtils;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.ModLoader.IO;

namespace KeybrandsPlus.Globals
{
    class KeyPlayer : ModPlayer
    {
        public int StoredUUIDX;
        public int StoredUUIDY;
        public int StoredUUIDZ;
        public Vector3 UUID;

        #region Glowmasks
        public bool HideGlowmask;
        public bool AvaliHelmet;
        public bool AvaliShirt;
        public bool AvaliPants;
        #endregion

        public bool NoFlight;

        public int statOldLife;
        public int PlayerDefense;

        public Item lastVisualizedSelectedItem;
        public Rectangle itemRectangle;

        private bool FixedDirection;
        private int FixedDir;

        public bool KeybrandSelected;
        public int HeldKeybrands;
        public bool KeybrandLimitReached;

        public bool LuckySevens;

        public bool Moving;
        private bool NoHitsound;

        #region Abilities
        public bool VitalBlow;
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
        public bool HeartlessAngel;
        public bool ChimeraBleed;
        public float ChimeraMultiplier = 1;
        public bool EtherSickness;
        public bool TurboExhaustion;
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
        public bool BlossomWings;
        public bool GliderInactive;
        public bool CrownCharm;
        public bool MasterTreasureMagnet;
        public bool TreasureMagnetPlus;
        public bool TreasureMagnet;
        //Equipment
        public bool SpecialEquipped;
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
        public float ChainResistNil;
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

        public float CureBoost;

        public int LightAlignment;
        public int DarkAlignment;
        public int TotalAlignment;

        public int LeafBracerTimer;
        public int ChimeraLifestealCD;

        public int ChargedCrystals;
        public bool showMP = true;
        public bool rechargeMP = true;
        public int maxMP = 100;
        public int currentMP;
        public int maxDelta = 200;
        public int currentDelta;
        public int deltaDecayDelay;
        public float deltaDecayTimer;

        public float maxRechargeMPTimer = 1;
        public float rechargeMPTimer = 1;
        public float rechargeMPRate;
        public float rechargeMPToastTimer = 60;

        public override void ResetEffects()
        {
            if (StoredUUIDX <= 0)
                StoredUUIDX = Main.rand.Next(1, 256);
            if (StoredUUIDY <= 0)
                StoredUUIDY = Main.rand.Next(1, 256);
            if (StoredUUIDZ <= 0)
                StoredUUIDZ = Main.rand.Next(1, 256);
            UUID = new Vector3(StoredUUIDX, StoredUUIDY, StoredUUIDZ);

            maxMP = 100 + (25 * ChargedCrystals);
            maxDelta = 2 * maxMP;

            #region Glowmasks
            HideGlowmask = false;
            AvaliHelmet = false;
            AvaliShirt = false;
            AvaliPants = false;
            #endregion  

            statOldLife = 0;

            KeybrandSelected = false;
            HeldKeybrands = 0;

            LuckySevens = false;

            Moving = false;

            NoHitsound = false;

            #region Abilities
            VitalBlow = false;
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
            HeartlessAngel = false;
            ChimeraBleed = false;
            EtherSickness = false;
            TurboExhaustion = false;
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
            BlossomWings = false;
            GliderInactive = false;
            CrownCharm = false;
            MasterTreasureMagnet = false;
            TreasureMagnetPlus = false;
            TreasureMagnet = false;
            SpecialEquipped = false;
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
            ChainResistNil = 0;
            RingAttackPhysical = 0;
            RingAttackMagic = 0;
            RibbonEndurance = 0;
            #endregion

            KeybrandMelee = 0;
            KeybrandRanged = 0;
            KeybrandMagic = 0;

            CureBoost = 0;

            LightAlignment = 0;
            DarkAlignment = 0;
            TotalAlignment = 0;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write(ChargedCrystals);
            packet.Write(StoredUUIDX);
            packet.Write(StoredUUIDY);
            packet.Write(StoredUUIDZ);
            packet.Send(toWho, fromWho);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                { "ChargedCrystals", ChargedCrystals },
                { "StoredUUIDX", StoredUUIDX },
                { "StoredUUIDY", StoredUUIDY },
                { "StoredUUIDZ", StoredUUIDZ },
            };
        }

        public override void Load(TagCompound tag)
        {
            ChargedCrystals = tag.GetInt("ChargedCrystals");
            StoredUUIDX = tag.GetInt("StoredUUIDX");
            StoredUUIDY = tag.GetInt("StoredUUIDY");
            StoredUUIDZ = tag.GetInt("StoredUUIDZ");
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            if (!mediumcoreDeath)
            {
                Item item = new Item();
                item.SetDefaults(ItemType<Items.Weapons.WoodenKeybrand>());
                items.Add(item);
                item = new Item();
                item.SetDefaults(ItemType<Items.Consumables.MP.Ether>());
                item.stack = 10;
                items.Add(item);
                item = new Item();
                item.SetDefaults(ItemType<Items.Other.DevNull>());
                items.Add(item);
            }
        }

        public override void PostUpdateEquips()
        {
            if (NoFlight)
                player.mount.Dismount(player);
            KeybrandLimitReached = HeldKeybrands > 5;
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (NoFlight)
            {
                player.wingTime *= 0;
                player.wingTimeMax *= 0;
            }
            player.statDefense += BeltDefense;
            player.statDefense = (int)(player.statDefense * (1 + RibbonEndurance));
            KeybrandMelee += RingAttackPhysical;
            KeybrandRanged += RingAttackPhysical;
            KeybrandMagic += RingAttackMagic;
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
            {
                if (damage / 3 < 1)
                {
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 1);
                    currentMP++;
                }
                else
                {
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, (int)(damage / 3));
                    currentMP += (int)(damage / 3);
                }
                if (currentMP > maxMP)
                    currentMP = maxMP;
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
                player.AddBuff(BuffType<SecondChanceCooldown>(), 900);
                player.ClearBuff(BuffType<SecondChance>());
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure")
                Item.NewItem(player.getRect(), ItemType<Items.Consumables.IceCream>());
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
                        if (Main.myPlayer == player.whoAmI)
                            CombatText.NewText(player.getRect(), Color.Goldenrod, "Dropped 9999 Munny, " + (amount - 9999) + " was lost", true);
                        Munny = Item.NewItem(player.getRect(), ItemType<Munny>(), 9999);
                    }
                    else
                    {
                        if (Main.myPlayer == player.whoAmI)
                            CombatText.NewText(player.getRect(), Color.Goldenrod, "Dropped " + amount + " Munny", true);
                        Munny = Item.NewItem(player.getRect(), ItemType<Munny>(), amount);
                    }
                    Main.item[Munny].GetGlobalItem<KeyItem>().PlayerDropped = true;
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
            if (ChargedCrystals > 8)
                ChargedCrystals = 8;
            else if (ChargedCrystals < 0)
                ChargedCrystals = 0;

            if (NoFlight || GliderInactive)
                player.wingsLogic = 0;

            if (DefenderPlus && player.statLife <= player.statLifeMax2 / 2)
            {
                DefenderThreshold = player.statDefense / 10;
                if (DefenderThreshold < 4)
                    DefenderThreshold = 4;
                player.statDefense += DefenderThreshold;
            }
            else if (Defender && player.statLife <= player.statLifeMax2 / 5)
                player.statDefense += 4;

            if (player.dead)
            {
                rechargeMP = false;
                currentMP = maxMP;
                rechargeMPToastTimer = 60;
            }
            if (maxMP < 100)
                maxMP = 100;
            else if (maxMP > 300)
                maxMP = 300;
            if (currentDelta > 0)
            {
                if (deltaDecayDelay > 150)
                {
                    if (deltaDecayTimer >= 5f)
                    {
                        deltaDecayTimer = 0;
                        currentDelta--;
                    }
                    else
                        deltaDecayTimer += 1 * (maxDelta / 100);
                }
                else
                    deltaDecayDelay++;
            }
            else
            {
                deltaDecayDelay = 0;
                deltaDecayTimer = 0;
            }
            if (currentDelta < 0)
                currentDelta = 0;
            if (!rechargeMP)
            {
                if (ModContent.GetInstance<KeyServerConfig>().MPRegen && currentMP < maxMP && Main.GameUpdateCount % 600 == 0)
                    currentMP++;
                if (currentDelta >= maxDelta)
                {
                    int RestoreMP = Main.rand.Next(3, 11);
                    if (Main.myPlayer == player.whoAmI)
                        CombatText.NewText(player.getRect(), Color.DodgerBlue, RestoreMP);
                    currentMP += RestoreMP;
                    currentDelta = 0;
                }
                if (currentMP > maxMP)
                    currentMP = maxMP;
                if (maxRechargeMPTimer > 0)
                    maxRechargeMPTimer = 0;
                if (currentMP <= 0)
                {
                    currentMP = maxMP;
                    rechargeMPTimer = 1200;
                    if (rechargeMPTimer < 300)
                        rechargeMPTimer = 300;
                    maxRechargeMPTimer = rechargeMPTimer;
                    rechargeMP = true;
                }
            }
            else
            {
                if (currentDelta >= maxDelta)
                {
                    if (Main.myPlayer == player.whoAmI)
                        CombatText.NewText(player.getRect(), Color.DodgerBlue, "-1s");
                    rechargeMPTimer -= 60;
                    currentDelta = 0;
                }

                rechargeMPRate = 1;
                if (HeldKeybrands <= 5)
                { 
                    if (CritMPHasteza && player.statLife <= player.statLifeMax2 / 5)
                    {
                        rechargeMPRate *= 1.7f;
                    }
                    else if (CritMPHastega && player.statLife <= player.statLifeMax2 / 5)
                    {
                        rechargeMPRate *= 1.5f;
                    }
                    else if (MPHasteza)
                    {
                        rechargeMPRate *= 1.7f;
                    }
                    else if (MPHastega)
                    {
                        rechargeMPRate *= 1.5f;
                    }
                    else if (MPHastera)
                    {
                        rechargeMPRate *= 1.3f;
                    }
                    else if (MPHaste)
                    {
                        rechargeMPRate *= 1.1f;
                    }
                    if (DarkAffinity && player.lifeRegen < 0)
                    {
                        float AlignmentFactor = DarkAlignment - LightAlignment;
                        if (AlignmentFactor < 50)
                            AlignmentFactor = 50;
                        AlignmentFactor /= 100;

                        rechargeMPRate *= 1 + AlignmentFactor;
                    }
                }
                if (rechargeMPRate > 3)
                    rechargeMPRate = 3;
                else if (rechargeMPRate < 0)
                    rechargeMPRate = 0;
                rechargeMPTimer -= rechargeMPRate;

                currentMP = (int)MathHelper.Lerp(maxMP, 0, 1f - rechargeMPTimer / (float)maxRechargeMPTimer);

                if (rechargeMPTimer <= 0)
                {
                    rechargeMPTimer = 0;
                    currentMP = maxMP;
                    rechargeMP = false;
                    rechargeMPToastTimer = 60;
                    currentDelta = 0;
                }
            }

            if (rechargeMPToastTimer > 0)
            {
                rechargeMPToastTimer *= .925f;
                if (rechargeMPToastTimer < .1f)
                    rechargeMPToastTimer = 0;
            }
        }

        public override void PostUpdate()
        {
            if (LightAlignment < 0)
                LightAlignment = 0;
            if (DarkAlignment < 0)
                DarkAlignment = 0;
            TotalAlignment = LightAlignment + DarkAlignment;
            if (Main.expertMode)
                PlayerDefense = (int)Math.Ceiling(player.statDefense * 0.75f);
            else
                PlayerDefense = (int)Math.Ceiling(player.statDefense * 0.5f);
            if (player.mount.Type == MountID.Ufo && (player.controlUp || player.controlDown) || player.controlLeft || player.controlRight || (player.controlJump && player.velocity.Y < 0f))
                Moving = true;
            if (GliderInactive)
                player.wingsLogic = 0;
            if (ChimeraBleed)
            {
                SuperBleedTimer++;
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
                    if (Main.rand.NextBool(100))
                        DeathText = player.name + " withered away.";
                    else switch (Main.rand.Next(3))
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
                        SecondChance = false;
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
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybrandsPlus.QuickEther.JustPressed && !EtherSickness && currentMP < maxMP)
            {
                if (player.HasItem(ItemType<Unused.PGI>()))
                {
                    Main.PlaySound(SoundID.Item84);
                    if (Main.myPlayer == player.whoAmI)
                        CombatText.NewText(player.getRect(), Color.DodgerBlue, maxMP);
                    if (rechargeMP)
                    {
                        rechargeMP = false;
                        rechargeMPToastTimer = 60;
                        currentDelta = 0;
                    }
                    currentMP = maxMP;
                }
                else if (player.HasItem(ItemType<Items.Consumables.MP.TurboEther>()) && rechargeMP && !TurboExhaustion)
                {
                    foreach (Item item in player.inventory)
                        if (item.type == ItemType<Items.Consumables.MP.TurboEther>())
                        {
                            item.stack -= 1;
                            if (item.stack <= 0)
                                item.SetDefaults(0, false);
                            Main.PlaySound(SoundID.Item84);
                            if (Main.myPlayer == player.whoAmI)
                                CombatText.NewText(player.getRect(), Color.DodgerBlue, maxMP);
                            rechargeMP = false;
                            rechargeMPToastTimer = 60;
                            currentMP = maxMP;
                            currentDelta = 0;
                            player.AddBuff(BuffType<TurboExhaustion>(), 1800);
                            player.AddBuff(BuffType<EtherSickness>(), 300);
                            break;
                        }
                }
                else if (player.HasItem(ItemType<Items.Consumables.MP.MegaEther>()))
                {
                    foreach (Item item in player.inventory)
                        if (item.type == ItemType<Items.Consumables.MP.MegaEther>())
                        {
                            item.stack -= 1;
                            if (item.stack <= 0)
                                item.SetDefaults(0, false);
                            Main.PlaySound(SoundID.Item3);
                            if (Main.myPlayer == player.whoAmI)
                                CombatText.NewText(player.getRect(), Color.DodgerBlue, 150);
                            if (rechargeMP)
                                rechargeMPTimer = (int)(rechargeMPTimer * .25f);
                            else
                                currentMP += 150;
                            if (currentMP > maxMP)
                                currentMP = maxMP;
                            player.AddBuff(BuffType<EtherSickness>(), 600);
                            break;
                        }
                }
                else if (player.HasItem(ItemType<Items.Consumables.MP.HiEther>()))
                {
                    foreach (Item item in player.inventory)
                        if (item.type == ItemType<Items.Consumables.MP.HiEther>())
                        {
                            item.stack -= 1;
                            if (item.stack <= 0)
                                item.SetDefaults(0, false);
                            Main.PlaySound(SoundID.Item3);
                            if (Main.myPlayer == player.whoAmI)
                                CombatText.NewText(player.getRect(), Color.DodgerBlue, 75);
                            if (rechargeMP)
                                rechargeMPTimer = (int)(rechargeMPTimer * .5f);
                            else
                                currentMP += 75;
                            if (currentMP > maxMP)
                                currentMP = maxMP;
                            player.AddBuff(BuffType<EtherSickness>(), 600);
                            break;
                        }
                }
                else if (player.HasItem(ItemType<Items.Consumables.MP.Ether>()))
                {
                    foreach (Item item in player.inventory)
                        if (item.type == ItemType<Items.Consumables.MP.Ether>())
                        {
                            item.stack -= 1;
                            if (item.stack <= 0)
                                item.SetDefaults(0, false);
                            Main.PlaySound(SoundID.Item3);
                            if (Main.myPlayer == player.whoAmI)
                                CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
                            if (rechargeMP)
                                rechargeMPTimer = (int)(rechargeMPTimer * .75f);
                            else
                                currentMP += 25;
                            if (currentMP > maxMP)
                                currentMP = maxMP;
                            player.AddBuff(BuffType<EtherSickness>(), 600);
                            break;
                        }
                }
            }
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
            if (rechargeMP && rechargeMPTimer > maxRechargeMPTimer / 4 && player.HeldItem.GetGlobalItem<KeyItem>().IsKeybrand)
                damage = (int)(damage * 1.25f);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (item.GetGlobalItem<KeyItem>().IsKeybrand)
            {
                if (!target.friendly && !target.SpawnedFromStatue && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
                {
                    if (rechargeMP)
                        currentDelta += damage / 2;
                    else
                        currentDelta += (int)(damage * .75f);
                    if (player.HeldItem.type == ItemType<Items.Weapons.Developer.Chimera>())
                    {
                        if (rechargeMP)
                            currentDelta += damage / 2;
                        else
                            currentDelta += (int)(damage * .75f);
                    }
                    deltaDecayDelay = 0;
                }
                Vector2 point = itemRectangle.Center.ToVector2();
                Vector2 positionInWorld = ClosestPointInRect(target.Hitbox, point);
                if (item.type == ItemType<Items.Weapons.EdgeOfUltima>() || item.type == ItemType<Items.Weapons.Keybrand>() || item.type == ItemType<Items.Weapons.KeybrandD>() || item.type == ItemType<Items.Weapons.TrueKeybrand>() || item.type == ItemType<Items.Weapons.TrueKeybrandD>())
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

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.GetGlobalProjectile<KeyProjectile>().IsKeybrandProj && !target.friendly && !target.SpawnedFromStatue && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
            {
                if (rechargeMP)
                    currentDelta += damage / 5;
                else
                    currentDelta += damage / 3;
                deltaDecayDelay = 0;
            }
        }

        public void ItemCheck_GetMeleeHitbox(Item sItem, Rectangle heldItemFrame, out bool dontAttack, out Rectangle itemRectangle)
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
                CureBoost -= .4f;
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
                CureBoost += .25f;
                KeybrandMagic += .25f;
                KeybrandMelee -= .4f;
                KeybrandRanged -= .4f;
                RibbonEndurance -= .4f;
            }
            if (ChimeraBleed && !player.buffImmune[BuffID.Bleeding])
            {
                player.meleeDamageMult *= Main.expertMode ? .5f : .75f;
                player.rangedDamageMult *= Main.expertMode ? .5f : .75f;
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

        #region Drawcode
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int index = layers.IndexOf(PlayerLayer.HeldItem);
            if (index != -1)
            {
                layers.Insert(index + 1, WeaponGlow);
            }
            index = layers.IndexOf(PlayerLayer.Head);
            if (index != -1)
            {
                layers.Insert(index + 1, HeadGlow);
            }
            index = layers.IndexOf(PlayerLayer.Body);
            if (index != -1)
            {
                layers.Insert(index + 1, BodyGlow);
            }
            index = layers.IndexOf(PlayerLayer.Arms);
            if (index != -1)
            {
                layers.Insert(index + 1, ArmsGlow);
            }
            index = layers.IndexOf(PlayerLayer.Legs);
            if (index != -1)
            {
                layers.Insert(index + 1, LegsGlow);
            }
        }
        #region PlayerLayer stuff
        public static readonly PlayerLayer WeaponGlow = new PlayerLayer("KeybrandsPlus", "WeaponGlow", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            KeyPlayer modPlayer = drawPlayer.GetModPlayer<KeyPlayer>();

            if (drawInfo.shadow != 0f || drawPlayer.dead || drawPlayer.frozen || drawPlayer.itemAnimation <= 0)
            {
                return;
            }

            Mod mod = ModLoader.GetMod("KeybrandsPlus");

            if (drawPlayer.HeldItem.type == ItemType<Items.Weapons.Developer.Chimera>())
            {
                Texture2D weaponGlow = mod.GetTexture("Textures/Glowmasks/Chimera");
                Vector2 position = new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y));
                Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
                DrawData drawData = new DrawData(weaponGlow, position, null, new Color(255, 255, 255, 0) * 0.8f, drawPlayer.itemRotation, origin, drawPlayer.HeldItem.scale, drawInfo.spriteEffects, 0);
                Main.playerDrawData.Add(drawData);
            }

            if (drawPlayer.HeldItem.type == ItemType<Items.Weapons.FlameLiberator>() && !modPlayer.HideGlowmask)
            {
                Texture2D weaponGlow = mod.GetTexture("Textures/Glowmasks/FlameLiberator");
                Vector2 position = new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y));
                Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
                DrawData drawData = new DrawData(weaponGlow, position, null, new Color(255, 255, 255, 0) * 0.8f, drawPlayer.itemRotation, origin, drawPlayer.HeldItem.scale, drawInfo.spriteEffects, 0);
                Main.playerDrawData.Add(drawData);
            }
        });
        public static readonly PlayerLayer HeadGlow = new PlayerLayer("KeybrandsPlus", "HeadGlow", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            KeyPlayer modPlayer = drawPlayer.GetModPlayer<KeyPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KeybrandsPlus");

            if (modPlayer.AvaliHelmet)
            {
                texture = mod.GetTexture("Textures/Glowmasks/AvaliHelmet");
            }

            if (texture == null)
            {
                return;
            }

            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.headOrigin, drawPlayer.bodyFrame, color, drawPlayer.headRotation, drawInfo.headOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        public static readonly PlayerLayer BodyGlow = new PlayerLayer("KeybrandsPlus", "BodyGlow", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            KeyPlayer modPlayer = drawPlayer.GetModPlayer<KeyPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KeybrandsPlus");

            if (modPlayer.AvaliShirt)
            {
                texture = mod.GetTexture("Textures/Glowmasks/AvaliShirt");
            }

            if (texture == null)
            {
                return;
            }

            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        public static readonly PlayerLayer ArmsGlow = new PlayerLayer("KeybrandsPlus", "ArmsGlow", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            KeyPlayer modPlayer = drawPlayer.GetModPlayer<KeyPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KeybrandsPlus");

            if (modPlayer.AvaliShirt)
            {
                texture = mod.GetTexture("Textures/Glowmasks/AvaliArms");
            }

            if (texture == null)
            {
                return;
            }

            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        public static readonly PlayerLayer LegsGlow = new PlayerLayer("KeybrandsPlus", "LegsGlow", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            KeyPlayer modPlayer = drawPlayer.GetModPlayer<KeyPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KeybrandsPlus");

            if (modPlayer.AvaliPants)
            {
                texture = mod.GetTexture("Textures/Glowmasks/AvaliPants");
            }

            if (texture == null)
            {
                return;
            }

            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.legFrame.Width / 2, drawPlayer.height - drawPlayer.legFrame.Height + 4f) + drawPlayer.legPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.legOrigin, drawPlayer.legFrame, color, drawPlayer.legRotation, drawInfo.legOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.legArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        #endregion
        #endregion
    }
}
