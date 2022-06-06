using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus.Common
{
    [Label("Client")]
    internal class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static ClientConfig Instance;

        [Label("MP Bar Position X")]
        [Range(0, 9999)]
        [DefaultValue(50)]
        public int MPBarPosX { get; set; }

        [Label("MP Bar Position Y")]
        [Range(0, 4000)]
        [DefaultValue(200)]
        public int MPBarPosY { get; set; }
    }
}
