using KeybrandsPlus.Helpers;

namespace KeybrandsPlus.Items.Synthesis.Mythril
{
    class MythrilShard : SynthShard
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Shard");
            Tooltip.SetDefault("A fragment of mythical ore\nA valuable material for synthesis");
        }
    }
    class MythrilStone : SynthStone
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Stone");
            Tooltip.SetDefault("A stone made from a mythical ore\nA valuable material for synthesis");
        }
    }
    class MythrilGem : SynthGem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Gem");
            Tooltip.SetDefault("A gem which serves as a base for mythical ore\nA valuable material for synthesis");
        }
    }
    class MythrilCrystal : SynthCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Crystal");
            Tooltip.SetDefault("A mysterious crystal made from a mythical ore\nA valuable material for synthesis");
        }
    }
}
