using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace KeybrandsPlus.Content.NPCs.TownNPC
{
    [AutoloadHead]
    public class KeybladeMaster : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Keyblade Master");
            Main.npcFrameCount[Type] = 23;
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 80;
            NPCID.Sets.AttackType[Type] = 3;
            NPCID.Sets.AttackTime[Type] = 10;
            NPCID.Sets.AttackAverageChance[Type] = 10;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Cyborg, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
                ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.lifeMax = 2500;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.knockBackResist = .1f;

            AnimationType = NPCID.Stylist;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            ContentSamples.NpcBestiaryRarityStars[Type] = 5;
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("I don't know what to put here lmao")
            });
        }

        public override void AI()
        {
            if (NPC.life < NPC.lifeMax)
                NPC.life += 25;
            if (NPC.life > NPC.lifeMax)
                NPC.life = NPC.lifeMax;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            SoundEngine.PlaySound(SoundID.NPCHit4, NPC.Center);
            if (damage < 500)
                damage = 0;
            else
                damage -= 500;
            if (damage > 500)
            {
                damage -= 500;
                damage /= 10;
                damage += 500;
            }
            if (damage > 1000)
                damage = 1000;
            crit = false;
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 2.5f * (float)hitDirection, -2.5f);

                int headGore = Mod.Find<ModGore>("MasterHead").Type;
                int bodyGore = Mod.Find<ModGore>("MasterBody").Type;
                int armGore = Mod.Find<ModGore>("MasterArm").Type;
                int legGore = Mod.Find<ModGore>("MasterLeg").Type;

                var entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), headGore);
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), bodyGore);
                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), armGore);
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), legGore);
                }
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return false;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new KeybladeMasterProfile();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Araxlaez"
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add("May your heart be your guiding key.");

            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.51");
            button2 = Language.GetTextValue("LegacyInterface.64");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                chat.Add("Munny is power!");
                chat.Add("Woaw.");

                Main.npcChatText = chat;
            }
            else
            {
                Main.npcChatText = "This isn't implemented yet. I'm not sure what you were expecting, to be honest.";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 50;
            knockback = 3f;
            if (Main.hardMode)
                damage = 75;
            if (NPC.downedPlantBoss)
                damage = 100;
            if (NPC.downedMoonlord)
                damage = 500;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 7;
            randExtraCooldown = 7;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = TextureAssets.Item[ItemID.Keybrand].Value;
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = new Item(ItemID.Keybrand).width;
            itemHeight = new Item(ItemID.Keybrand).height;
        }
    }

    public class KeybladeMasterProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();
        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc) => ModContent.Request<Texture2D>($"{nameof(KeybrandsPlus)}/Content/NPCs/TownNPC/KeybladeMaster");
        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot($"{nameof(KeybrandsPlus)}/Content/NPCs/TownNPC/KeybladeMaster");
    }
}
