using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Weapons
{
    class CircleOfLife : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Circle of Life");
            Tooltip.SetDefault("+10 Light Alignment\n" +
                "Alt Action: Cure\n" +
                "MP Cost: All\n" +
                "Creates a healing field at the cursor's postion\n" +
                "The range and heal rate depends on your Cure efficiency\n" +
                "Can heal players who are on the same team as you are\n" +
                "Ability: Alive 'n' Kicking\n" +
                "'Hakuna Matata!'");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 27;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.1f;
            item.useTime = 29;
            item.useAnimation = 29;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1.6f;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 1f;
            item.autoReuse = true;
            item.GetGlobalItem<KeyItem>().AliveNKicking = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.damage = 28;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.useTime = 14;
                item.useAnimation = 14;
                item.melee = true;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                item.shoot = 0;
            }

            else
            {
                item.damage = 0;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.useTime = 45;
                item.useAnimation = 45;
                item.melee = false;
                item.noMelee = true;
                item.UseSound = SoundID.Item29;
                item.shoot = 10;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP = 0;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                position = Main.MouseWorld;
                Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<Projectiles.CureField>(), 0, 0, player.whoAmI);
            }
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.KeybrandMold>());
            r.AddIngredient(ItemID.LifeCrystal, 3);
            r.AddIngredient(ItemID.JungleSpores, 14);
            r.AddIngredient(ItemID.Stinger, 18);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
