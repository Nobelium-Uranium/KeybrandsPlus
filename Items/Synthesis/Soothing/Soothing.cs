using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Synthesis.Soothing
{
    class SoothingShard : TrinityShard
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem fragment filled with brightness\nAssociated with vitality");
        }
    }
    class SoothingStone : TrinityStone
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A stone filled with brightness\nAssociated with vitality");
        }
    }
    class SoothingGem : TrinityGem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem filled with brightness\nAssociated with vitality");
        }
    }
    class SoothingCrystal : SynthCrystal
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A mysterious crystal filled with brightness\nAssociated with vitality");
        }
    }
}
