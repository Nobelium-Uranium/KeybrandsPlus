using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class Superglide : ModItem
    {
        private bool Gliding;
        private int SlowfallTime;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants enhanced gliding capabilities\nGreatly increases horizontal mobility and negates fall damage while gliding\nCannot benefit from accessories or other items that increase flight duration");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.rare = ItemRarityID.Pink;
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
                    speed = 15f;
                    acceleration = 0.5f;
                }
                else
                {
                    SlowfallTime = 60;
                    Gliding = false;
                }
                if (Math.Abs(player.velocity.X) > 7.5f)
                {
                    SlowfallTime = 60;
                    if (Gliding)
                    {
                        int index = Dust.NewDust(player.position, player.width, player.height, 16, -player.velocity.X / 2, -player.velocity.Y / 2);
                        Main.dust[index].noGravity = true;
                        Main.dust[index].scale = 1.5f;
                    }
                    player.maxFallSpeed /= 5f;
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
                        }
                        player.maxFallSpeed *= 3f;
                    }
                    else if (Gliding)
                    {
                        int index = Dust.NewDust(player.position, player.width, player.height, 16, -player.velocity.X / 3, -player.velocity.Y / 3);
                        Main.dust[index].noGravity = true;
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
            r.AddIngredient(ItemType<Glide>());
            r.AddIngredient(ItemID.HallowedBar, 25);
            r.AddIngredient(ItemID.SoulofFright);
            r.AddIngredient(ItemID.SoulofMight);
            r.AddIngredient(ItemID.SoulofSight);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}