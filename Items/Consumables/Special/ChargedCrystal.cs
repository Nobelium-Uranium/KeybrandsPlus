using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace KeybrandsPlus.Items.Consumables.Special
{
    public abstract class ChargedCrystal : ModItem
    {
        public virtual void SafeSetDefaults()
        {
        }
        
        public sealed override void SetDefaults()
        {
            item.Size = new Vector2(18, 28);
            item.rare = ItemRarityID.Cyan;
            item.useAnimation = item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item101;
            item.consumable = true;
            SafeSetDefaults();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * Main.essScale;
        }
    }
    public class ChargedCrystalT1 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amethyst-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 0;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 0)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(165, 0, 236);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(165, 0, 236).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Amethyst, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddRecipeGroup("K+:EvilBar", 20);
            r.AddRecipeGroup("K+:EvilSample", 5);
            r.AddIngredient(ItemID.Bone, 15);
            r.AddIngredient(ItemID.JungleSpores, 10);
            r.AddIngredient(ItemID.HellstoneBar, 25);
            r.AddTile(TileID.DemonAltar);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT2 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Topaz-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming an Amethyst-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 1;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 1)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(255, 198, 0);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(255, 198, 0).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Topaz, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ItemID.HallowedBar, 15);
            r.AddIngredient(ItemID.SoulofFright, 5);
            r.AddIngredient(ItemID.SoulofMight, 5);
            r.AddIngredient(ItemID.SoulofSight, 5);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT3 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapphire-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming a Topaz-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 2;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 2)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(13, 107, 216);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(13, 107, 216).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Sapphire, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ItemID.ChlorophyteBar, 20);
            r.AddIngredient(ItemID.SpectreBar, 20);
            r.AddIngredient(ItemID.ShroomiteBar, 20);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT4 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emerald-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming a Sapphire-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 3;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 3)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(33, 184, 115);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(33, 184, 115).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Emerald, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ModContent.ItemType<Materials.WarriorFragment>(), 25);
            r.AddIngredient(ModContent.ItemType<Materials.GuardianFragment>(), 25);
            r.AddIngredient(ModContent.ItemType<Materials.MysticFragment>(), 25);
            r.AddIngredient(ItemID.BeetleHusk, 10);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT5 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruby-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming an Emerald-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 4;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 4)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(238, 51, 53);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(238, 51, 53).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Ruby, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ItemID.MartianConduitPlating, 50);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT6 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming a Ruby-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 5;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 5)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(218, 185, 210);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(218, 185, 210).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Diamond, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ItemID.FragmentSolar, 10);
            r.AddIngredient(ItemID.FragmentVortex, 10);
            r.AddIngredient(ItemID.FragmentNebula, 10);
            r.AddIngredient(ItemID.FragmentStardust, 10);
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT7 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming a Diamond-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 6;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 6)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(252, 163, 45);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(252, 163, 45).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ItemID.Amber, 10);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ItemID.LunarBar, 5);
            r.AddIngredient(ModContent.ItemType<Synthesis.Other.Zenithite>());
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
    public class ChargedCrystalT8 : ChargedCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar-Charged Crystal");
            Tooltip.SetDefault("Permanently increases maximum MP by 25\n" +
                "Can only be used after consuming an Amber-Charged Crystal");
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<KeyPlayer>().ChargedCrystals == 7;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0 && player.GetModPlayer<KeyPlayer>().ChargedCrystals == 7)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.GetModPlayer<KeyPlayer>().maxMP += 25;
                player.GetModPlayer<KeyPlayer>().ChargedCrystals += 1;
                player.GetModPlayer<KeyPlayer>().currentMP += 25;
                if (Main.myPlayer == player.whoAmI)
                    CombatText.NewText(player.getRect(), Color.DodgerBlue, 25);
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int tooltip = 0; tooltip < tooltips.Count; tooltip++)
            {
                TooltipLine line = tooltips[tooltip];
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(0, 250, 190);
                }
            }
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Color(0, 250, 190).ToVector3() * 0.3f);
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Materials.UnchargedCrystal>());
            r.AddIngredient(ModContent.ItemType<Materials.ZenithFragment>(), 50);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddIngredient(ItemID.LunarBar, 25);
            r.AddIngredient(ItemID.Amethyst);
            r.AddIngredient(ItemID.Topaz);
            r.AddIngredient(ItemID.Sapphire);
            r.AddIngredient(ItemID.Emerald);
            r.AddIngredient(ItemID.Ruby);
            r.AddIngredient(ItemID.Diamond);
            r.AddIngredient(ItemID.Amber);
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
