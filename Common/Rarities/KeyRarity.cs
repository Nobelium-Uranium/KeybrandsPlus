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
        public override Color RarityColor => new Color(.5f, 1f, 1f);
    }
    public class ElectrumRarity : ModRarity
    {
        public override Color RarityColor => new Color(1f, 1f, .5f);
    }
}
