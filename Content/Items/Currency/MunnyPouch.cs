using System.Collections.Generic;
using System.Linq;
using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.ModHooks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace KeybrandsPlus.Content.Items.Currency
{
    public class MunnyPouch : ModItem, IItemOverrideLeftClick
    {
        public ulong storedMunny;

        public override ModItem Clone(Item newEntity)
        {
            MunnyPouch clone = (MunnyPouch)base.Clone(newEntity);
            clone.storedMunny = storedMunny;
            return clone;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A convenient place to store your hard-earned Munny\n" +
                "<right> to quickly withdraw up to 9999 Munny\n" +
                "#MUNNY");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(24);
            Item.rare = ItemRarityID.Quest;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int index = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Name.StartsWith("Tooltip") && tooltips[i].Text.Contains("#MUNNY"))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                tooltips.RemoveAt(index);
                tooltips.Insert(index, new TooltipLine(ModContent.GetInstance<KeybrandsPlus>(), "KPlus:StoredMunny", $"[i:{ModContent.ItemType<Munny>()}]" + storedMunny)
                {
                    OverrideColor = Color.Goldenrod
                });
            }
        }
        public override bool CanRightClick()
        {
            if ((!Main.mouseItem.IsAir && Main.mouseItem.type != ModContent.ItemType<Munny>()) || (Main.mouseItem.type == ModContent.ItemType<Munny>() && Main.mouseItem.stack >= Main.mouseItem.maxStack))
                return false;
            if (storedMunny > 0)
                return true;
            return false;
        }
        public override void RightClick(Player player)
        {
            int amount = (int)Utils.Clamp<ulong>(storedMunny, 0, 9999);
            int remainder = 0;
            if (Main.mouseItem.type == ModContent.ItemType<Munny>() && Main.mouseItem.stack + amount > Main.mouseItem.maxStack)
                remainder = Main.mouseItem.stack + amount - Main.mouseItem.maxStack;
            if (amount > 0)
            {
                storedMunny -= (ulong)amount - (ulong)remainder;
                Main.mouseItem.SetDefaults(ModContent.ItemType<Munny>());
                Main.mouseItem.stack = amount;
            }
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }
        public bool OverrideLeftClick(Item[] inventory, int context, int slot)
        {
            if (ItemSlot.ShiftInUse || ItemSlot.ControlInUse || Main.mouseItem.IsAir || (context != ItemSlot.Context.InventoryItem && context != ItemSlot.Context.ChestItem))
                return false;
            if (Main.mouseItem.type == ModContent.ItemType<Munny>())
            {
                storedMunny += (ulong)Main.mouseItem.stack;
                Main.mouseItem.TurnToAir();
                SoundEngine.PlaySound(SoundID.Grab);
            }
            return true;
        }
        public override void SaveData(TagCompound tag)
        {
            tag["StoredMunny"] = storedMunny;
        }
        public override void LoadData(TagCompound tag)
        {
            storedMunny = tag.Get<ulong>("StoredMunny");
        }
    }
}
