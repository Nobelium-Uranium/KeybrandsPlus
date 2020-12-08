using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Synthesis.Hungry
{
    class HungryShard : TrinityShard
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem fragment filled with contentment\nAssociated with fulfillment");
        }
    }
    class HungryStone : TrinityStone
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A stone filled with contentment\nAssociated with fulfillment");
        }
    }
    class HungryGem : TrinityGem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem filled with contentment\nAssociated with fulfillment");
        }
    }
    class HungryCrystal : SynthCrystal
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A mysterious crystal filled with contentment\nAssociated with fulfillment");
        }
    }
}
