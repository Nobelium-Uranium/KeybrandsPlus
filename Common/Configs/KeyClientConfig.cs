using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
