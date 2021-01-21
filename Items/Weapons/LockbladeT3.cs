using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Weapons
{
    class LockbladeT3 : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Refined Lockblade");
            Tooltip.SetDefault("Alt Attack: Lesser Flame\nFires a weak spark, faster than Reinforced Lockblade\nAbility: Defender\n'Sturdy and reliable'");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1;
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
                item.mana = 0;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 20;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.mana = 12;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 18;
                item.shoot = ProjectileID.Spark;
                item.noMelee = true;
                item.UseSound = SoundID.Item20;
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
            player.GetModPlayer<KeyPlayer>().Defender = true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<LockbladeT2>());
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddRecipeGroup("K+:T4Bar", 15);
            r.AddIngredient(ItemID.GoldenKey);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
