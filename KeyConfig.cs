using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace KeybrandsPlus
{
    public class KeyServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("MP Regeneration")]
        [Tooltip("Enables slow but passive regeneration of the MP gauge\nAdded as a joke, disabled by default")]
        [DefaultValue(false)]
        public bool MPRegen { get; set; }
    }
    public class KeyClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Always Show MP Gauge")]
        [Tooltip("Determines whether or not the MP gauge should be shown when not holding a keybrand\nIt will still appear when you're holding a keybrand if disabled, but will be hidden otherwise\nEnabled by default")]
        [DefaultValue(true)]
        public bool AlwaysShowMP { get; set; }
    }
}
