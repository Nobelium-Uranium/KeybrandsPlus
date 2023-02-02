using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus.Common.Configs
{
    [Label("Client-side")]
    public class KeyClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static KeyClientConfig Instance;

        [Label("Display total Munny")]
        [Tooltip("Toggles whether or not the total amount of Munny you have is displayed to the left of your screen.\n" +
            "You can safely disable this if it's intrusive.")]
        [DefaultValue(true)]
        public bool DisplayTotalMunny { get; set; }
    }
}
