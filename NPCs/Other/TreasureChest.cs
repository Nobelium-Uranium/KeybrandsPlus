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
        private bool Unlocked;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Chest (UNFINISHED)");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.Size = new Vector2(32, 30);
            npc.friendly = true;
            npc.dontTakeDamageFromHostiles = true;
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
            return item.GetGlobalItem<KeyItem>().IsKeybrand;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            crit = false;
        }

        public override bool PreAI()
        {
            if (npc.life < 1)
                npc.life = 1;
            return base.PreAI();
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            npc.life = 1;
            if (item.GetGlobalItem<KeyItem>().IsKeybrand)
            {
                npc.dontTakeDamage = true;
                Unlocked = true;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            //TODO loot flash
        }

        public override void AI()
        {
            npc.active = false;
            if (Unlocked)
                npc.localAI[0]++;
            //TODO chest open logic
        }
    }
}
