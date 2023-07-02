using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Rarities
{
    public class EffervescentRarity : CycleColorRarity
    {
        public override Color[] RarityColors
        {
            get
            {
                return new Color[]
                {
                    new Color(1f, .5f, 1f),
                    new Color(1f, 1f, .5f),
                    new Color(.5f, 1f, 1f)
                };
            }
        }
    }
    public class LuxRarity : ModRarity
    {
        public override Color RarityColor => new Color(.66f, 1f, 1f);
    }
    public class ElectrumRarity : CycleColorRarity
    {
        public override int CycleRate => 5;
        public override Color[] RarityColors
        {
            get
            {
                return new Color[]
                {
                    new Color(1f, 1f, .25f),
                    new Color(1f, 1f, .75f)
                };
            }
        }
    }
}
