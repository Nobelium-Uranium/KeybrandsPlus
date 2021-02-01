using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Weapons

{
    public class TrueKeybrandD : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kingdom Key D");
            Tooltip.SetDefault("+30 Dark Alignment\n" +
                "Direct melee hits inflict up to 200% more damage to injured foes\n" +
                "Alt Attack: Elemental Raid\n" +
                "MP Cost: 24\n" +
                "Throws a returning ethereal keybrand imbued with the elements\n" +
                "Abilities: Defender+, Leaf Bracer\n" +
                "'Imbued with the forces of darkness'");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 150;
            item.width = 52;
            item.height = 58;
            item.scale = 1.25f;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 8;
            item.crit = 13;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 75f;
            item.GetGlobalItem<KeyItem>().Dark = true;
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
                item.useTime = 15;
                item.useAnimation = 15;
                item.knockBack = 8;
                item.shoot = ProjectileID.None;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.melee = false;
                item.ranged = true;
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 1;
                item.shoot = ModContent.ProjectileType<Projectiles.StrikeRaid>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.UseSound = SoundID.Item71;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.StrikeRaid>()] <= 0) player.GetModPlayer<KeyPlayer>().currentMP -= 24;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.StrikeRaid>()] <= 0;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.StrikeRaid>()] <= 0;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 1);
            return false;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<KeyPlayer>().DefenderPlus = true;
            player.GetModPlayer<KeyPlayer>().LeafBracer = true;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment += 30;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<KeybrandD>());
            r.AddIngredient(ItemID.SoulofFright, 10);
            r.AddIngredient(ItemID.SoulofMight, 10);
            r.AddIngredient(ItemID.SoulofSight, 10);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}