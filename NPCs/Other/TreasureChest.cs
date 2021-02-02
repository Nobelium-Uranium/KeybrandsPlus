using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        #endregion

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
            npc.takenDamageMultiplier = 0;
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
            if (npc.life < 1)
                npc.life = 1;
            return base.PreAI();
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            //TODO loot flash
        }

        public override void AI()
        {
            npc.active = false;
            npc.target = npc.FindClosestPlayer();
            Player player = Main.player[npc.target];
            if (Unlocked)
                npc.localAI[0]++;
            else if (player.Hitbox.Intersects(npc.Hitbox))
                Unlocked = true;
            if (npc.localAI[0] == 1)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ChestOpen").WithVolume(0.8f));
                
            }
            //TODO chest open logic
        }
    }
}
