using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Weapons
{
    class TrueKeybrand : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kingdom Key");
            Tooltip.SetDefault("+30 Light Alignment\n" +
                "Direct melee hits inflict up to 200% more damage to injured foes\n" +
                "Alt Attack: Judgement Triad\n" +
                "MP Cost: 30\n" +
                "Throws up to 3 ethereal keybrands that home into enemies\n" +
                "Abilities: Damage Control, Leaf Bracer\n" +
                "'Imbued with the forces of light'");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 175;
            item.width = 52;
            item.height = 58;
            item.scale = 1.25f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 17;
            item.useAnimation = 17;
            item.knockBack = 7;
            item.crit = 13;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 25f;
            item.GetGlobalItem<KeyItem>().Light = true;
            item.GetGlobalItem<KeyItem>().LimitPenalty = 1;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            float t = (float)target.life / (float)target.lifeMax;
            float lerpValue = Helpers.KeyUtils.GetLerpValue(1f, 0.1f, t, true);
            float damageBoost = 2f * lerpValue;
            damage = (int)(damage * (1 + damageBoost));
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.melee = true;
                item.ranged = false;
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useTurn = true;
                item.useTime = 17;
                item.useAnimation = 17;
                item.shoot = ProjectileID.None;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.melee = false;
                item.ranged = true;
                item.useTurn = false;
                item.useTime = 10;
                item.useAnimation = 10;
                item.shoot = ModContent.ProjectileType<Projectiles.Judgement>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.UseSound = SoundID.Item71;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Judgement>()] < 3) player.GetModPlayer<KeyPlayer>().currentMP -= 30;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Judgement>()] < 3;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                float rotation = MathHelper.ToRadians(15);
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(rotation);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, ai1: 1);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached)
            {
                player.GetModPlayer<KeyPlayer>().DamageControl = true;
                player.GetModPlayer<KeyPlayer>().LeafBracer = true;
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment += 30;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Keybrand>());
            r.AddIngredient(ModContent.ItemType<Materials.BrokenHeroKeybrand>());
            r.AddIngredient(ModContent.ItemType<Materials.WarriorFragment>(), 50);
            r.AddIngredient(ModContent.ItemType<Materials.GuardianFragment>(), 50);
            r.AddIngredient(ModContent.ItemType<Materials.MysticFragment>(), 50);
            r.AddIngredient(ModContent.ItemType<Synthesis.Other.Zenithite>(), 3);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
