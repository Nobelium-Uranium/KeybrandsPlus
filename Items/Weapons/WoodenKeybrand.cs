using KeybrandsPlus.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Weapons

{
    public class WoodenKeybrand : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("No Alt Attack\nNo Abilities\n'A practice sword based on the legendary Keybrand'");
        }
        public override void SetDefaults()
        {
            item.damage = 7;
            item.melee = true;
            item.width = 20;
            item.height = 24;
            item.scale = 0.9f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 4.5f;
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
            item.GetGlobalItem<KeyItem>().NoWarning = true;
            item.GetGlobalItem<KeyItem>().NoKeybrandMaster = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("Wood", 30);
            r.AddIngredient(ItemID.Rope, 5);
            r.AddTile(TileID.WorkBenches);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}