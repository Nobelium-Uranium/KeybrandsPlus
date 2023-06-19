using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus.Common.Configs
{
    public class KeyServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static KeyServerConfig Instance;

        [DefaultValue(true)]
        public bool MunnyDrops { get; set; }
    }
}
