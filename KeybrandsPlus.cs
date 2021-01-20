using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;
using Terraria.GameContent.UI;

namespace KeybrandsPlus
{
    public class KeybrandsPlus : Mod
    {
        public static int MunnyCost;
        
        public override void Load()
        {
            if (!Main.dedServ)
                MunnyCost = CustomCurrencyManager.RegisterCurrency(new MunnyData(ModContent.ItemType<Items.Currency.Munny>(), 9999L));
        }
        public override void Unload()
        {
            MunnyCost = 0;
        }
        public override void AddRecipes()
        {
            RecipeFinder finder = new RecipeFinder();
            finder.AddIngredient(ItemID.Keybrand);
            foreach (Recipe recipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(recipe);
                editor.DeleteIngredient(ItemID.Keybrand);
                editor.AddIngredient(ModContent.ItemType<Items.Weapons.Keybrand>());
            }
            finder = new RecipeFinder();
            finder.SetResult(ItemID.Keybrand);
            foreach (Recipe recipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(recipe);
                editor.SetResult(ModContent.ItemType<Items.Materials.RustedKeybrand>());
            }
            ModRecipe r = new ModRecipe(this);
            r.AddIngredient(ItemID.HellstoneBar, 15);
            r.AddTile(TileID.Anvils);
            r.SetResult(ItemID.Flamarang);
            r.AddRecipe();
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup g = new RecipeGroup(() => Lang.misc[37] + " Shortsword", new int[]
                {
                    ItemID.CopperShortsword,
                    ItemID.TinShortsword,
                    ItemID.IronShortsword,
                    ItemID.LeadShortsword,
                    ItemID.SilverShortsword,
                    ItemID.TungstenShortsword,
                    ItemID.GoldShortsword,
                    ItemID.PlatinumShortsword
                });
            RecipeGroup.RegisterGroup("K+:Shortsword", g);
            g = new RecipeGroup(() => "Copper/Tin Bar", new int[]
                {
                    ItemID.CopperBar,
                    ItemID.TinBar,
                });
            RecipeGroup.RegisterGroup("K+:T1Bar", g);
            g = new RecipeGroup(() => "Iron/Lead Bar", new int[]
                {
                    ItemID.IronBar,
                    ItemID.LeadBar,
                });
            RecipeGroup.RegisterGroup("K+:T2Bar", g);
            g = new RecipeGroup(() => "Silver/Tungsten Bar", new int[]
                {
                    ItemID.SilverBar,
                    ItemID.TungstenBar,
                });
            RecipeGroup.RegisterGroup("K+:T3Bar", g);
            g = new RecipeGroup(() => "Gold/Platinum Bar", new int[]
                {
                    ItemID.GoldBar,
                    ItemID.PlatinumBar,
                });
            RecipeGroup.RegisterGroup("K+:T4Bar", g);
            g = new RecipeGroup(() => "Demonite/Crimtane Bar", new int[]
                {
                    ItemID.DemoniteBar,
                    ItemID.CrimtaneBar,
                });
            RecipeGroup.RegisterGroup("K+:EvilBar", g);
            g = new RecipeGroup(() => "Shadow Scale/Tissue Sample", new int[]
                {
                    ItemID.ShadowScale,
                    ItemID.TissueSample,
                });
            RecipeGroup.RegisterGroup("K+:EvilSample", g);
            g = new RecipeGroup(() => "Cobalt/Palladium Bar", new int[]
                {
                    ItemID.CobaltBar,
                    ItemID.PalladiumBar,
                });
            RecipeGroup.RegisterGroup("K+:HardT1Bar", g);
            g = new RecipeGroup(() => "Mythril/Orichalcum Bar", new int[]
                {
                    ItemID.MythrilBar,
                    ItemID.OrichalcumBar,
                });
            RecipeGroup.RegisterGroup("K+:HardT2Bar", g);
            g = new RecipeGroup(() => "Adamantite/Titanium Bar", new int[]
                {
                    ItemID.AdamantiteBar,
                    ItemID.TitaniumBar,
                });
            RecipeGroup.RegisterGroup("K+:HardT3Bar", g);
            g = new RecipeGroup(() => Lang.misc[37] + " Boss Soul", new int[]
                {
                    ItemID.SoulofFright,
                    ItemID.SoulofMight,
                    ItemID.SoulofSight,
                });
            RecipeGroup.RegisterGroup("K+:BossSoul", g);
        }
    }
}