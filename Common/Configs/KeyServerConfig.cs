using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus.Common.Configs
{
    [Label("Server-side")]
    public class KeyServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static KeyServerConfig Instance;

        [Label("Enable Munny Drops")]
        [Tooltip("Toggles whether or not enemies will drop Munny. Doesn't affect the displayed drop tables in the Bestiary.")]
        [DefaultValue(true)]
        public bool MunnyDrops { get; set; }
    }
}
