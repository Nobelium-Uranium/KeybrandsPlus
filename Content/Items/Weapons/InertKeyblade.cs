using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace KeybrandsPlus.Content.Items.Weapons
{
    public class InertKeyblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A keyblade forged with basic metals and a magic core\n" +
                "Lacking a keychain to draw power from, it is unwieldy as a weapon");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(40);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;

            Item.DamageType = DamageClass.Generic;
            Item.damage = 10;
            Item.knockBack = 5f;

            Item.value = Item.buyPrice(silver: 10);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("IronBar", 12)
                .AddIngredient(ItemID.ManaCrystal)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
    }
}
