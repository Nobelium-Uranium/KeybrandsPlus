using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Weapons

{
    public class KeybrandD : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15 Dark Alignment\n" +
                "Direct melee hits inflict up to 100% more damage to injured foes\n" +
                "Alt Attack: Strike Raid\n" +
                "MP Cost: 6\n" +
                "Throws returning ethereal keybrands\n" +
                "Ability: Defender+\n" +
                "'A weapon from the dark realm'");
        }

        public override void SetDefaults()
        {
            item.damage = 55;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.15f;
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = 1;
            item.knockBack = 6;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 50f;
            item.GetGlobalItem<KeyItem>().Dark = true;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            float t = (float)target.life / (float)target.lifeMax;
            float lerpValue = Helpers.KeyUtils.GetLerpValue(1f, 0.1f, t, true);
            float damageBoost = 1f * lerpValue;
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
                item.useTime = 19;
                item.useAnimation = 19;
                item.knockBack = 6;
                item.shoot = 0;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.melee = false;
                item.ranged = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.knockBack = 1;
                item.shoot = ModContent.ProjectileType<Projectiles.StrikeRaid>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.UseSound = SoundID.Item71;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP -= 6;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP;
            }
            return base.CanUseItem(player);
        }

        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached)
                player.GetModPlayer<KeyPlayer>().DefenderPlus = true;
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().DarkAlignment += 15;
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<KeybrandOfHeart>());
            r.AddIngredient(ModContent.ItemType<AbyssalTide>());
            r.AddIngredient(ModContent.ItemType<CircleOfLife>());
            r.AddIngredient(ModContent.ItemType<FlameLiberator>());
            r.AddTile(TileID.DemonAltar);
            r.SetResult(this);
            r.AddRecipe();
        }

    }
}