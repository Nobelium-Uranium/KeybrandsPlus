﻿using Terraria;
using KeybrandsPlus.Common.Globals;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using KeybrandsPlus.Content.Items.Currency;
using KeybrandsPlus.Content.Projectiles;
using KeybrandsPlus.Common.UI;

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
                if (category == "munny")
                {
                    string subcommand = args[1];
                    if (subcommand == "set")
                    {
                        if (int.TryParse(args[2], out int amount))
                        {
                            if (Math.Abs(amount - modPlayer.MunnySavings) > 0)
                                modPlayer.SetRecentMunny(amount - modPlayer.MunnySavings);
                            modPlayer.MunnySavings = amount;
                        }
                        else
                            caller.Reply("Invalid amount");
                    }
                    else if (subcommand == "add")
                    {
                        if (int.TryParse(args[2], out int amount))
                            modPlayer.AddMunny(amount);
                        else
                            caller.Reply("Invalid amount");
                    }
                    else if (subcommand == "remove")
                    {
                        if (int.TryParse(args[2], out int amount))
                            modPlayer.AddMunny(-amount);
                        else
                            caller.Reply("Invalid amount");
                    }
                    else if (subcommand == "rosebud")
                    {
                        if (Math.Abs(999999999 - modPlayer.MunnySavings) > 0)
                            modPlayer.SetRecentMunny(999999999 - modPlayer.MunnySavings);
                        modPlayer.MunnySavings = 999999999;
                    }
                    else if (subcommand == "reset")
                    {
                        if (Math.Abs(-modPlayer.MunnySavings) > 0)
                            modPlayer.SetRecentMunny(-modPlayer.MunnySavings);
                        modPlayer.MunnySavings = 0;
                    }
                }
                else if (category == "fun")
                {
                    string subcommand = args[1];
                    if (subcommand == "kupo")
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