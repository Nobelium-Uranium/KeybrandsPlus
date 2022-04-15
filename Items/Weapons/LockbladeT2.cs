using Terraria;
using Terraria.ID;
using KeybrandsPlus.Helpers;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Items.Weapons
{
    class LockbladeT2 : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reinforced Lockblade");
            Tooltip.SetDefault("Alt Attack: Lesser Flame\n" +
                "MP Cost: 2\n" +
                "Fires a weak spark\n" +
                "No Abilities");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 17;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.075f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5.5f;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 7.5f;
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
                item.damage = 15;
                item.knockBack = 5.5f;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.useTime = 28;
                item.useAnimation = 28;
                item.damage = 12;
                item.knockBack = 1f;
                item.shoot = ProjectileID.Spark;
                item.noMelee = true;
                item.UseSound = SoundID.Item20;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP -= 2;
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
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("K+:T1Lockblade");
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.SilverBar, 15);
            r.AddIngredient(ItemID.WandofSparking);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    class LockbladeT2Alt : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reinforced Lockblade");
            Tooltip.SetDefault("Alt Attack: Lesser Flame\n" +
                "MP Cost: 2\n" +
                "Fires a weak spark\n" +
                "No Abilities");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 17;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.075f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5.5f;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 7.5f;
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
                item.damage = 15;
                item.knockBack = 5.5f;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.useTime = 28;
                item.useAnimation = 28;
                item.damage = 12;
                item.knockBack = 1f;
                item.shoot = ProjectileID.Spark;
                item.noMelee = true;
                item.UseSound = SoundID.Item20;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP -= 2;
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
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddRecipeGroup("K+:T1Lockblade");
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.TungstenBar, 15);
            r.AddIngredient(ItemID.WandofSparking);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
