using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus.Common.Configs
{
    public class KeyClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static KeyClientConfig Instance;

        [DefaultValue(true)]
        public bool DisplayTotalMunny { get; set; }
    }
}
