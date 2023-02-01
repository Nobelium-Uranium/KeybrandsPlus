using KeybrandsPlus.Common.Helpers;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Systems
{
    public class SubArmorSlot : ModAccessorySlot
    {
        public override string FunctionalTexture => "KeybrandsPlus/Assets/UI/SubArmorIcon";
        public override string VanityTexture => "KeybrandsPlus/Assets/UI/SubArmorVanityIcon";
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            bool result;
            if (context != AccessorySlotType.FunctionalSlot)
            {
                result = context != AccessorySlotType.VanitySlot || (checkItem.ModItem is SubArmorItem && checkItem.FitsAccessoryVanitySlot);
            }
            else
            {
                result = checkItem.ModItem is SubArmorItem;
            }
            return result;
        }
        public override void OnMouseHover(AccessorySlotType context)
        {
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                    Main.hoverItemName = "Sub-Armor";
                    break;
                case AccessorySlotType.VanitySlot:
                    Main.hoverItemName = "Auxiliary Sub-Armor";
                    break;
                default:
                    return;
            }

        }
    }
}
