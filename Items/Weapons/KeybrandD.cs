using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Weapons

{
    public class KeybrandD : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+15 Dark Alignment\nAlt Attack: Strike Raid\nThrows a returning ethereal keybrand\nAbility: Defender+\n'A weapon from the dark realm'");
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
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 1;
                item.shoot = ModContent.ProjectileType<Projectiles.StrikeRaid>();
                item.noMelee = true;
                item.noUseGraphic = true;
                item.UseSound = SoundID.Item71;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.StrikeRaid>()] <= 0;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<KeyPlayer>().DefenderPlus = true;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment += 15;
        }
    }
}