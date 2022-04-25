using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;

namespace KeybrandsPlus.Items.Synthesis.Other
{
    class Fluorite : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A softly glowing piece of ore");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(24);
            item.maxStack = 99;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(silver: 1);
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
    }
    class Damascus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A hard and resilient piece of ore");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(24);
            item.maxStack = 99;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(silver: 10);
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
    }
    class Adamantine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gloomy piece of ore");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(24);
            item.maxStack = 99;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(gold: 1);
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
    }
    class Electrum : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A piece of ore that radiates golden light");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(30);
            item.maxStack = 99;
            item.rare = ItemRarityID.Cyan;
            item.value = Item.sellPrice(gold: 10);
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
    }
}
