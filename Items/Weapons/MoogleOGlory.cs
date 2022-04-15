using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Weapons
{
    class MoogleOGlory : Helpers.Keybrand
    {
        int munny = 0;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+100 Light and Dark Alignment when held\n" +
                "Alt Attack: Munny Toss\n" +
                "Consumes 100 Munny to throw a coin that can inflict massive damage\n" +
                "Ability: Munny Converter\n" +
                "Boosts damage by 7 for every 77 Munny you have in your inventory\n" +
                "'Don't spend all of your Munny in one place, kupo!'");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.Stabbing;
            item.melee = true;
            item.width = 46;
            item.height = 50;
            item.damage = 7;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 7f;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 50f;
            item.value = 500000000;
            item.GetGlobalItem<KeyItem>().Light = true;
            item.GetGlobalItem<KeyItem>().LimitPenalty = 4;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        { //Thanks, Joost
            munny = 0;
            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<Items.Currency.Munny>() && player.inventory[i].stack > 0)
                {
                    munny += player.inventory[i].stack;
                }
            }
            if (munny == 7777)
                flat = 7770;
            else
                flat = (float)(Math.Floor((double)(munny / 77)) * 7);
        }
        public override bool CanUseItem(Player player)
        {
            munny = 0;
            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<Currency.Munny>() && player.inventory[i].stack > 0)
                {
                    munny += player.inventory[i].stack;
                }
            }
            if (player.altFunctionUse != 2)
            {
                item.useStyle = ItemUseStyleID.Stabbing;
                item.melee = true;
                item.ranged = false;
                item.useTurn = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shoot = 0;
                item.noMelee = false;
                item.noUseGraphic = false;
            }
            else
            {
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.melee = false;
                item.ranged = true;
                item.useTurn = false;
                item.useTime = 10;
                item.useAnimation = 10;
                item.shoot = ProjectileID.GoldCoin;
                item.noMelee = true;
                item.noUseGraphic = true;
                if (munny >= 100)
                {
                    int amount = 100;
                    for (int i = 0; i < 58 && amount > 0; i++)
                    {
                        if (player.inventory[i].stack > 0 && player.inventory[i].type == ModContent.ItemType<Currency.Munny>())
                        {
                            if (player.inventory[i].stack >= amount)
                            {
                                player.inventory[i].stack -= amount;
                                amount = 0;
                            }
                            else
                            {
                                amount -= player.inventory[i].stack;
                                player.inventory[i].SetDefaults(0, false);
                            }
                            if (player.inventory[i].stack <= 0)
                            {
                                player.inventory[i].SetDefaults(0, false);
                            }
                        }
                    }
                }
                return munny >= 100;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                int coin = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ProjectileID.GoldCoin, damage * 10, item.knockBack, player.whoAmI);
                Main.projectile[coin].Name = "Munny";
            }
            return false;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (munny == 7777)
            {
                int oldDefense = target.defense;
                if (target.boss)
                    target.defense /= 10;
                else
                    target.defense = 0;
                damage = 7777;
                crit = false;
                target.defense = oldDefense;
                oldDefense = 0;
            }
            else if (target.type != NPCID.TargetDummy && (crit || Main.rand.NextBool(5)))
                Item.NewItem(target.getRect(), ModContent.ItemType<Currency.Munny>());
            base.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
        }
        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached)
            {
                player.GetModPlayer<KeyPlayer>().LightAlignment += 100;
                player.GetModPlayer<KeyPlayer>().DarkAlignment += 100;
                if (munny == 7777)
                {
                    player.endurance *= 0;
                    player.statDefense *= 0;
                    player.GetModPlayer<KeyPlayer>().LuckySevens = true;
                    Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Dusts.Seven>());
                }
                player.GetModPlayer<KeyPlayer>().MunnyConverter = true;
            }
        }
    }
}
