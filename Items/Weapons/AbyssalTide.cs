using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Weapons
{
    class AbyssalTide : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("-5 Light Alignment\n+5 Dark Alignment\nAlt Attack: Water\nShoots a stream of water that splashes on contact\nAbility: MP Haste\n'Icing on the cake'");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 29;
            item.melee = true;
            item.width = 23;
            item.height = 25;
            item.scale = 1.1f;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.knockBack = 6.2f;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.GetGlobalItem<KeyItem>().Water = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.damage = 29;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.melee = true;
                item.magic = false;
                item.mana = 0;
                item.noMelee = false;
                item.shoot = 0;
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.damage = 14;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.mana = 7;
                item.shoot = ModContent.ProjectileType<Projectiles.WaterProj>();
                item.noMelee = true;
                item.UseSound = SoundID.Item21;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<KeyPlayer>().MPHaste = true;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().LightAlignment -= 5;
            player.GetModPlayer<KeyPlayer>().DarkAlignment += 5;
        }
    }
}
