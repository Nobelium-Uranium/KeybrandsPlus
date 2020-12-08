using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Materials
{
    class WarriorFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warrior Essence");
            Tooltip.SetDefault("A fragment of terrible destruction\nFilled with invincible courage");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 24;
            item.rare = ItemRarityID.Pink;
            item.maxStack = 999;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Crimson.ToVector3() * 0.35f * Main.essScale);
        }
    }
}
