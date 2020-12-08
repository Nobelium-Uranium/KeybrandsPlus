using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Materials
{
    class GuardianFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian Essence");
            Tooltip.SetDefault("A fragment to repel all\nFilled with the kindness to aid friends");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.rare = ItemRarityID.Pink;
            item.maxStack = 999;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Chartreuse.ToVector3() * 0.35f * Main.essScale);
        }
    }
}
