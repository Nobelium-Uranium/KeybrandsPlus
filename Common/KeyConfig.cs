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
        [Tooltip("Sets the horizontal position of the MP Bar\n" +
            "While this is automatically set by dragging it, make sure to manually save the config afterwards\n" +
            "This can be done by modifying any value then setting it back, then you'll be able to save")]
        [Range(0, 5000)]
        [DefaultValue(50)]
        public int MPBarPosX { get; set; }

        [Label("MP Bar Position Y")]
        [Tooltip("Sets the vertical position of the MP Bar\n" +
            "While this is automatically set by dragging it, make sure to manually save the config afterwards\n" +
            "This can be done by modifying any value then setting it back, then you'll be able to save")]
        [Range(0, 5000)]
        [DefaultValue(200)]
        public int MPBarPosY { get; set; }
    }
}
