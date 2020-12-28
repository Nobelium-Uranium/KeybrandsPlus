using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Synthesis.Frost
{
    class FrostShard : SynthShard
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frigid Shard");
            Tooltip.SetDefault("A gem fragment filled with ice\nAssociated with frost");
        }
    }
    class FrostStone : SynthStone
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frigid Stone");
            Tooltip.SetDefault("A stone filled with ice\nAssociated with frost");
        }
    }
    class FrostGem : SynthGem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frigid Gem");
            Tooltip.SetDefault("A gem filled with ice\nAssociated with frost");
        }
    }
    class FrostCrystal : SynthCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frigid Crystal");
            Tooltip.SetDefault("A mysterious crystal filled with ice\nAssociated with frost");
        }
    }
}
