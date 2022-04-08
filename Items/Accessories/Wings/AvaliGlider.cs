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
        private int flyStart;
        private bool Gliding;
        private int SlowfallTime;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Modified Aero Glider");
            Tooltip.SetDefault("Hold JUMP to glide with high speed, overrides other forms of flight\n" +
                "Negates fall damage\n" +
                "'A portable device worn on the back that can create wings of hard light, meant for gliding'\n" +
                "'Originally designed by a race beyond the stars, it was modified to boast increased performance'");
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
            player.GetModPlayer<KeyPlayer>().AvaliWings = true;
            player.wingTime *= 0;
            player.wingTimeMax *= 0;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (player.GetModPlayer<KeyPlayer>().Flying)
            {
                if (flyStart < 2)
                {
                    flyStart++;
                }
                else if (flyStart > 2)
                {
                    flyStart = 2;
                }
            }
            else
            {
                if (flyStart > 0)
                {
                    flyStart--;
                }
                else if (flyStart < 0)
                {
                    flyStart = 0;
                }
            }
            if (flyStart == 1)
            {
                player.wingFrameCounter = 0;
            }
            player.wingFrameCounter++;
            if (player.controlJump && player.velocity.Y != 0 && !player.GetModPlayer<KeyPlayer>().AvaliWings)
            {
                if (player.wingTime > 0)
                {
                    player.wingFrameCounter++;
                }
                if (player.wingFrameCounter > 48)
                    player.wingFrameCounter = 0;
                if (player.wingFrameCounter > 0 && player.wingFrameCounter <= 6)
                    player.wingFrame = 1;
                else if ((player.wingFrameCounter > 12 && player.wingFrameCounter <= 18) || (player.wingFrameCounter > 36 && player.wingFrameCounter <= 42))
                    player.wingFrame = 2;
                else if (player.wingFrameCounter > 24 && player.wingFrameCounter <= 30)
                    player.wingFrame = 3;
                else
                    player.wingFrame = 0;
                if (!player.GetModPlayer<KeyPlayer>().AvaliWings && player.wingTimeMax > 0 && player.controlJump && player.velocity.Y != 0 && player.wingFrame == 3)
                {
                    Main.PlaySound(SoundID.Item15.WithVolume(0.25f), player.position);
                }
            }
            else
            {
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
            }
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