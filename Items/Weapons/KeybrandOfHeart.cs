using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using Terraria;

namespace KeybrandsPlus.Items.Weapons
{
    class KeybrandOfHeart : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Keybrand of Heart");
            Tooltip.SetDefault("+10 Dark Alignment\n" +
                "No Alt Attack\n" +
                "No Abilities\n" +
                "'Lead me into everlasting darkness!'");
        }

        public override void SetDefaults()
        {
            item.damage = 38;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.1f;
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3.2f;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.GetGlobalItem<KeyItem>().Dark = true;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().DarkAlignment += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddRecipeGroup("K+:EvilBar", 18);
            r.AddRecipeGroup("K+:EvilSample", 12);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
