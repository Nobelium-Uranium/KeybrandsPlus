using KeybrandsPlus.Common.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace KeybrandsPlus.Common.Systems
{
    public class InterfaceLayerSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Resource Bars");
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer("KeybrandsPlus: Munny Display", delegate()
                {
                    MunnyUI.Draw(Main.spriteBatch, Main.LocalPlayer);
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}
