using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Other.Cure
{
    class CureSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spellbook - Lesser Cure");
            Tooltip.SetDefault("MP Cost: All\n" +
                "Creates a healing field at the cursor's position\n" +
                "The range and heal rate depends on your Cure efficiency\n" +
                "40% less efficient than Cure");
        }
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 18;
            item.rare = ItemRarityID.LightRed;
            item.useTime = 90;
            item.useAnimation = 90;
            item.shoot = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item29;
        }
        public override bool CanUseItem(Player player)
        {
            if (!player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP = 0;
            return !player.GetModPlayer<KeyPlayer>().rechargeMP;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<Projectiles.CureField>(), 0, 0, player.whoAmI, 0, 1);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.Book);
            r.AddIngredient(ItemID.GoldenKey);
            r.AddIngredient(ItemID.LifeCrystal, 10);
            r.AddRecipeGroup("K+:Gem", 25);
            r.AddTile(TileID.Bookcases);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    class CuraSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spellbook - Lesser Cura");
            Tooltip.SetDefault("MP Cost: All\n" +
                "Creates a healing field at the cursor's position\n" +
                "The range and heal rate depends on your Cure efficiency\n" +
                "40% less efficient than Cura");
        }
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 18;
            item.rare = ItemRarityID.Yellow;
            item.useTime = 90;
            item.useAnimation = 90;
            item.shoot = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item29;
        }
        public override bool CanUseItem(Player player)
        {
            if (!player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP = 0;
            return !player.GetModPlayer<KeyPlayer>().rechargeMP;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<Projectiles.CureField>(), 0, 0, player.whoAmI, 1, 1);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<CureSpell>());
            r.AddIngredient(ItemID.SpellTome);
            r.AddIngredient(ItemID.LifeCrystal, 15);
            r.AddIngredient(ItemID.SoulofFright, 5);
            r.AddIngredient(ItemID.SoulofMight, 5);
            r.AddIngredient(ItemID.SoulofSight, 5);
            r.AddTile(TileID.CrystalBall);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    class CuragaSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spellbook - Lesser Curaga");
            Tooltip.SetDefault("MP Cost: All\n" +
                "Creates a healing field at the cursor's position\n" +
                "The range and heal rate depends on your Cure efficiency\n" +
                "40% less efficient than Curaga");
        }
        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 18;
            item.rare = ItemRarityID.Red;
            item.useTime = 90;
            item.useAnimation = 90;
            item.shoot = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item29;
        }
        public override bool CanUseItem(Player player)
        {
            if (!player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP = 0;
            return !player.GetModPlayer<KeyPlayer>().rechargeMP;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<Projectiles.CureField>(), 0, 0, player.whoAmI, 2, 1);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<CuraSpell>());
            r.AddIngredient(ModContent.ItemType<Synthesis.Other.Zenithite>(), 5);
            r.AddIngredient(ItemID.LifeCrystal, 25);
            r.AddIngredient(ItemID.LifeFruit, 10);
            r.AddIngredient(ItemID.LunarBar, 30);
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
