using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Rarities
{
    public abstract class CycleColorRarity : ModRarity
    {
        public virtual Color[] RarityColors => null;
        public virtual int CycleRate => 60;

        private static Color CycleColors(int cycleRate, params Color[] colors)
        {
            float cycleDelta = cycleRate / 60f;
            if (cycleDelta < float.Epsilon)
                cycleDelta = float.Epsilon;
            int index = (int)(Main.GlobalTimeWrappedHourly / cycleDelta / 2f % colors.Length);
            float amount = MathHelper.Clamp(Main.GlobalTimeWrappedHourly / cycleDelta % 2f, 0f, 1f);
            return Color.Lerp(colors[index], colors[(index + 1) % colors.Length], amount);
        }

        public override Color RarityColor
        {
            get
            {
                if (RarityColors == null)
                    return Color.White;
                return CycleColors(CycleRate, RarityColors);
            }
        }
    }
}
