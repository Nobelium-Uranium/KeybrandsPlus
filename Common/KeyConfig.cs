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
        [Tooltip("Sets the horizontal position of the MP Bar")]
        [Range(0, 5000)]
        [DefaultValue(50)]
        public int MPBarPosX { get; set; }

        [Label("MP Bar Position Y")]
        [Tooltip("Sets the vertical position of the MP Bar")]
        [Range(0, 5000)]
        [DefaultValue(200)]
        public int MPBarPosY { get; set; }

        [Label("Lock MP Bar Position")]
        [Tooltip("Toggles the ability to drag the MP Bar to set its position\n" +
            "Its position can still be modified by changing the values through this config")]
        [DefaultValue(false)]
        public bool LockMPBar { get; set; }
    }
}
