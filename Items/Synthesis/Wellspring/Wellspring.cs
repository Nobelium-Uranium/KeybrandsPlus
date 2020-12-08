using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Synthesis.Wellspring
{
    class WellspringShard : TrinityShard
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem fragment filled with energy\nAssociated with spirit");
        }
    }
    class WellspringStone : TrinityStone
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A stone filled with energy\nAssociated with spirit");
        }
    }
    class WellspringGem : TrinityGem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem filled with energy\nAssociated with spirit");
        }
    }
    class WellspringCrystal : SynthCrystal
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A mysterious crystal filled with energy\nAssociated with spirit");
        }
    }
}
