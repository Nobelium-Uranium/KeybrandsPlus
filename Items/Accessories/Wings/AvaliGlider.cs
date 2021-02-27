using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace KeybrandsPlus.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    public class AvaliGlider : ModItem
    {
        private bool Gliding;
        private int SlowfallTime;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Modified Aero Glider");
            Tooltip.SetDefault("Grants enhanced gliding capabilities, increasing horizontal mobility, and negates fall damage\n" +
                "Cannot benefit from accessories or other items that increase flight duration\n" +
                "'A portable device worn on the back that can create wings of hard light, meant for gliding'\n" +
                "'The design was originally of the Avali, but it was modified to boast increased performance'");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
            SlowfallTime = 60;
            item.GetGlobalItem<KeyRarity>().DeveloperRarity = true;
            item.GetGlobalItem<KeyRarity>().DeveloperName = "ChemAtDark";
        }
        public override void UpdateEquip(Player player)
        {
            player.wingTime *= 0;
            player.wingTimeMax *= 0;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            player.wingFrameCounter++;
            if (player.wingFrameCounter > 75)
                player.wingFrameCounter = 0;
            if (player.wingFrameCounter > 50 && player.wingFrameCounter <= 55)
                player.wingFrame = 1;
            else if (player.wingFrameCounter > 60 && player.wingFrameCounter <= 65)
                player.wingFrame = 2;
            else if (player.wingFrameCounter > 70 && player.wingFrameCounter <= 75)
                player.wingFrame = 3;
            else
                player.wingFrame = 0;
            return true;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (!player.mount.Active && player.controlJump && player.carpetFrame < 1 && !player.wet)
            {
                if (player.velocity.Y > -3f)
                {
                    if (!Gliding)
                    {
                        Main.PlaySound(SoundID.DoubleJump, (int)player.position.X, (int)player.position.Y, volumeScale: 0.5f);
                        Main.PlaySound(SoundID.Item15, player.position);
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
                        int index = Dust.NewDust(player.position, player.width, player.height, 187, -player.velocity.X / 3, -player.velocity.Y / 3, 0, Color.Cyan);
                        Main.dust[index].noGravity = true;
                        Main.dust[index].scale = 2f;
                        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                    player.maxFallSpeed /= 5;
                }
                else
                {
                    SlowfallTime -= 1;
                    if (SlowfallTime <= 0)
                    {
                        if (Gliding && Main.rand.NextBool())
                        {
                            int index = Dust.NewDust(player.position, player.width, player.height, 187, -player.velocity.X / 5, -player.velocity.Y / 5, 0, Color.Cyan);
                            Main.dust[index].noGravity = true;
                            Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                        }
                        player.maxFallSpeed *= 3f;
                    }
                    else if (Gliding)
                    {
                        int index = Dust.NewDust(player.position, player.width, player.height, 187, -player.velocity.X / 5, -player.velocity.Y / 5, 0, Color.Cyan);
                        Main.dust[index].noGravity = true;
                        Main.dust[index].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                }
            }
            else if(player.grappling[0] >= 0 || ((!player.frozen || player.webbed || player.stoned) && player.velocity.Y == 0f))
            {
                SlowfallTime = 60;
                Gliding = false;
            }
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            if (pre == -1)
                return false;
            return base.PrefixChance(pre, rand);
        }
    }
}