using System;
using On.Terraria.UI;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.ModHooks
{
    public interface IItemOverrideLeftClick
    {
        bool OverrideLeftClick(Item[] inventory, int context, int slot);
    }
    public class ModHookSystem : ModSystem
    {
        public override void Load()
        {
            ItemSlot.OverrideLeftClick += new ItemSlot.hook_OverrideLeftClick(ApplyLeftClick);
        }
        private bool ApplyLeftClick(ItemSlot.orig_OverrideLeftClick orig, Item[] inv, int context, int slot)
        {
            if (Main.mouseLeft && Main.mouseLeftRelease)
            {
                bool result = false;
                if (inv[slot].ModItem is IItemOverrideLeftClick)
                    result |= (inv[slot].ModItem as IItemOverrideLeftClick).OverrideLeftClick(inv, context, slot);
                return result || orig.Invoke(inv, context, slot);
            }
            return orig.Invoke(inv, context, slot);
        }
    }
}
