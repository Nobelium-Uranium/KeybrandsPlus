using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Helpers
{
    public abstract class SynthShard : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 999;
            item.rare = ItemRarityID.Blue;
        }
    }
    public abstract class SynthStone : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;
            item.maxStack = 999;
            item.rare = ItemRarityID.LightRed;
        }
    }
    public abstract class SynthGem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 30;
            item.maxStack = 999;
            item.rare = ItemRarityID.Lime;
        }
    }
    public abstract class SynthCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.maxStack = 999;
            item.rare = ItemRarityID.Cyan;
        }
    }
    public abstract class TrinityShard : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.maxStack = 999;
            item.rare = ItemRarityID.Blue;
        }
    }
    public abstract class TrinityStone : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.maxStack = 999;
            item.rare = ItemRarityID.LightRed;
        }
    }
    public abstract class TrinityGem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 36;
            item.maxStack = 999;
            item.rare = ItemRarityID.Lime;
        }
    }
}
