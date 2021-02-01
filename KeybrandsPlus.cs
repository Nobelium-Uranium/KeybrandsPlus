using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Helpers;
using Terraria.GameContent.UI;
using Terraria.UI;
using System.Collections.Generic;
using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System;
using Terraria.Graphics.Shaders;

namespace KeybrandsPlus
{
    public class KeybrandsPlus : Mod
    {
        internal static KeybrandsPlus Instance;
        
        public static Mod SacredTools;

        public static bool SoALoaded;

        public static int MunnyCost;
        
        public override void Load()
        {
            Instance = ModContent.GetInstance<KeybrandsPlus>();
            
            GameShaders.Armor.BindShader(ModContent.ItemType<Items.Other.FullbrightDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/Fullbright")), "FullbrightShader"));

            #region Mod Support
            SacredTools = ModLoader.GetMod("SacredTools");
            SoALoaded = SacredTools != null;
            #endregion

            if (!Main.dedServ)
                MunnyCost = CustomCurrencyManager.RegisterCurrency(new MunnyData(ModContent.ItemType<Items.Currency.Munny>(), 9999L));
        }
        public override void Unload()
        {
            Instance = null;

            SacredTools = null;
            SoALoaded = false;
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

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Player player = Main.LocalPlayer;
            KeyPlayer keyPlayer = player.GetModPlayer<KeyPlayer>();

            if (keyPlayer.showMP && !player.dead && !Main.playerInventory)
            {
                int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Ruler"));
                LegacyGameInterfaceLayer mpBar = new LegacyGameInterfaceLayer("KeybrandsPlus: MP Gauge",
                    delegate
                    {
                        DrawMPGauge(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(index, mpBar);
            }
        }

        public void DrawMPGauge(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            KeyPlayer keyPlayer = player.GetModPlayer<KeyPlayer>();

            Texture2D gaugeLeft = Instance.GetTexture("UI/MPBarEnd");
            Texture2D gaugeRight = Instance.GetTexture("UI/MPBarText");
            Texture2D gaugeMid = Instance.GetTexture("UI/MPBarSection");
            Texture2D gaugeFill = Instance.GetTexture("UI/MPBarFill");
            float maxMP = keyPlayer.maxMP;
            float currMP = keyPlayer.currentMP;
            float toastTimer = keyPlayer.rechargeMPToastTimer;
            float rechargeTimer = keyPlayer.rechargeMPTimer;
            float maxRechargeTimer = keyPlayer.maxRechargeMPTimer;

            float midFrame = 0;
            float fillFrame = 0;

            if (keyPlayer.rechargeMP)
            {
                midFrame = 1;
                fillFrame = 1;

                if (Main.GameUpdateCount % 60 <= 30) fillFrame = 2;
            }

            Rectangle midRect = new Rectangle(0, (int)(gaugeMid.Height / 2 * midFrame), gaugeMid.Width, gaugeMid.Height / 2);
            Rectangle rightRect = new Rectangle(0, (int)(gaugeRight.Height / 2 * midFrame), gaugeRight.Width, gaugeRight.Height / 2);
            Rectangle fillRect = new Rectangle(0, (int)(gaugeFill.Height / 3 * fillFrame), gaugeFill.Width, gaugeFill.Height / 3);

            Vector2 drawPos = new Vector2(50, 200);

            if (toastTimer > 0)
            {
                drawPos.X = MathHelper.Lerp(-200 - maxMP, 50, 1f - toastTimer / 40);
            }

            Rectangle mpBar = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)(50 + maxMP), 22);
            
            spriteBatch.Draw(gaugeLeft, drawPos, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            drawPos.X += gaugeLeft.Width;

            for(int i = 0; i < maxMP; i++)
            {
                spriteBatch.Draw(gaugeMid, drawPos + new Vector2(i, 0f), midRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            }
            drawPos.X += maxMP;

            Vector2 Offset = new Vector2(drawPos.X - currMP, drawPos.Y);
            for (int i = 0; i < currMP; i++)
            {
                spriteBatch.Draw(gaugeFill, Offset + new Vector2(i, 4f), fillRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(gaugeRight, drawPos, rightRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            if (mpBar.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                if (keyPlayer.rechargeMP)
                    Main.spriteBatch.DrawString(Main.fontMouseText, "Recharging: " + Math.Ceiling(rechargeTimer / 60f) + "s", new Vector2(Main.mouseX + 20, Main.mouseY + 8), Color.Crimson);
                else
                    Main.spriteBatch.DrawString(Main.fontMouseText, currMP + "/" + maxMP, new Vector2(Main.mouseX + 20, Main.mouseY + 8), Color.DodgerBlue);
            }

            //int timerProgress = (int)(gaugeFill.Width * (maxMP - keyPlayer.asthralBlockCooldown) / maxMP);
            //Vector2 drawPos = player.Center + new Vector2(0, -32) - Main.screenPosition;
            //spriteBatch.Draw(gaugeMid, drawPos, null, Color.White, 0f, gaugeMid.Size() / 2f, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(gaugeFill, drawPos, new Rectangle?(new Rectangle(0, 0, timerProgress, gaugeFill.Height)), Color.White, 0f, gaugeFill.Size() / 2f, 1f, SpriteEffects.None, 0f);
        }
    }
}