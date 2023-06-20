using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Materials.Special
{
    public class GradeMaterial1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1000;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(14);
            Item.rare = ModContent.RarityType<LuxRarity>();
            Item.maxStack = 9999;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            KeyUtils.DrawItemWorldTexture(spriteBatch, Texture, Item.position, Item.width, Item.height, rotation, scale, Color.White);
        }
        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient<GradeMaterial2>()
                .Register();
        }
    }
    public class GradeMaterial2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1000;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(22);
            Item.rare = ModContent.RarityType<LuxRarity>();
            Item.maxStack = 9999;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            KeyUtils.DrawItemWorldTexture(spriteBatch, Texture, Item.position, Item.width, Item.height, rotation, scale, Color.White);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GradeMaterial1>(100)
                .Register();

            CreateRecipe(100)
                .AddIngredient<GradeMaterial3>()
                .Register();
        }
    }
    public class GradeMaterial3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1000;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(26);
            Item.rare = ModContent.RarityType<LuxRarity>();
            Item.maxStack = 9999;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            KeyUtils.DrawItemWorldTexture(spriteBatch, Texture, Item.position, Item.width, Item.height, rotation, scale, Color.White);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GradeMaterial2>(100)
                .Register();

            CreateRecipe(100)
                .AddIngredient<GradeMaterial4>()
                .Register();
        }
    }
    public class GradeMaterial4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1000;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(32);
            Item.rare = ModContent.RarityType<LuxRarity>();
            Item.maxStack = 9999;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            KeyUtils.DrawItemWorldTexture(spriteBatch, Texture, Item.position, Item.width, Item.height, rotation, scale, Color.White);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GradeMaterial3>(100)
                .Register();
        }
    }
    public class GradeMaterial5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            KeyUtils.DrawItemWorldTexture(spriteBatch, Texture, Item.position, Item.width, Item.height, rotation, scale, Color.White);
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(26);
            Item.rare = ModContent.RarityType<ElectrumRarity>();
            Item.maxStack = 9999;
        }
    }
}
