using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus.Common.Configs
{
    public class KeyServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static KeyServerConfig Instance;

        [DefaultValue(true)]
        public bool MunnyDrops { get; set; }
    }
}
