﻿using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Weapons
{
    class Keybrand : Helpers.Keybrand
    {
        public override string Texture => "Terraria/Item_" + ItemID.Keybrand;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15 Light Alignment\n" +
                "Direct melee hits inflict up to 150% more damage to injured foes\n" +
                "MP Cost: 20\n" +
                "Alt Attack: Judgement\n" +
                "Throws a ethereal keybrand that follows the cursor\n" +
                "Ability: Damage Control\n" +
                "'A weapon from the realm of light'");
        }
        public override void SetDefaults()
        {
            item.crit = 13;
            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.knockBack = 6.5f;
            item.width = 40;
            item.height = 40;
            item.damage = 70;
            item.scale = 1.2f;
            item.UseSound = SoundID.Item1;
            item.rare = 8;
            item.value = 138000;
            item.melee = true;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            float t = (float)target.life / (float)target.lifeMax;
            float lerpValue = Helpers.KeyUtils.GetLerpValue(1f, 0.1f, t, true);
            float damageBoost = 1.5f * lerpValue;
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
                item.useTime = 20;
                item.useAnimation = 20;
                item.shoot = 0;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.shootSpeed = 25f;
                item.melee = false;
                item.ranged = true;
                item.useTurn = false;
                item.useTime = 10;
                item.useAnimation = 10;
                item.shoot = ModContent.ProjectileType<Projectiles.Judgement>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.UseSound = SoundID.Item71;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Judgement>()] <= 0) player.GetModPlayer<KeyPlayer>().currentMP -= 20;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Judgement>()] <= 0;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Judgement>()] <= 0;
        }
        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached)
                player.GetModPlayer<KeyPlayer>().DamageControl = true;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment += 15;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<LockbladeT3>());
            r.AddIngredient(ModContent.ItemType<Materials.RustedKeybrand>());
            r.AddIngredient(ModContent.ItemType<Materials.WarriorFragment>(), 5);
            r.AddIngredient(ModContent.ItemType<Materials.GuardianFragment>(), 5);
            r.AddIngredient(ModContent.ItemType<Materials.MysticFragment>(), 5);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
