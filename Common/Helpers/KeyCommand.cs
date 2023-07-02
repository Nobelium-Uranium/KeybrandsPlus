using Terraria;
using KeybrandsPlus.Common.Globals;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using KeybrandsPlus.Content.Items.Currency;
using KeybrandsPlus.Content.Projectiles;
using KeybrandsPlus.Common.UI;
using KeybrandsPlus.Content.Items.Other;

//Dev SteamIDs for reference
//Chem: 76561198079106803
//Dan: 76561198178272217

namespace KeybrandsPlus.Common.Helpers
{
    public class KeyCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "kplus";
        public override string Usage => "Do you know the rules?";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (KeybrandsPlus.SteamID == "76561198079106803" || KeybrandsPlus.SteamID == "76561198178272217")
            {
                KeyPlayer modPlayer = caller.Player.GetModPlayer<KeyPlayer>();
                string category = args[0];
                if (category.ToLower() == "munny")
                {
                    string subcommand = args[1];
                    if (subcommand.ToLower() == "set")
                    {
                        if (int.TryParse(args[2], out int amount))
                        {
                            if (Math.Abs(amount - modPlayer.MunnySavings) > 0)
                                modPlayer.SetRecentMunny(amount - modPlayer.MunnySavings);
                            modPlayer.MunnySavings = amount;
                        }
                    }
                    else if (subcommand.ToLower() == "add")
                    {
                        if (int.TryParse(args[2], out int amount))
                            modPlayer.AddMunny(amount);
                    }
                    else if (subcommand.ToLower() == "subtract" || subcommand.ToLower() == "remove")
                    {
                        if (int.TryParse(args[2], out int amount))
                            modPlayer.AddMunny(-amount);
                    }
                    else if (subcommand.ToLower() == "max" || subcommand.ToLower() == "rosebud")
                    {
                        if (Math.Abs(999999999 - modPlayer.MunnySavings) > 0)
                            modPlayer.SetRecentMunny(999999999 - modPlayer.MunnySavings);
                        modPlayer.MunnySavings = 999999999;
                    }
                    else if (subcommand.ToLower() == "min" || subcommand.ToLower() == "reset")
                    {
                        if (Math.Abs(-modPlayer.MunnySavings) > 0)
                            modPlayer.SetRecentMunny(-modPlayer.MunnySavings);
                        modPlayer.MunnySavings = 0;
                    }
                }
                else if (category.ToLower() == "treasure")
                {
                    string type = args[1];
                    if (type.ToLower() == "basic")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox1>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                    else if (type.ToLower() == "basicrepeat")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox1Repeat>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                    else if (type.ToLower() == "hard")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox2>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                    else if (type.ToLower() == "hardrepeat")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox2Repeat>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                    else if (type.ToLower() == "lunar")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox3>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                    else if (type.ToLower() == "lunarrepeat")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox3Repeat>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                    else if (type.ToLower() == "radiant")
                    {
                        if (int.TryParse(args[2], out int score))
                        {
                            int itemIndex = caller.Player.QuickSpawnItem(caller.Player.GetSource_Misc("KPlusCommand"), ModContent.ItemType<TreasureBox3Special>());
                            Item item = Main.item[itemIndex];
                            TreasureBox treasureBox = (TreasureBox)item.ModItem;
                            treasureBox.SetScore(score);
                        }
                    }
                }
                else if (category.ToLower() == "fun")
                {
                    string subcommand = args[1];
                    if (subcommand.ToLower() == "kupo")
                    {
                        if (!modPlayer.KupoMode)
                        {
                            modPlayer.KupoMode = true;
                            caller.Reply("Kupo Mode On");
                        }
                        else
                        {
                            modPlayer.KupoMode = false;
                            caller.Reply("Kupo Mode Off");
                        }
                    }
                }
            }
        }
    }
}
