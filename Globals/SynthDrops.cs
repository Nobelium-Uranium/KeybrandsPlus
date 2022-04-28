using KeybrandsPlus.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class SynthDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        { // npc.type == NPCID.
            bool bossAlive = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC k = Main.npc[i];
                if (k.active && k.boss)
                {
                    bossAlive = true;
                    break;
                }
            }
            if (npc.lastInteraction != 255 && !npc.boss && !npc.friendly && !npc.SpawnedFromStatue && npc.lifeMax > 5 && npc.type != NPCID.TargetDummy && npc.type != NPCID.Creeper && npc.type != NPCID.EaterofWorldsHead && npc.type != NPCID.EaterofWorldsBody && npc.type != NPCID.EaterofWorldsTail)
            {
                if (Main.hardMode && (!bossAlive || Main.rand.NextBool(10)))
                {
                    if (NPC.downedMoonlord && KeyUtils.RandPercent(.005f))
                        Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Other.Zenithite>());
                    else if (NPC.downedPlantBoss && KeyUtils.RandPercent(.0025f))
                        Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Other.Zenithite>());
                    else if (KeyUtils.RandPercent(.0001f))
                        Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Other.Zenithite>());
                }
                /*
            #region Blazing
            if (npc.type == NPCID.Hellbat || npc.type == NPCID.LavaSlime)
            {
                if (Main.hardMode && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingStone>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingShard>());
            }
            else if (npc.type == NPCID.FireImp)
            {
                if (Main.hardMode && Main.rand.NextBool(7))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingGem>());
                else if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingStone>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingShard>());
            }
            else if (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon || npc.type == NPCID.Lavabat)
            {
                if (Main.hardMode && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingGem>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingStone>());
            }
            else if (npc.type == NPCID.HellArmoredBones || npc.type == NPCID.HellArmoredBonesMace || npc.type == NPCID.HellArmoredBonesSpikeShield || npc.type == NPCID.HellArmoredBonesSword || npc.type == NPCID.DiabolistRed || npc.type == NPCID.DiabolistWhite || npc.type == NPCID.RedDevil || npc.type == NPCID.Pumpking)
            {
                if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingCrystal>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Blazing.BlazingGem>());
            }
            #endregion
            #region Frost
            if (npc.type == NPCID.ZombieEskimo || npc.type == NPCID.ArmedZombieEskimo || npc.type == NPCID.IceSlime)
            {
                if (Main.hardMode && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostStone>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostShard>());
            }
            else if (npc.type == NPCID.DarkCaster || npc.type == NPCID.SnowFlinx || npc.type == NPCID.IceBat || npc.type == NPCID.SpikedIceSlime || npc.type == NPCID.UndeadViking)
            {
                if (Main.hardMode && Main.rand.NextBool(7))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostGem>());
                else if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostStone>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostShard>());
            }
            else if (npc.type == NPCID.ArmoredViking || npc.type == NPCID.IceElemental || npc.type == NPCID.IcyMerman || npc.type == NPCID.Wolf || npc.type == NPCID.IceTortoise)
            {
                if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostGem>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostStone>());
            }
            else if (npc.type == NPCID.IceGolem || npc.type == NPCID.IceQueen)
            {
                if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostCrystal>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Frost.FrostGem>());
            }
            #endregion
            #region Lightning
            if (npc.type == NPCID.PinkJellyfish)
            {
                if (Main.hardMode && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningStone>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningShard>());
            }
            else if (npc.type == NPCID.GraniteFlyer || npc.type == NPCID.GraniteGolem)
            {
                if (Main.hardMode && Main.rand.NextBool(7))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningGem>());
                else if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningStone>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningShard>());
            }
            else if (npc.type == NPCID.AnglerFish || npc.type == NPCID.Gastropod || npc.type == NPCID.AngryNimbus)
            {
                if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningGem>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningStone>());
            }
            else if (npc.type == NPCID.GigaZapper || npc.type == NPCID.MartianWalker)
            {
                if (Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningCrystal>());
                else if (Main.rand.NextBool(3))
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Lightning.LightningGem>());
            }
            #endregion
            #region Stormy

            #endregion
            #region Brave

            #endregion
            #region Lucid

            #endregion
            #region Pulsing

            #endregion
            #region Writhing

            #endregion
            #region Sinister

            #endregion
            #region Hungry

            #endregion
            #region Soothing

            #endregion
            #region Wellspring

            #endregion
            */
            }
        }
    }
}
