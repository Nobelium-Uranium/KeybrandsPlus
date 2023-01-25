using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using KeybrandsPlus.Common.Systems;
using Terraria.ModLoader.Default;

namespace KeybrandsPlus.Common.Helpers
{
    public abstract class SubArmorItem : ModItem
    {
        public virtual void SafeSetStaticDefaults()
        {
        }
        public sealed override void SetStaticDefaults()
        {
            SafeSetStaticDefaults();
            SacrificeTotal = 1;
        }
        public virtual void SafeSetDefaults()
        {
        }
        public sealed override void SetDefaults()
        {
            Item.DefaultToAccessory();
            SafeSetDefaults();
        }
    }
    public class GlobalSubArmor : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.ModItem is SubArmorItem;
        }
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            int baseSlot = ModContent.GetInstance<SubArmorSlot>().Type;
            return modded && (slot == baseSlot || slot == baseSlot + player.GetModPlayer<ModAccessorySlotPlayer>().SlotCount);
        }
    }
}
