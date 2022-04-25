using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace KeybrandsPlus.Items.Materials
{
    public class AeroCarbonAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aero-Carbon Alloy");
            Tooltip.SetDefault("An alloy specially engineered to be very light and flexible");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.rare = ItemRarityID.Cyan;
            item.maxStack = 999;
            item.value = Item.sellPrice(silver: 10);
        }
    }
    public class AerosteelPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aerosteel Plating");
            Tooltip.SetDefault("A plate made with light yet durable metal");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.rare = ItemRarityID.Cyan;
            item.maxStack = 999;
            item.value = Item.sellPrice(silver: 25);
        }
    }
    public class Aerogel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aerogel");
            Tooltip.SetDefault("An extremely light and structurally stable material");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.maxStack = 999;
            item.value = Item.sellPrice(gold: 1);
        }
    }
}
