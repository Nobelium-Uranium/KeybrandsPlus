using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Weapons
{
    class ChimeraKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Debugging tool\n'You shouldn't have this'");
        }
        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useAnimation = 45;
            item.useTime = 45;
            item.width = 20;
            item.height = 20;
            item.damage = 100;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<Projectiles.ChimeraTooth>();
            item.shootSpeed = 25f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 10;
            for (int i = 0; i < numberProjectiles; i++)
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
