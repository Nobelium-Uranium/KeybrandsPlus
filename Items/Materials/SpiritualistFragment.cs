using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Materials
{
    class SpiritualistFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiritualist Essence");
            Tooltip.SetDefault("A fragment of promising potential\nFilled with the power to link hearts");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(20);
            item.rare = ItemRarityID.Pink;
            item.maxStack = 999;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.DarkOrange.ToVector3() * 0.35f * Main.essScale);
        }
    }
}
