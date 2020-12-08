using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Materials
{
    class ZenithFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A fragment of ultimate power\nFilled with hidden potential");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.MediumSpringGreen.ToVector3() * 0.65f * Main.essScale);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.FragmentSolar);
            r.AddIngredient(ItemID.FragmentVortex);
            r.AddIngredient(ItemID.FragmentNebula);
            r.AddIngredient(ModContent.ItemType<WarriorFragment>());
            r.AddIngredient(ModContent.ItemType<GuardianFragment>());
            r.AddIngredient(ModContent.ItemType<MysticFragment>());
            r.AddIngredient(ModContent.ItemType<Synthesis.Other.Zenithite>());
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this, 3);
            r.AddRecipe();
        }
    }
}
