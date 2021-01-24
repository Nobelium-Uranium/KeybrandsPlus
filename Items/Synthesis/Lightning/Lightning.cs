using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Synthesis.Lightning
{
    class LightningShard : SynthShard
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Shard");
            Tooltip.SetDefault("A gem fragment filled with lightning\nAssociated with electricity");
        }
    }
    class LightningStone : SynthStone
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Stone");
            Tooltip.SetDefault("A stone filled with lightning\nAssociated with electricity");
        }
    }
    class LightningGem : SynthGem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Gem");
            Tooltip.SetDefault("A gem filled with lightning\nAssociated with electricity");
        }
    }
    class LightningCrystal : SynthCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Crystal");
            Tooltip.SetDefault("A mysterious crystal filled with lightning\nAssociated with electricity");
        }
    }
}
