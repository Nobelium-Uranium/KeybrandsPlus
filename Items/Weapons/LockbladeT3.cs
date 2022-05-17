using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Weapons
{
    class LockbladeT3 : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Refined Lockblade");
            Tooltip.SetDefault($"Alt Attack: Lesser Flame [i:{ItemID.SorcererEmblem}]\n" +
                "MP Cost: 3\n" +
                "Fires a weak spark, faster than Reinforced Lockblade\n" +
                "Ability: Defender\n" +
                "'Sturdy and reliable'");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.15f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
            item.GetGlobalItem<KeyItem>().NoWarning = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.melee = true;
                item.magic = false;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 20;
                item.knockBack = 6;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 18;
                item.knockBack = 1f;
                item.shoot = ProjectileID.Spark;
                item.noMelee = true;
                item.UseSound = SoundID.Item20;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP -= 3;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile p = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            p.GetGlobalProjectile<KeyProjectile>().IsKeybrandProj = true;
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached)
                player.GetModPlayer<KeyPlayer>().Defender = true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("K+:T2Lockblade");
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.GoldBar, 15);
            r.AddIngredient(ItemID.GoldenKey);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    class LockbladeT3Alt : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Refined Lockblade");
            Tooltip.SetDefault($"Alt Attack: Lesser Flame [i:{ItemID.SorcererEmblem}]\n" +
                "MP Cost: 3\n" +
                "Fires a weak spark, faster than Reinforced Lockblade\n" +
                "Ability: Defender\n" +
                "'Sturdy and reliable'");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.15f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
            item.GetGlobalItem<KeyItem>().NoWarning = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.melee = true;
                item.magic = false;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 20;
                item.knockBack = 6;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 18;
                item.knockBack = 1f;
                item.shoot = ProjectileID.Spark;
                item.noMelee = true;
                item.UseSound = SoundID.Item20;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP -= 3;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile p = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            p.GetGlobalProjectile<KeyProjectile>().IsKeybrandProj = true;
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached)
                player.GetModPlayer<KeyPlayer>().Defender = true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("K+:T2Lockblade");
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.PlatinumBar, 15);
            r.AddIngredient(ItemID.GoldenKey);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
