﻿using KeybrandsPlus.Assets.Sounds;
using KeybrandsPlus.Content.Items.Currency;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace KeybrandsPlus.Content.NPCs.Other
{
    public class TreasureChest : ModNPC
    {
        private int MaxMunny;

        private bool Unlocked;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Chest");
            Main.npcFrameCount[Type] = 3;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.Size = new Vector2(32, 30);
            NPC.friendly = true;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.knockBackResist = 0f;
            NPC.rarity = 1;
            NPC.behindTiles = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerInTown || spawnInfo.PlayerSafe || Main.invasionType != 0 || spawnInfo.Water || spawnInfo.Sky || NPC.AnyNPCs(Type))
                return 0;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss)
                    return 0;
            }
            foreach (ModBiome modBiome in ModContent.GetContent<ModBiome>())
            {
                if (modBiome == null || modBiome.Mod == Mod)
                    continue;
                if (spawnInfo.Player.InModBiome(modBiome))
                    return 0;
            }
            if (spawnInfo.Player.ZoneDirtLayerHeight)
                return SpawnCondition.Underground.Chance * .01f;
            else if (spawnInfo.Player.ZoneRockLayerHeight)
                return SpawnCondition.Cavern.Chance * .025f;
            return 0;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] >= 10 && NPC.ai[0] < 70)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.ai[0] >= 5 && NPC.ai[0] < 75)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else
                NPC.frame.Y = 0;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs) => false;

        public override bool CanChat() => !Unlocked;

        public override string GetChat()
        {
            return "It's a treasure chest! What goodies could be inside?";
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Open";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                Main.npcChatCornerItem = 0;
                Main.npcChatText = "";
                NPC.ShowNameOnHover = false;
                Unlocked = true;
            }
        }

        public override void AI()
        {
            if (NPC.lavaWet)
                NPC.active = false;
            if (Unlocked)
                NPC.ai[0]++;
            if (NPC.ai[0] == 1)
                SoundEngine.PlaySound(KeySoundStyle.NPCChestOpen, NPC.Center);
            else if (NPC.ai[0] == 10)
            {
                NPC.rarity = 0;
                #region Munny
                if (NPC.downedMoonlord)
                    MaxMunny = 4000;
                else if (NPC.downedPlantBoss)
                    MaxMunny = 2000;
                else if (Main.hardMode)
                    MaxMunny = 1000;
                else
                    MaxMunny = 500;
                Player targetPlayer;
                if (Main.netMode == NetmodeID.SinglePlayer)
                    targetPlayer = Main.LocalPlayer;
                else
                {
                    byte target = Player.FindClosest(NPC.position, NPC.width, NPC.height);
                    targetPlayer = Main.player[target];
                }
                int tries = 1;
                if (Main.rand.NextFloat() <= targetPlayer.luck)
                    tries++;
                int DropAmount = 0;
                for (int i = 0; i < tries; i++)
                {
                    int RandAmount = Main.rand.Next(MaxMunny / 50, MaxMunny / 5 + 1) * 5;
                    if (RandAmount > DropAmount)
                        DropAmount = RandAmount;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<Munny>(), DropAmount);
                #endregion
            }
            else if (NPC.ai[0] > 90)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<Items.Placeable.Furniture.TreasureChest>());
                NPC.active = false;
            }
        }
    }
}
