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
        public int storedMunny;

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
            Item.rare = ItemRarityID.Blue;
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
            if (Main.mouseItem.type == ModContent.ItemType<Munny>() && Main.mouseItem.stack >= new Item(ModContent.ItemType<Munny>()).maxStack && !KeyUtils.HasSpaceForMunny(Main.LocalPlayer, 1, out _, out _))
                return false;
            if (storedMunny > 0)
                return true;
            return false;
        }
        public override void RightClick(Player player)
        {
            int amount = Utils.Clamp(storedMunny, 0, 9999);
            int remainder = 0;
            bool intoInv = false;
            if ((Main.mouseItem.type == ModContent.ItemType<Munny>() ? Main.mouseItem.stack >= new Item(ModContent.ItemType<Munny>()).maxStack : !Main.mouseItem.IsAir) && KeyUtils.HasSpaceForMunny(Main.LocalPlayer, amount, out _, out remainder, false))
                intoInv = true;
            else if ((Main.mouseItem.IsAir || Main.mouseItem.type == ModContent.ItemType<Munny>()) && Main.mouseItem.stack + amount > new Item(ModContent.ItemType<Munny>()).maxStack)
                remainder = Main.mouseItem.stack + amount - new Item(ModContent.ItemType<Munny>()).maxStack;
            if (amount > 0)
            {
                storedMunny -= amount - remainder;
                if (intoInv)
                    player.GetItem(player.whoAmI, new Item(ModContent.ItemType<Munny>(), amount), new GetItemSettings(false, true, false, null));
                else
                {
                    Main.mouseItem.SetDefaults(ModContent.ItemType<Munny>());
                    Main.mouseItem.stack = amount;
                }
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
                storedMunny += Main.mouseItem.stack;
                Main.mouseItem.TurnToAir();
                SoundEngine.PlaySound(SoundID.Grab);
            }
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (storedMunny < 0)
                storedMunny = 0;
        }
        public override void SaveData(TagCompound tag)
        {
            tag["StoredMunny"] = storedMunny;
        }
        public override void LoadData(TagCompound tag)
        {
            storedMunny = tag.Get<int>("StoredMunny");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient<Munny>(50)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
}
