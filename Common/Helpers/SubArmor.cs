using KeybrandsPlus.Common.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
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
            Item.canBePlacedInVanityRegardlessOfConditions = true;
            Item.value = Item.sellPrice(gold: 1);
            SafeSetDefaults();
        }
        public virtual void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
        }
        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int index = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Name.Equals("ItemName"))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                tooltips.Insert(index + 1, new TooltipLine(Mod, "KeybrandsPlus:SubArmor", "Sub-Armor") { OverrideColor = Color.Goldenrod });
            }
            SafeModifyTooltips(tooltips);
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
