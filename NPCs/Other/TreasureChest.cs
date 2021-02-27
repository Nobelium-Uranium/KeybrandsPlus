using KeybrandsPlus.Globals;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.NPCs.Other
{
    public class TreasureChest : ModNPC
    {
        #region Treasure Pools
        private int[] ShardPool = {
            ItemType<Items.Synthesis.Brave.BraveShard>(),
            ItemType<Items.Synthesis.Writhing.WrithingShard>(),
            ItemType<Items.Synthesis.Sinister.SinisterShard>(),
            ItemType<Items.Synthesis.Lucid.LucidShard>(),
            ItemType<Items.Synthesis.Pulsing.PulsingShard>(),
            ItemType<Items.Synthesis.Blazing.BlazingShard>(),
            ItemType<Items.Synthesis.Frost.FrostShard>(),
            ItemType<Items.Synthesis.Lightning.LightningShard>(),
            ItemType<Items.Synthesis.Twilight.TwilightShard>(),
            ItemType<Items.Synthesis.Betwixt.BetwixtShard>(),
            ItemType<Items.Synthesis.Hungry.HungryShard>(),
            ItemType<Items.Synthesis.Soothing.SoothingShard>(),
            ItemType<Items.Synthesis.Wellspring.WellspringShard>()
        };
        private int[] StonePool = {
            ItemType<Items.Synthesis.Brave.BraveStone>(),
            ItemType<Items.Synthesis.Writhing.WrithingStone>(),
            ItemType<Items.Synthesis.Sinister.SinisterStone>(),
            ItemType<Items.Synthesis.Lucid.LucidStone>(),
            ItemType<Items.Synthesis.Pulsing.PulsingStone>(),
            ItemType<Items.Synthesis.Blazing.BlazingStone>(),
            ItemType<Items.Synthesis.Frost.FrostStone>(),
            ItemType<Items.Synthesis.Lightning.LightningStone>(),
            ItemType<Items.Synthesis.Twilight.TwilightStone>(),
            ItemType<Items.Synthesis.Betwixt.BetwixtStone>(),
            ItemType<Items.Synthesis.Hungry.HungryStone>(),
            ItemType<Items.Synthesis.Soothing.SoothingStone>(),
            ItemType<Items.Synthesis.Wellspring.WellspringStone>()
        };
        private int[] GemPool = {
            ItemType<Items.Synthesis.Brave.BraveGem>(),
            ItemType<Items.Synthesis.Writhing.WrithingGem>(),
            ItemType<Items.Synthesis.Sinister.SinisterGem>(),
            ItemType<Items.Synthesis.Lucid.LucidGem>(),
            ItemType<Items.Synthesis.Pulsing.PulsingGem>(),
            ItemType<Items.Synthesis.Blazing.BlazingGem>(),
            ItemType<Items.Synthesis.Frost.FrostGem>(),
            ItemType<Items.Synthesis.Lightning.LightningGem>(),
            ItemType<Items.Synthesis.Twilight.TwilightGem>(),
            ItemType<Items.Synthesis.Betwixt.BetwixtGem>(),
            ItemType<Items.Synthesis.Hungry.HungryGem>(),
            ItemType<Items.Synthesis.Soothing.SoothingGem>(),
            ItemType<Items.Synthesis.Wellspring.WellspringGem>()
        };
        private int[] CrystalPool = {
            ItemType<Items.Synthesis.Brave.BraveCrystal>(),
            ItemType<Items.Synthesis.Writhing.WrithingCrystal>(),
            ItemType<Items.Synthesis.Sinister.SinisterCrystal>(),
            ItemType<Items.Synthesis.Lucid.LucidCrystal>(),
            ItemType<Items.Synthesis.Pulsing.PulsingCrystal>(),
            ItemType<Items.Synthesis.Blazing.BlazingCrystal>(),
            ItemType<Items.Synthesis.Frost.FrostCrystal>(),
            ItemType<Items.Synthesis.Lightning.LightningCrystal>(),
            ItemType<Items.Synthesis.Twilight.TwilightCrystal>(),
            ItemType<Items.Synthesis.Betwixt.BetwixtCrystal>(),
            ItemType<Items.Synthesis.Hungry.HungryCrystal>(),
            ItemType<Items.Synthesis.Soothing.SoothingCrystal>(),
            ItemType<Items.Synthesis.Wellspring.WellspringCrystal>()
        };
        private int MaxMunny;
        #endregion

        private bool SafeToUnlock;
        private bool Unlocked;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Chest");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.Size = new Vector2(32, 30);
            npc.friendly = true;
            npc.dontTakeDamage = true;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.knockBackResist = 0f;
            npc.rarity = 1;
            npc.behindTiles = true;
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
        
        public override bool PreAI()
        {
            if (npc.alpha >= 255)
                npc.active = false;
            return base.PreAI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneDirtLayerHeight)
            {
                return SpawnCondition.Underground.Chance * 0.025f;
            }
            else if (spawnInfo.player.ZoneRockLayerHeight)
            {
                return SpawnCondition.Cavern.Chance * 0.05f;
            }
            return 0;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            //TODO loot flash
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.localAI[0] >= 10)
                npc.frame.Y = 2 * frameHeight;
            else if (npc.localAI[0] >= 5)
                npc.frame.Y = 1 * frameHeight;
            else
                npc.frame.Y = 0;
        }

        public override void AI()
        {
            //npc.active = false;
            SafeToUnlock = true;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    if (Main.npc[k].Distance(npc.Center) < 300f)
                        SafeToUnlock = false;
                }
            }
            for (int k = 0; k < 200; k++)
            {
                Player player = Main.player[k];
                if (player.active && !player.dead)
                {
                    Rectangle itemRect = player.GetModPlayer<KeyPlayer>().itemRectangle;
                    itemRect.Inflate(itemRect.Width / 2, 0);
                    if (!Unlocked && SafeToUnlock && player.itemAnimation > 0 && player.HeldItem.GetGlobalItem<KeyItem>().IsKeybrand && itemRect.Intersects(npc.getRect()) && Collision.CanHit(player.GetModPlayer<KeyPlayer>().itemRectangle.Center.ToVector2(), 0, 0, npc.Center, 0, 0))
                    {
                        Unlocked = true;
                        npc.netUpdate = true;
                    }
                }
            }
            if (Unlocked)
                npc.localAI[0]++;
            if (npc.localAI[0] == 1)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ChestOpen").WithVolume(0.25f), npc.Center);
            }
            else if (npc.localAI[0] == 10)
            {
                npc.rarity = 0;
                #region Munny
                if (NPC.downedMoonlord)
                    MaxMunny = 500;
                else if (NPC.downedPlantBoss)
                    MaxMunny = 300;
                else if (Main.hardMode)
                    MaxMunny = 150;
                else
                    MaxMunny = 75;
                int DropAmount = 1;
                if (KeyUtils.RandPercent(.05f))
                {
                    DropAmount = Main.rand.Next(MaxMunny / 2, MaxMunny + 1);
                }
                else if (KeyUtils.RandPercent(.25f))
                {
                    DropAmount = Main.rand.Next(MaxMunny / 4, MaxMunny / 2 + 1);
                }
                else if (KeyUtils.RandPercent(.5f))
                {
                    DropAmount = Main.rand.Next(MaxMunny / 10, MaxMunny / 4 + 1);
                }
                else
                {
                    DropAmount = Main.rand.Next(1, MaxMunny / 10 + 1);
                }
                if (KeyUtils.RandPercent(.1f))
                {
                    if (KeyUtils.RandPercent(.05f))
                    {
                        DropAmount += Main.rand.Next(MaxMunny / 2, MaxMunny + 1);
                    }
                    else if (KeyUtils.RandPercent(.25f))
                    {
                        DropAmount += Main.rand.Next(MaxMunny / 4, MaxMunny / 2 + 1);
                    }
                    else if (KeyUtils.RandPercent(.5f))
                    {
                        DropAmount += Main.rand.Next(MaxMunny / 10, MaxMunny / 4 + 1);
                    }
                    else
                    {
                        DropAmount += Main.rand.Next(1, MaxMunny / 10 + 1);
                    }
                }
                if (DropAmount > (int)((float)MaxMunny * 1.5f))
                    DropAmount = (int)((float)MaxMunny * 1.5f);
                Item.NewItem(npc.getRect(), ItemType<Items.Currency.Munny>(), DropAmount);
                #endregion
                #region Synthesis Materials
                if (KeyUtils.RandPercent(NPC.downedMoonlord ? .1f : .01f))
                {
                    Item.NewItem(npc.getRect(), ItemType<Items.Synthesis.Other.Zenithite>());
                }
                #endregion
                #region Other Materials
                if (NPC.downedPlantBoss || (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3))
                {
                    if (KeyUtils.RandPercent(.1f))
                    {
                        Item.NewItem(npc.getRect(), ItemType<Items.Materials.BrokenHeroKeybrand>());
                    }
                    if (KeyUtils.RandPercent(.25f))
                    {
                        Item.NewItem(npc.getRect(), ItemType<Items.Materials.WarriorFragment>(), Main.rand.Next(3, 8));
                    }
                    if (KeyUtils.RandPercent(.25f))
                    {
                        Item.NewItem(npc.getRect(), ItemType<Items.Materials.GuardianFragment>(), Main.rand.Next(3, 8));
                    }
                    if (KeyUtils.RandPercent(.25f))
                    {
                        Item.NewItem(npc.getRect(), ItemType<Items.Materials.MysticFragment>(), Main.rand.Next(3, 8));
                    }
                }
                else if (KeyUtils.RandPercent(.1f))
                {
                    Item.NewItem(npc.getRect(), ItemType<Items.Materials.KeybrandMold>());
                }
                #endregion
            }
            else if (npc.localAI[0] > 30)
            {
                npc.alpha += 5;
            }
        }
    }
}
