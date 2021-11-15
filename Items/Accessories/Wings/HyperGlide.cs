using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class Hyperglide : ModItem
    {
        private bool Gliding;
        private int SlowfallTime;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hold JUMP to glide with extreme speed, overrides other forms of flight\nNegates fall damage while active");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
            SlowfallTime = 60;
        }
        public override void UpdateEquip(Player player)
        {
            if (!player.controlJump || !Gliding || SlowfallTime <= 0)
                player.GetModPlayer<Globals.KeyPlayer>().GliderInactive = true;
            player.wingTime *= 0;
            player.wingTimeMax *= 0;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (!player.mount.Active && player.controlJump && player.carpetFrame < 1 && !player.wet)
            {
                if (player.velocity.Y > -3f)
                {
                    if (!Gliding)
                    {
                        Main.PlaySound(SoundID.DoubleJump, player.position);
                        Gliding = true;
                    }
                    speed = 22.5f;
                    acceleration = 0.75f;
                }
                else
                {
                    SlowfallTime = 60;
                    Gliding = false;
                }
                if (Math.Abs(player.velocity.X) > 10f)
                {
                    SlowfallTime = 60;
                    if (Gliding)
                    {
                        int index = Dust.NewDust(player.position, player.width, player.height, 16, -player.velocity.X / 2, -player.velocity.Y / 2);
                        Main.dust[index].noGravity = true;
                        Main.dust[index].scale = 1.5f;
                        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                    player.maxFallSpeed /= 7.5f;
                }
                else
                {
                    SlowfallTime -= 1;
                    if (SlowfallTime <= 0)
                    {
                        if (Gliding && Main.rand.NextBool())
                        {
                            int index = Dust.NewDust(player.position, player.width, player.height, 16, -player.velocity.X / 3, -player.velocity.Y / 3);
                            Main.dust[index].noGravity = true;
                            Main.dust[index].scale = 0.75f;
                            Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                        }
                        player.maxFallSpeed *= 3f;
                    }
                    else if (Gliding)
                    {
                        int index = Dust.NewDust(player.position, player.width, player.height, 16, -player.velocity.X / 3, -player.velocity.Y / 3);
                        Main.dust[index].noGravity = true;
                        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
            }
            else if (player.grappling[0] >= 0 || ((!player.frozen || player.webbed || player.stoned) && player.velocity.Y == 0f))
            {
                SlowfallTime = 60;
                Gliding = false;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemType<Superglide>());
            r.AddIngredient(ItemID.MartianConduitPlating, 50);
            r.AddIngredient(ItemID.SpectreBar, 30);
            r.AddIngredient(ItemID.BeetleHusk, 10);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}