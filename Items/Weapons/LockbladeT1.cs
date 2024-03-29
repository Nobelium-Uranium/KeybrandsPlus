﻿using KeybrandsPlus.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Weapons
{
    class LockbladeT1 : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dull Lockblade");
            Tooltip.SetDefault("No Alt Attack\nNo Abilities"); 
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
            item.GetGlobalItem<KeyItem>().NoWarning = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<WoodenKeybrand>());
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.CopperBar, 15);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    class LockbladeT1Alt : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dull Lockblade");
            Tooltip.SetDefault("No Alt Attack\nNo Abilities");
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
            item.GetGlobalItem<KeyItem>().NoWarning = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<WoodenKeybrand>());
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.TinBar, 15);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
