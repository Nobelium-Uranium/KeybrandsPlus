using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class KeyNPC : GlobalNPC
    {
        public override bool InstancePerEntity { get { return true; } }

        private bool Init;

        public bool ChimeraBleed;
        private int SuperbleedTimer;
        public bool DragonRot;
        public bool DragonAura;
        public bool EternalBlaze;
        public bool EternalBlazeImmune;
        public int BlazeStack;
        public bool PhysImmune;
        public float PhysResist;
        public bool MagicImmune;
        public float MagicResist;
        public bool ChimeraImmune;
        public bool OnlyKeybrand;
        public bool LockedOn;

        #region Debuff Immunities
        public int[] EternalBlazeImmuneList = { 35, 36, 39, 40, 41, 59, 60, 62, 66, 68, 113, 114, 151, 156, 245, 246, 247, 248, 249, 277, 278, 279, 280, 285, 286, 412, 413, 414, 415, 416, 417, 418, 419, 517, 518 };
        #endregion  

        public bool Fire;
        public bool Blizzard;
        public bool Thunder;
        public bool Aero;
        public bool Water;
        public bool Light;
        public bool Dark;
        public bool Nil;
        public int[] FireTypes = { 23, 24, 25, 59, 60, 72, 151, 277, 278, 279, 280, 285, 286 };
        public int[] BlizzardTypes = { 143, 144, 145, 147, 150, 154, 155, 161, 167, 169, 171, 184, 185, 197, 206, 243, 343, 344, 345, 352 };
        public int[] ThunderTypes = { 250, 387, 388, 389, 392, 393, 394, 395, 467, 482, 483, 520 };
        public int[] AeroTypes = { 48, 205, 222, 370, 477, 479, 491, 541, 546, 551 };
        public int[] WaterTypes = { 32, 33, 58, 63, 64, 65, 67, 103, 157, 223, 224, 225, 371, 372, 373, 461 };
        public int[] LightTypes = { 75, 80, 84, 120, 137, 138, 171, 172, 244, 454, 455, 456, 457, 458, 459, 522, 527, 528 };
        public int[] DarkTypes = { 6, 7, 8, 9, 13, 14, 15, 29, 30, 34, 35, 36, 47, 57, 62, 66, 79, 81, 82, 83, 85, 94, 98, 99, 100, 101, 112, 113, 114, 115, 116, 117, 118, 119, 121, 140, 156, 158, 159, 168, 170, 173, 174, 179, 180, 181, 182, 183, 196, 239, 240, 241, 242, 253, 266, 267, 268, 281, 282, 283, 284, 288, 289, 305, 306,307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 325, 326, 327, 328, 329, 330, 464, 465, 470, 371, 472, 473, 474, 523, 525, 526, 529, 533, 534 };
        public int[] NilTypes = { };

        public float FireResist;
        public float BlizzardResist;
        public float ThunderResist;
        public float AeroResist;
        public float WaterResist;
        public float LightResist;
        public float DarkResist;
        public float NilResist;

        private float OldPhysRes;
        private float OldMagicRes;
        private float OldFireRes;
        private float OldBlizzardRes;
        private float OldThunderRes;
        private float OldAeroRes;
        private float OldWaterRes;
        private float OldLightRes;
        private float OldDarkRes;
        private float OldNilResist;
        
        
        public override void SetDefaults(NPC npc)
        {
            Init = false;
            if (npc.modNPC is Heartless)
                OnlyKeybrand = true;
            foreach (int i in EternalBlazeImmuneList)
                if (npc.type == i)
                    EternalBlazeImmune = true;
            foreach (int i in FireTypes)
                if (npc.type == i)
                    Fire = true;
            foreach (int i in BlizzardTypes)
                if (npc.type == i)
                    Blizzard = true;
            foreach (int i in ThunderTypes)
                if (npc.type == i)
                    Thunder = true;
            foreach (int i in AeroTypes)
                if (npc.type == i)
                    Aero = true;
            foreach (int i in WaterTypes)
                if (npc.type == i)
                    Water = true;
            foreach (int i in LightTypes)
                if (npc.type == i)
                    Light = true;
            foreach (int i in DarkTypes)
                if (npc.type == i)
                    Dark = true;
            foreach (int i in NilTypes)
                if (npc.type == i)
                    Nil = true;
            BlazeStack = 1;
        }
        public override void ResetEffects(NPC npc)
        {
            PhysResist = OldPhysRes;
            MagicResist = OldMagicRes;
            FireResist = OldFireRes;
            BlizzardResist = OldBlizzardRes;
            ThunderResist = OldThunderRes;
            AeroResist = OldAeroRes;
            WaterResist = OldWaterRes;
            LightResist = OldLightRes;
            DarkResist = OldDarkRes;
            NilResist = OldNilResist;
            ChimeraBleed = false;
            DragonRot = false;
            DragonAura = false;
            EternalBlaze = false;
            LockedOn = false;
        }
        public override void AI(NPC npc)
        {
            if (!Init)
            {
                Init = true;
                OldPhysRes = PhysResist;
                OldMagicRes = MagicResist;
                OldFireRes = FireResist;
                OldBlizzardRes = BlizzardResist;
                OldThunderRes = ThunderResist;
                OldAeroRes = AeroResist;
                OldWaterRes = WaterResist;
                OldLightRes = LightResist;
                OldDarkRes = DarkResist;
                OldNilResist = NilResist;
            }
            if (npc.wet || EternalBlazeImmune)
                npc.buffImmune[BuffType<Buffs.EternalBlaze>()] = true;
            else
                npc.buffImmune[BuffType<Buffs.EternalBlaze>()] = false;
            if (ChimeraBleed)
            {
                if (!npc.dontTakeDamage)
                    SuperbleedTimer += 2;
                else
                    SuperbleedTimer++;
                if (SuperbleedTimer >= 30)
                {
                    SuperbleedTimer = 0;
                    int Damage = Main.DamageVar(75);
                    if (KeybrandsPlus.SoALoaded)
                        Damage = Main.DamageVar(300);
                    npc.StrikeNPC(Damage, 0, 0);
                }
            }
            else
                SuperbleedTimer = 15;
            if (EternalBlaze)
            {
                if (BlazeStack > 10)
                    BlazeStack = 10;
                else if (BlazeStack < 1)
                    BlazeStack = 1;
            }
            else
            {
                BlazeStack = 1;
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (EternalBlaze)
            {
                npc.lifeRegen -= 9 * BlazeStack;
                if (damage < 3 * BlazeStack)
                    damage = 3 * BlazeStack;
            }
            if (DragonRot)
            {
                if (KeybrandsPlus.SoALoaded)
                    npc.lifeRegen -= 900;
                else
                    npc.lifeRegen -= 300;
                if (KeybrandsPlus.SoALoaded ? damage < 100 : damage < 50)
                {
                    if (KeybrandsPlus.SoALoaded)
                        damage = 150;
                    else
                        damage = 50;
                }
            }
            else if (DragonAura)
            {
                if (KeybrandsPlus.SoALoaded)
                    npc.lifeRegen -= 450;
                else
                    npc.lifeRegen -= 150;
                if (KeybrandsPlus.SoALoaded ? damage < 75 : damage < 25)
                {
                    if (KeybrandsPlus.SoALoaded)
                        damage = 75;
                    else
                        damage = 25;
                }
            }
        }
        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            if ((DragonRot || DragonAura) && damage >= npc.life && !npc.immortal && npc.type != NPCID.TargetDummy)
            {
                for (int i = 0; i < Main.rand.Next(DragonAura ? 1 : 3, DragonAura ? 4 : 6); i++)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 5f).RotatedByRandom(MathHelper.ToRadians(360)), ProjectileType<Projectiles.DraconicFireball>(), 50, 0, npc.lastInteraction);
                }
            }
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if ((!item.GetGlobalItem<KeyItem>().Nil || NilResist >= 1f) && 
                (((PhysImmune || PhysResist >= 1f) && (item.melee || item.ranged)) || 
                ((MagicImmune || MagicResist >= 1f) && (item.magic || item.summon))))
                return false;
            if ((item.GetGlobalItem<KeyItem>().Fire && FireResist >= 1f) || 
                (item.GetGlobalItem<KeyItem>().Blizzard && BlizzardResist >= 1f) || 
                (item.GetGlobalItem<KeyItem>().Thunder && ThunderResist >= 1f) || 
                (item.GetGlobalItem<KeyItem>().Aero && AeroResist >= 1f) || 
                (item.GetGlobalItem<KeyItem>().Water && WaterResist >= 1f) || 
                (item.GetGlobalItem<KeyItem>().Light && LightResist >= 1f) || 
                (item.GetGlobalItem<KeyItem>().Dark && DarkResist >= 1f))
                return false;
            return base.CanBeHitByItem(npc, player, item);
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (projectile.type == ProjectileType<Projectiles.ChimeraBite>() && ChimeraImmune)
                return false;
            if (((PhysImmune || PhysResist >= 1f) && (projectile.melee || projectile.ranged)) || 
                ((MagicImmune || MagicResist >= 1f) && (projectile.magic || projectile.minion || projectile.sentry) && 
                projectile.type != ProjectileType<Projectiles.ChimeraBite>()))
                return false;
            if ((projectile.GetGlobalProjectile<KeyProjectile>().Fire && FireResist >= 1f) || 
                (projectile.GetGlobalProjectile<KeyProjectile>().Blizzard && BlizzardResist >= 1f) || 
                (projectile.GetGlobalProjectile<KeyProjectile>().Thunder && ThunderResist >= 1f) || 
                (projectile.GetGlobalProjectile<KeyProjectile>().Aero && AeroResist >= 1f) || 
                (projectile.GetGlobalProjectile<KeyProjectile>().Water && WaterResist >= 1f) ||
                (projectile.GetGlobalProjectile<KeyProjectile>().Light && LightResist >= 1f) ||
                (projectile.GetGlobalProjectile<KeyProjectile>().Dark && DarkResist >= 1f))
                return false;
            return base.CanBeHitByProjectile(npc, projectile);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (Fire)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistFire);
            if (Blizzard)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistBlizzard);
            if (Thunder)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistThunder);
            if (Aero)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistAero);
            if (Water)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistWater);
            if (Light)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistLight);
            if (Dark)
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistDark);
            if (Nil)
            {
                damage = (int)(damage + target.GetModPlayer<KeyPlayer>().PlayerDefense * (1 + target.endurance));
                damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistNil);
            }
            else if (target.GetModPlayer<KeyPlayer>().DamageControlPlus && target.statLife <= target.statLifeMax2 / 2)
                damage /= 2;
            else if (target.GetModPlayer<KeyPlayer>().DamageControl && target.statLife <= target.statLifeMax2 / 5)
                damage /= 2;
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (ChimeraBleed)
            {
                int Blood = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
                Main.dust[Blood].position -= new Vector2(4);
                Main.dust[Blood].velocity = Vector2.Zero;
            }
            if (EternalBlaze)
            {
                drawColor = Color.White;
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 235, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, Scale: 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 3.6f;
                Main.dust[dustIndex].velocity.Y -= 1f;
                Main.dust[dustIndex].velocity *= 0.5f;
                int dustIndex2 = Dust.NewDust(npc.position, npc.width, npc.height, 127, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, Scale: 3f);
                Main.dust[dustIndex2].noGravity = true;
                Main.dust[dustIndex2].velocity *= 3.6f;
                Main.dust[dustIndex2].velocity.Y -= 1f;
                Main.dust[dustIndex2].velocity *= 0.5f;
            }
            if (DragonRot)
            {
                drawColor = new Color(252, 26, 247);
                if (!Main.rand.NextBool(4))
                {
                    int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, DustType<Dusts.DraconicFlame>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].velocity *= 0.5f;
                }
            }
            else if (DragonAura)
            {
                drawColor = new Color(144, 15, 141);
            }
        }
        public override void NPCLoot(NPC npc)
        {
            if (Main.hardMode && Main.rand.NextBool(3))
            {
                if (npc.type == NPCID.GoblinWarrior || npc.type == NPCID.ArmoredSkeleton || npc.type == NPCID.CursedHammer || npc.type == NPCID.EnchantedSword || npc.type == NPCID.CrimsonAxe || npc.type == NPCID.Werewolf || npc.type == NPCID.PossessedArmor || npc.type == NPCID.UndeadViking || npc.type == NPCID.ArmoredViking || npc.type == NPCID.PirateCorsair || npc.type == NPCID.RustyArmoredBonesAxe || npc.type == NPCID.RustyArmoredBonesFlail || npc.type == NPCID.RustyArmoredBonesSword || npc.type == NPCID.RustyArmoredBonesSwordNoArmor || npc.type == NPCID.BlueArmoredBones || npc.type == NPCID.BlueArmoredBonesMace || npc.type == NPCID.BlueArmoredBonesNoPants || npc.type == NPCID.BlueArmoredBonesSword || npc.type == NPCID.HellArmoredBones || npc.type == NPCID.HellArmoredBonesMace || npc.type == NPCID.HellArmoredBonesSpikeShield || npc.type == NPCID.HellArmoredBonesSword || npc.type == NPCID.GigaZapper || npc.type == NPCID.SolarDrakomire || npc.type == NPCID.SolarDrakomireRider || npc.type == NPCID.SolarSpearman || npc.type == NPCID.SolarSroller || npc.type == NPCID.SolarCorite || npc.type == NPCID.SolarSolenian || npc.type == NPCID.GreekSkeleton || npc.type == NPCID.DesertBeast)
                    Item.NewItem(npc.getRect(), ItemType<Items.Materials.WarriorFragment>(), Main.rand.Next(1, 4));
                else if (npc.type == NPCID.GoblinArcher || npc.type == NPCID.Harpy || npc.type == NPCID.Mimic || npc.type == NPCID.WyvernHead || npc.type == NPCID.SkeletonArcher || npc.type == NPCID.GiantTortoise || npc.type == NPCID.IceTortoise || npc.type == NPCID.PirateDeadeye || npc.type == NPCID.PirateCrossbower || npc.type == NPCID.BoneLee || npc.type == NPCID.Paladin || npc.type == NPCID.SkeletonSniper || npc.type == NPCID.TacticalSkeleton || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.MartianOfficer || npc.type == NPCID.VortexRifleman || npc.type == NPCID.VortexHornetQueen || npc.type == NPCID.VortexSoldier || npc.type == NPCID.GraniteGolem || npc.type == NPCID.DesertGhoul || npc.type == NPCID.DesertGhoulCorruption || npc.type == NPCID.DesertGhoulCrimson || npc.type == NPCID.DesertGhoulHallow)
                    Item.NewItem(npc.getRect(), ItemType<Items.Materials.GuardianFragment>(), Main.rand.Next(1, 4));
                else if (npc.type == NPCID.GoblinSorcerer || npc.type == NPCID.FireImp || npc.type == NPCID.DarkCaster || npc.type == NPCID.Tim || npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon || npc.type == NPCID.RedDevil || npc.type == NPCID.Pixie || npc.type == NPCID.RuneWizard || npc.type == NPCID.RaggedCaster || npc.type == NPCID.RaggedCasterOpenCoat || npc.type == NPCID.Necromancer || npc.type == NPCID.NecromancerArmored || npc.type == NPCID.DiabolistRed || npc.type == NPCID.DiabolistWhite || npc.type == NPCID.MartianEngineer || npc.type == NPCID.NebulaBrain || npc.type == NPCID.NebulaHeadcrab || npc.type == NPCID.NebulaBeast || npc.type == NPCID.NebulaSoldier || npc.type == NPCID.Medusa || npc.type == NPCID.DesertDjinn)
                    Item.NewItem(npc.getRect(), ItemType<Items.Materials.MysticFragment>(), Main.rand.Next(1, 4));
            }
            if ((npc.type == NPCID.Mothron && Main.rand.NextBool(3)) || ((npc.type == NPCID.Eyezor || npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing || npc.type == NPCID.Vampire || npc.type == NPCID.VampireBat || npc.type == NPCID.CreatureFromTheDeep || npc.type == NPCID.Fritz || npc.type == NPCID.Reaper || npc.type == NPCID.ThePossessed || npc.type == NPCID.Butcher || npc.type == NPCID.DeadlySphere || npc.type == NPCID.DrManFly || npc.type == NPCID.Nailhead || npc.type == NPCID.Psycho) && KeyUtils.RandPercent(.025f)))
                Item.NewItem(npc.getRect(), ItemType<Items.Materials.BrokenHeroKeybrand>());
            if (npc.lastInteraction != 255 && !npc.boss && !npc.friendly && !npc.SpawnedFromStatue && npc.lifeMax >= 50 && npc.damage != 0 && npc.type != NPCID.TargetDummy && npc.type != NPCID.Creeper && npc.type != NPCID.EaterofWorldsHead && npc.type != NPCID.EaterofWorldsBody && npc.type != NPCID.EaterofWorldsTail)
                if (NPC.downedMoonlord)
                {
                    if (Main.rand.NextBool())
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>(), Main.rand.Next(1, 16));
                    else if (Main.rand.NextBool())
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>());
                    if (Main.rand.NextBool(8))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(10, 76));
                    else if (Main.rand.NextBool(3))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(5, 11));
                }
                else if (NPC.downedPlantBoss)
                {
                    if (Main.rand.NextBool(4))
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>(), Main.rand.Next(1, 11));
                    else if (Main.rand.NextBool())
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>());
                    if (Main.rand.NextBool(7))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(5, 51));
                    else if (Main.rand.NextBool(3))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(3, 6));
                }
                else if (Main.hardMode)
                {
                    if (Main.rand.NextBool(6))
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>(), Main.rand.Next(1, 8));
                    else if (Main.rand.NextBool())
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>());
                    if (Main.rand.NextBool(6))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(3, 26));
                    else if (Main.rand.NextBool(3))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(1, 4));
                }
                else
                {
                    if (Main.rand.NextBool(8))
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>(), Main.rand.Next(1, 6));
                    else if (Main.rand.NextBool())
                        Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>());
                    if (Main.rand.NextBool(5))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), Main.rand.Next(1, 11));
                    else if (Main.rand.NextBool(3))
                        Item.NewItem(npc.getRect(), ItemType<Items.Other.MPOrb>(), 1);
                }
            if (npc.type == NPCID.Plantera && npc.lastInteraction != 255 && Main.player[npc.lastInteraction].active && (Main.player[npc.lastInteraction].GetModPlayer<KeyPlayer>().StoredUUIDX % 7 == 0 || Main.player[npc.lastInteraction].GetModPlayer<KeyPlayer>().StoredUUIDY % 5 == 0 || Main.player[npc.lastInteraction].GetModPlayer<KeyPlayer>().StoredUUIDZ % 4 == 0) && Main.rand.NextBool(754))
                Item.NewItem(npc.getRect(), ItemType<Items.Accessories.Wings.BlossomWings>());
        }
    }
}
