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
using Terraria.GameContent.Events;
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

        public override bool PreAI()
        {
            if (NPC.life < NPC.lifeMax)
                NPC.life += 25;
            if (NPC.life > NPC.lifeMax)
                NPC.life = NPC.lifeMax;
            return base.PreAI();
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
            bool women = false;

            int guide = NPC.FindFirstNPC(NPCID.Guide);
            int nurse = NPC.FindFirstNPC(NPCID.Nurse);
            int zoologist = NPC.FindFirstNPC(NPCID.BestiaryGirl);
            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            int stylist = NPC.FindFirstNPC(NPCID.Stylist);
            int mechanic = NPC.FindFirstNPC(NPCID.Mechanic);
            int partygirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            int steampunker = NPC.FindFirstNPC(NPCID.Steampunker);
            int jerk = NPC.FindFirstNPC(NPCID.Angler);

            if (nurse != -1 || zoologist != -1 || dryad != -1 || stylist != -1 || mechanic != -1 || partygirl != -1 || steampunker != -1)
                women = true;

            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add("May your heart be your guiding key.");
            chat.Add($"My name is {NPC.GivenName}, and I am here to provide tips regarding the looming Heartless threat.");
            chat.Add("If there is anything you would like to know, I am willing to oblige. My database is full of useful, factual information.");
            if ((Main.dayTime && BirthdayParty.PartyIsUp) || (System.DateTime.Now.Month == 9 && System.DateTime.Now.Day == 10) || (System.DateTime.Now.Month == 1 && System.DateTime.Now.Day == 31) || (System.DateTime.Now.Month == 4 && System.DateTime.Now.Day == 12))
                chat.Add("Good tidings, my friend. Today is a momentous day.", .5f);
            if (Main.dayTime)
            {
            }
            else
            {
                if (Main.bloodMoon)
                {
                    if (women)
                        chat.Add("Some of the people here seem to be agitated, there had better not be some sort of psychic droner out there.");
                    chat.Add("How strange, the Heartless seem to dislike the crimson shade of the moon. Ironic considering the nature of these abhorrent flesh beasts.");
                }
                if (guide != -1)
                    chat.Add($"That {Main.npc[guide].GivenName}, he is quite the anomaly. His heart is full of darkness, and yet he is a true ally...");
            }
            if (jerk != -1)
                chat.Add($"{Main.npc[jerk].GivenName} keeps trying to 'prank' me by dousing me in water. His persistence is admirable, but my armor and circuitry are completely waterproofed.");
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.51");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                if (Main.hardMode)
                    chat.Add("Munny can be obtained by slaying foes who drop a sizeable amount of coins. Consider hunting down more valuable creatures to build up your wealth. I would recommend hunting for Mimics underground.", .5f);
                chat.Add("Munny can be obtained by slaying foes who drop a sizeable amount of coins. Consider hunting down more valuable creatures to build up your wealth. I would recommend rematching easy bosses.", Main.hardMode ? .5f : 1f);
                chat.Add("If you have a pouch to store your Munny in, you can save your inventory space for more important items. Picking up Munny will automatically be placed in an available pouch.");
                chat.Add("You can quickly deposit into a Munny Pouch by left-clicking on one while holding a stack of Munny on your cursor. It is far more convenient than dropping it on the ground first.");
                chat.Add("Having any item or a full stack of Munny on your cursor when withdrawing from a pouch will place the remainder Munny directly into your inventory, if you have the room for it.");

                Main.npcChatText = chat;
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
                damage = 250;
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
        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot($"{nameof(KeybrandsPlus)}/Content/NPCs/TownNPC/KeybladeMaster_Head");
    }
}
