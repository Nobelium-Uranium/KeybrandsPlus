using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;


namespace KeybrandsPlus.Items.Weapons
{
    class EdgeOfUltima : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Edge of Ultima");
            Tooltip.SetDefault("Debugging tool\n'You shouldn't have this'");
        }
        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.knockBack = 6.5f;
            item.width = 40;
            item.height = 40;
            item.damage = 100;
            item.scale = 1.2f;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 50f;
            item.GetGlobalItem<KeyItem>().ExemptFromLimit = true;
            item.GetGlobalItem<KeyRarity>().ZenithRarity = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.useTurn = true;
                item.noUseGraphic = false;
                item.noMelee = false;
                item.shoot = 0;
            }
            else
            {
                item.useTurn = false;
                item.noUseGraphic = true;
                item.noMelee = true;
                item.shoot = ModContent.ProjectileType<Projectiles.StrikeRaid>();
            }
            return base.CanUseItem(player);
        }
    }
}
